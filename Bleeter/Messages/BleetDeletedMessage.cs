using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter.Messages
{
    public class BleetDeletedMessage
    {
        private readonly string _identifier;
        public string Identifier { get { return _identifier; } }
        public BleetDeletedMessage(string identifier)
        {
            _identifier = identifier;
        }
    }
}
