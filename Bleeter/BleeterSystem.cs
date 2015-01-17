using Akka.Actor;
using Bleeter.Actors;
using Bleeter.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter
{
    public static class BleeterSystem
    {
        public static ActorSystem ActorSystem { get; private set; }

        public static void HydrateSystem()
        {
            ActorSystem = ActorSystem.Create("bleeter");
            var actor = ActorSystem.ActorOf<RealTimeService>("realtime");
        }
    }
}
