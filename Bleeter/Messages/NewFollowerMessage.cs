using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter.Messages
{
    public class NewFollowerMessage
    {
        private readonly string _followerName;
        public string FollowerName { get { return _followerName; } }

        public NewFollowerMessage(string followerName)
        {
            _followerName = followerName;
        }
    }
}
