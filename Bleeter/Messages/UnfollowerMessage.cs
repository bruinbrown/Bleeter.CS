using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter.Messages
{
    public class UnfollowerMessage
    {
        private readonly string _unfollowerName;
        public string UnfollowerName { get { return _unfollowerName; } }
        public UnfollowerMessage(string unfollowerName)
        {
            _unfollowerName = unfollowerName;
        }
    }
}
