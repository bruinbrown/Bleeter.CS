using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter.Messages
{
    public class PostBleetMessage
    {
        private readonly DateTime _postedAt;
        private readonly string _content;
        public DateTime PostedAt { get { return _postedAt; } }
        public string Content { get { return _content; } }

        public PostBleetMessage(DateTime postedAt, string content)
        {
            _postedAt = postedAt;
            _content = content;
        }
    }
}