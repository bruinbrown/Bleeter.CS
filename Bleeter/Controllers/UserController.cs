using Akka.Actor;
using Bleeter.Messages;
using Bleeter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bleeter.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        private ActorSystem _system;
        public UserController()
        {
            _system = BleeterSystem.ActorSystem;
        }

        [Route("feed")]
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Bleet>> GetFeed()
        {
            var userid = ((ClaimsIdentity)User.Identity).FindFirst("sub").Value;
            var actor = _system.ActorSelection(String.Format("akka://bleeter/user/{0}", userid));
            var response = await actor.Ask(new FeedRequestMessage()) as BleetsCollectionMessage;
            return response.Bleets;
        }

        [Route("{userid}/bleets")]
        [HttpGet]
        public async Task<IEnumerable<Bleet>> GetBleets(string userid)
        {
            var userActor = _system.ActorSelection(String.Format("akka://bleeter/user/{0}", userid));
            var response = await userActor.Ask(new BleetsRequestMessage()) as BleetsCollectionMessage;
            return response.Bleets;
        }

        [Route("{userid}/details")]
        [HttpGet]
        public async Task<UserDetailsModel> GetUserDetails(string userid)
        {
            var actor = _system.ActorSelection(String.Format("akka://bleeter/user/{0}", userid));
            var userDetails = await actor.Ask<UserDetailsResponseMessage>(new UserDetailsRequestMessage());
            return userDetails.UserDetails;
        }

        [Route("details")]
        [HttpPut]
        [Authorize]
        public async Task<UserDetailsModel> GetLoggedInUserDetails()
        {
            var username = ((ClaimsIdentity)User.Identity).FindFirst("sub").Value;
            var actor = _system.ActorSelection(String.Format("akka://bleeter/user/{0}", username));
            var userDetails = await actor.Ask<UserDetailsResponseMessage>(new UserDetailsRequestMessage());
            return userDetails.UserDetails;
        }

        [Route("details")]
        [HttpPut]
        [Authorize]
        public IHttpActionResult ModifyUserDetails([FromBody]string displayName)
        {
            var userid = ((ClaimsIdentity)User.Identity).FindFirst("sub").Value;
            var message = new ChangeDisplayNameMessage(displayName);
            _system.ActorSelection(userid).Tell(message);
            return this.Ok();
        }

        [Route("{userid}/follow")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult FollowUser(string userid)
        {
            var currentUser = ((ClaimsIdentity)User.Identity).FindFirst("sub").Value;
            var actor = _system.ActorSelection(currentUser); 
            actor.Tell(new FollowMessage(userid));
            return this.Ok();
        }

        [Route("{userid}/unfollow")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult UnfollowUser(string userid)
        {
            var currentUser = ((ClaimsIdentity)User.Identity).FindFirst("sub").Value;
            var actor = _system.ActorSelection(currentUser);
            actor.Tell(new UnfollowerMessage(userid));
            return this.Ok();
        }
    }
}
