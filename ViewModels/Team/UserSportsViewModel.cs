using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UserSportsViewModel
    {
        public Sport SportId { get; set; }
        public SportType SportTypeId {  get; set; }
        public string Sport { get; set; }
        public string SportType { get; set; }
        public string SportName {  get; set; }
        public int TeamsCount { get; set; }
    }
}
