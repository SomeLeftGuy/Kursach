using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models
{
    public class Chapters
    {
        public int id { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public string image { get; set; }
        public int fanficId { get; set; }
        public Fanfic fanfic { get; set; }
    }
}
