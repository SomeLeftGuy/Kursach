using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models
{
    public class TagsToFanfics
    {
        public int id { get; set; }
        public int tagId { get; set; }
        public Tag tag { get; set; }
        public int companyId { get; set; }
        public Fanfic Fanfic { get; set; }
    }
}
