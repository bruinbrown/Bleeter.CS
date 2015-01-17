using Akka.Actor;
using Bleeter.Actors;
using Bleeter.Messages;
using Bleeter.Model;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter.Realtime
{
    public class CallablePersistentConnection : PersistentConnection
    {
        ActorSystem _system;
        public CallablePersistentConnection()
        {
            _system = BleeterSystem.ActorSystem;
        }

        protected override bool AuthorizeRequest(IRequest request)
        {
            if (request.User != null && request.User.Identity != null)
            {
                return request.User.Identity.IsAuthenticated;
            }
            return false;
        }
        protected override async Task OnConnected(IRequest request, string connectionId)
        {
            var username = ((ClaimsIdentity)request.User.Identity).FindFirst("sub").Value;
            await Groups.Add(connectionId, username);
        }

        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {
            var username = ((ClaimsIdentity)request.User.Identity).FindFirst("sub").Value;
            return Groups.Remove(connectionId, username);
        }
    }
}
