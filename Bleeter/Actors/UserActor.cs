using Akka.Actor;
using Bleeter.Messages;
using Bleeter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter.Actors
{
    public class UserActor : TypedActor,
                             IHandle<NewFollowerMessage>,
                             IHandle<NewBleetMessage>,
                             IHandle<UnfollowerMessage>,
                             IHandle<PostBleetMessage>,
                             IHandle<ChangeDisplayNameMessage>,
                             IHandle<DeleteBleetMessage>,
                             IHandle<BleetDeletedMessage>,
                             IHandle<UserDetailsRequestMessage>,
                             IHandle<FollowMessage>,
                             IHandle<BleetsRequestMessage>,
                             IHandle<FeedRequestMessage>
    {
        private List<string> _followers;
        private List<string> _following;
        private List<Bleet> _feed;
        private List<Bleet> _bleets;
        private string _displayName;

        public List<string> Followers { get { return _followers; } }
        public List<string> Following { get { return _following; } }
        public List<Bleet> Feed { get { return _feed; } }
        public List<Bleet> Bleets { get { return _bleets; } }
        public string DisplayName { get { return _displayName; } }

        public UserActor(string displayName)
        {
            _displayName = displayName;
        }

        protected override void PreStart()
        {
            // If an actor gets restarted then we can reload all of the state in here
            _followers = new List<string>();
            _following = new List<string>();
            _feed = new List<Bleet>();
            _bleets = new List<Bleet>();
            base.PreStart();
        }

        private string Username { get { return this.Self.Path.Name; } }

        private ActorSelection RealtimeService { get { return Context.ActorSelection("../realtime"); } }

        private ActorSelection ActorForUsername(string username)
        {
            return Context.ActorSelection(String.Format("../{0}", username));
        }

        public void Handle(NewFollowerMessage message)
        {
            _followers.Add(message.FollowerName);
        }

        public void Handle(NewBleetMessage message)
        {
            _feed.Add(message.Bleet);
            _feed = _feed.Take(300).ToList();
            var notification = new RealtimeMessage(Username, message.Bleet);
            RealtimeService.Tell(notification);
        }

        public void Handle(UnfollowerMessage message)
        {
            _followers.Remove(message.UnfollowerName);
        }

        public void Handle(PostBleetMessage message)
        {
            var bleet = new Bleet(Username, message.Content, message.PostedAt, Guid.NewGuid().ToString());
            _bleets.Add(bleet);
            var notificationMessage = new RealtimeMessage(Username, bleet);
            RealtimeService.Tell(notificationMessage);
            var msg = new NewBleetMessage(bleet);
            foreach(var follower in _followers)
            {
                ActorForUsername(follower).Tell(msg);
            }
        }

        public void Handle(ChangeDisplayNameMessage message)
        {
            _displayName = message.DisplayName;
        }

        public void Handle(DeleteBleetMessage message)
        {
            _bleets.RemoveAll(b => b.Identifier == message.Identifier);
            var msg = new BleetDeletedMessage(message.Identifier);
            foreach(var follower in _followers)
            {
                ActorForUsername(follower).Tell(msg);
            }
        }

        public void Handle(BleetDeletedMessage message)
        {
            _feed.RemoveAll(b => b.Identifier == message.Identifier);
        }

        public void Handle(UserDetailsRequestMessage message)
        {
            var userDetails = new UserDetailsModel(Username, _displayName, _bleets.Count, _followers.Count, _following.Count);
            var msg = new UserDetailsResponseMessage(userDetails);
            Sender.Tell(msg);
        }

        public void Handle(FollowMessage message)
        {
            _following.Add(message.Username);
            var msg = new NewFollowerMessage(Username);
            var bleets = new BleetsCollectionMessage(_bleets.Take(300));
            ActorForUsername(message.Username).Tell(msg);
            ActorForUsername(message.Username).Tell(bleets);
        }

        public void Handle(FeedRequestMessage message)
        {
            Sender.Tell(new BleetsCollectionMessage(this.Feed));
        }

        public void Handle(BleetsRequestMessage message)
        {
            Sender.Tell(new BleetsCollectionMessage(this.Bleets));
        }
    }
}
