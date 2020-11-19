using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.ModelViews
{
    public class FanficViewModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string FanficName { get; set; }
        [Required]
        public string Theme { get; set; }
        public string[] Themes { get; set; }
        public string Tags { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        [Required]
        public string requiredCh { get; set; }
        [Required]
        public string EndDate { get; set; }

    }
}
