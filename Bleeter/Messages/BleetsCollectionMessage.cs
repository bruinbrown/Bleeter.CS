using Bleeter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter.Messages
{
    public class BleetsCollectionMessage
    {
        private readonly IEnumerable<Bleet> _bleets;
        public IEnumerable<Bleet> Bleets { get { return _bleets; } }
        public BleetsCollectionMessage(IEnumerable<Bleet> bleets)
        {
            _bleets = bleets;
        }
    }
}
