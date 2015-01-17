using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter.Messages
{
    public class ChangeDisplayNameMessage
    {
        private readonly string _displayName;
        public string DisplayName { get { return _displayName; } }
        public ChangeDisplayNameMessage(string displayName)
        {
            _displayName = displayName;
        }
    }
}
