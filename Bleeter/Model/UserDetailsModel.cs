using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter.Model
{
    public class UserDetailsModel
    {
        private readonly string _username;
        private readonly string _displayName;
        private readonly int _bleetsCount;
        private readonly int _followersCount;
        private readonly int _followingCount;

        public string Username { get { return _username; } }
        public string DisplayName { get { return _displayName; } }
        public int BleetsCount { get { return _bleetsCount; } }
        public int FollowersCount { get { return _followersCount; } }
        public int FollowingCount { get { return _followingCount; } }

        public UserDetailsModel(string username, string displayName, int bleetsCount, int followersCount, int followingCount)
        {
            _username = username;
            _displayName = displayName;
            _bleetsCount = bleetsCount;
            _followersCount = followersCount;
            _followingCount = followingCount;
        }
    }
}
