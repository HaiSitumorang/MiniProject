using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.Model.Entitites
{
    public class Game
    {
        public int Idgame { get; set; }
        public string Name { get; set; }
        public string Release_date { get; set; }
        public int Rating { get; set; }
        public string Genre { get; set; }
    }
}
