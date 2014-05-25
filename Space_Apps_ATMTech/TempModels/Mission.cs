using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Apps_ATMTech.TempModels
{

    public class Mission
    {
        public int MissionId { get; set; }
        public String Title { get; set; }
        public bool Following { get; set; }
        public String Content { get; set; }
        public String ImageUrl { get; set; }
        public String VideoUrl { get; set; }
        public String Agency { get; set; }
        public bool hasLiveStream { get; set; }
        public String LaunchDate { get; set; }
    }
}
