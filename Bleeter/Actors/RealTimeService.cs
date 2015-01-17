using Akka.Actor;
using Bleeter.Messages;
using Bleeter.Realtime;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter.Actors
{
    public class RealTimeService : TypedActor,
                                   IHandle<RealtimeMessage>
    {
        IPersistentConnectionContext _connection;

        protected override void PreStart()
        {
            _connection = GlobalHost.ConnectionManager.GetConnectionContext<CallablePersistentConnection>();
            base.PreStart();
        }

        public void Handle(RealtimeMessage message)
        {
            _connection.Groups.Send(message.Target, message.Content);
        }
    }
}
