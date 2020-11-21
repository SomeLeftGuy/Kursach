using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models
{
    public class Mark
    {
        public int id { get; set; }
        public int value { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
        public int fanficId { get; set; }
        public Fanfic fanfic { get; set; }
    }
}
