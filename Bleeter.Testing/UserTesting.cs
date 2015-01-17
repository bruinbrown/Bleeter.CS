using Akka.Actor;
using Akka.TestKit.Xunit;
using Bleeter.Actors;
using Bleeter.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bleeter.Testing
{
    public class UserTesting : TestKit
    {
        [Fact]
        public void ReceivingAFollowMessageAddsAFollowerToTheUser()
        {
            var props = Props.Create(typeof(UserActor), "Anthony Brown");
            var user = ActorOfAsTestActorRef<UserActor>(props, "bruinbrown");
            user.Tell(new FollowMessage("user2"));
            var userActor = user.UnderlyingActor;
            Assert.Equal(1, userActor.Following.Count);
        }
    }
}
