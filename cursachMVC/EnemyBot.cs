using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cursachMVC
{
    internal class EnemyBot : IPlayer
    {
        string hod { get; set; }
        string IPlayer.hod { get => hod; set => hod = value; }
        Grid[,] gameMap;
        public EnemyBot(Grid[,] gameMap)
        {
            hod = "X";
            this.gameMap = gameMap;
        }
        public void Hod ()
        {

        }
    }
}
