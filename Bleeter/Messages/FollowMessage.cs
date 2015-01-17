using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter.Messages
{
    public class FollowMessage
    {
        private readonly string _username;
        public string Username { get { return _username; } }
        public FollowMessage(string username)
        {
            _username = username;
        }
    }
}
