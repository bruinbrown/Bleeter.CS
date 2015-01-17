using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter.Messages
{
    public class RealtimeMessage
    {
        private readonly string _target;
        private readonly object _content;
        public string Target { get { return _target; } }
        public object Content { get { return _content; } }
        public RealtimeMessage(string target, object content)
        {
            _target = target;
            _content = content;
        }
    }
}
