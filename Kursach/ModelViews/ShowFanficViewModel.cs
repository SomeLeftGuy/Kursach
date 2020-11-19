using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

namespace Kursach.ModelViews
{
    public class ShowFanficViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public bool[] mark { get; set; }
        public double marks { get; set; }
        public string User { get; set; }
        public CommentsView[] comments { get; set; }

    }

    public class CommentsView
    {
        public int id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}

