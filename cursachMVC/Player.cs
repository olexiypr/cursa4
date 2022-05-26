using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cursachMVC
{
    internal class Player : IPlayer
    {
        string hod { get; set; }
        public Player(string hod)
        {
            this.hod = hod;
        }
        string IPlayer.hod { get => hod; set => hod = value; }
    }
}
