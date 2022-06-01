using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cursachMVC
{
    internal class Player : IPlayer
    {
        public string hod { get; set; }
        public Player(string hod)
        {
            this.hod = hod;
        }
        public void Hod()
        {
            
        }
    }
}
