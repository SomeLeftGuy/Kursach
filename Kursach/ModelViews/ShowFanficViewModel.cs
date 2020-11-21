using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Kursach.ModelViews
{
    public class ShowFanficViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public object Image { get; set; }
        public bool[] mark { get; set; }
        public double marks { get; set; }
        public string User { get; set; }
        public CommentsView[] comments { get; set; }
        public ChapterView[] chapters { get; set; }
        public int deletechapterID { get; set; }
        public ChapterView addChapter { get; set; }
    }

    public class CommentsView
    {
        public int id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
    public class ChapterView
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
    }
}

