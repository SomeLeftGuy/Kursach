using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.ModelViews
{
    public class ProfileViewModel
    {
        public bool accessType { get; set; }

        public FanficView[] fanfics { get; set; }
       // public AchivesView[] bonuses { get; set; }
        public int removeAchivesID { get; set; }
    }
    public class FanficView
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime createDate { get; set; }
        public DateTime endDate { get; set; }
        public string requiredCh { get; set; }
        public string endedCh { get; set; }
        public bool selected { get; set; }
    }
}
