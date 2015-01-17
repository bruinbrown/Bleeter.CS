using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter.Messages
{
    public class DeleteBleetMessage
    {
        private readonly string _identifier;
        public string Identifier { get { return _identifier; } }
        public DeleteBleetMessage(string identifier)
        {
            _identifier = identifier;
        }
    }
}
