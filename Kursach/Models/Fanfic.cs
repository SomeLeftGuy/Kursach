﻿using Microsoft.EntityFrameworkCore;
using System;

namespace Kursach.Models
{
    public class Fanfic
    {
        public int id { get; set; }
        public string tags { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public string userId { get; set; }
        public DateTime endDate { get; set; }
        public DateTime createDate { get; set; }
        public string requiredCh { get; set; }
        public string endedCh { get; set; }
        public string image { get; set; }
        public User user { get; set; }
    }
 

}
