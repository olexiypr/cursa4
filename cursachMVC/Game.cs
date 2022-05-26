using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace cursachMVC
{
    internal class Game
    {
        private string mode;
        Grid[,] gameMap;
        IPlayer firstPlayer;
        IPlayer secondPlayer;
        public bool isWin { get; private set; }
        public Game(string mode, Grid[,] gameMap)
        {
            this.mode = mode;
            this.gameMap = gameMap;
            //if (mode == "two")
            //{
                firstPlayer = new Player("X");
            //}
        }

        public void GridClick(object sender, EventArgs e)
        {
            Grid grid = (Grid)sender;
            if (firstPlayer.hod == "X")
            {
                grid.Text = firstPlayer.hod;
                if (IsWin(firstPlayer.hod))
                {
                    isWin = true;
                    MessageBox.Show($"Гравець {firstPlayer.hod} виграв");
                }
                firstPlayer.hod = "0";
            }
            else
            {
                grid.Text = firstPlayer.hod;
                if (IsWin(firstPlayer.hod))
                {
                    isWin = true;
                    MessageBox.Show($"Гравець {firstPlayer.hod} виграв");
                }
                firstPlayer.hod = "X";
            }
            
        }
        private bool IsWin(string hod)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (gameMap[i, j].Text == hod && gameMap[i + 1, j].Text == hod && gameMap[i + 2, j].Text == hod && gameMap[i + 3, j].Text == hod && gameMap[i + 4, j].Text == hod)
                    {
                        gameMap[i, j].Text = "";
                        gameMap[i + 1, j].Text = "";
                        gameMap[i + 2, j].Text = "";
                        gameMap[i + 3, j].Text = "";
                        gameMap[i + 4, j].Text = "";
                        return true;
                    }
                    else if (gameMap[i, j].Text == hod && gameMap[i, j + 1].Text == hod && gameMap[i, j + 2].Text == hod && gameMap[i, j + 3].Text == hod && gameMap[i, j + 4].Text == hod)
                    {
                        gameMap[i, j].Text = "";
                        gameMap[i, j + 1].Text = "";
                        gameMap[i, j + 2].Text = "";
                        gameMap[i, j + 3].Text = "";
                        gameMap[i, j + 4].Text = "";
                        return true;
                    }
                    else if (gameMap[i, j].Text == hod && gameMap[i + 1, j + 1].Text == hod && gameMap[i + 2, j + 2].Text == hod && gameMap[i + 3, j + 3].Text == hod && gameMap[i + 4, j + 4].Text == hod)
                    {
                        gameMap[i, j].Text = "";
                        gameMap[i + 1, j + 1].Text = "";
                        gameMap[i + 2, j + 2].Text = "";
                        gameMap[i + 3, j + 3].Text = "";
                        gameMap[i + 4, j + 4].Text = "";
                        return true;
                    }
                    else if (gameMap[i, j].Text == hod && gameMap[i + 1, j - 1].Text == hod && gameMap[i + 2, j - 2].Text == hod && gameMap[i + 3, j - 3].Text == hod && gameMap[i + 4, j - 4].Text == hod)
                    {
                        gameMap[i, j].Text = "";
                        gameMap[i + 1, j - 1].Text = "";
                        gameMap[i + 2, j - 2].Text = "";
                        gameMap[i + 3, j - 3].Text = "";
                        gameMap[i + 4, j - 4].Text = "";
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
