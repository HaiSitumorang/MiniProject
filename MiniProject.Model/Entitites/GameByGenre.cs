using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.Model.Entitites
{
    public class GameByGenre
    {
        public string Genre { get; set; }
        public List<string> Name { get; set; }

        public GameByGenre()
        {
            Name = new List<string>();
        }
    }
}
