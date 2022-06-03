using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cursachMVC
{
    internal class Player : IPlayer  //гравець (людина)
    {
        public string move { get; set; } 
        public Player(string hod)
        {
            this.move = hod;
        }
        public void Move()
        {
            
        }
    }
}
