﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models
{
    public class Comment
    {
        public int id { get; set; }
        public string text { get; set; }
        public string userId { get; set; }
        public User user { get; set; }
        public int fanficId { get; set; }
        public Fanfic fanfic { get; set; }
    }
}
