using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.Model.Entitites
{
    public class Games
    {
        public int Idgame { get; set; }
        public string Name { get; set; }
        public string Release_date { get; set; }
        public int Rating { get; set; }
        public List<string> Genre { get; set; }
        public Games()
        {
            Genre = new List<string>();
        }
    }
}
