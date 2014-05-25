using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Apps_ATMTech.TempModels
{
    public class News
    {
        public int ID { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Date { get; set; }
        public String ImageUrl { get; set; }
        public int MissionID { get; set; }

    }
}
