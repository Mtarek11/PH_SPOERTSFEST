using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class TeamsViewModelForAdmin
    {
        public string TeamName { get; set; }
        public string Sport { get; set; }
        public string SportType { get; set; }
        public List<PlayerViewModelForAdmin> Players { get; set; }
    }
}
