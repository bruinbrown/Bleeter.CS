using Bleeter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter.Messages
{
    public class UserDetailsResponseMessage
    {
        private readonly UserDetailsModel _userDetails;
        public UserDetailsModel UserDetails { get { return _userDetails; } }
        public UserDetailsResponseMessage(UserDetailsModel userDetails)
        {
            _userDetails = userDetails;
        }
    }
}
