using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleeter.Model
{
    public class Bleet
    {
        public string Poster { get; set; }
        public string Content { get; set; }
        public DateTime PostedAt { get; set; }
        public string Identifier { get; set; }
        public Bleet(string poster, string content, DateTime postedAt, string identifier)
        {
            Poster = poster;
            Content = content;
            PostedAt = postedAt;
            Identifier = identifier;
        }
    }
}
