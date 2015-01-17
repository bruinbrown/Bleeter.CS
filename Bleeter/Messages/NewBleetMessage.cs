using Bleeter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter.Messages
{
    public class NewBleetMessage
    {
        private readonly Bleet _bleet;
        public Bleet Bleet { get { return _bleet; } }

        public NewBleetMessage(Bleet bleet)
        {
            _bleet = bleet;
        }
    }
}
