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
        public bool isPlaying { get; set; }
        public bool isWin { get; set; }
        public Game(string mode, Grid[,] gameMap)
        {
            this.mode = mode;
            this.gameMap = gameMap;
            /*if (mode == "two")
            {*/
                firstPlayer = new Player("X");
            /*}
            else if (mode == "single")
            {
                firstPlayer = new EnemyBot();
                secondPlayer = new Player("0");
            }*/
        }

        public void GridClick(object sender, EventArgs e)
        {
            Grid grid = (Grid)sender;
            if (firstPlayer.hod == "X")
            {
                grid.Text = firstPlayer.hod;
                CountPoints();
                if (mode.IndexOf('r') == -1)
                {
                    if (IsWin(firstPlayer.hod))
                    {
                        isWin = true;
                        MessageBox.Show($"Гравець {firstPlayer.hod} виграв");
                        
                    }
                }
                if (isWin) 
                    firstPlayer.hod = "X";
                else 
                    firstPlayer.hod = "0";
            }
            else
            {
                grid.Text = firstPlayer.hod;
                CountPoints();
                if (mode.IndexOf('r') == -1)
                {
                    if (IsWin(firstPlayer.hod))
                    {
                        isWin = true;
                        MessageBox.Show($"Гравець {firstPlayer.hod} виграв");
                    }
                }
                /*if (isWin)
                    firstPlayer.hod = "0";*/
                /*else*/
                    firstPlayer.hod = "X";
            }
        }
        private void Win ()
        {

        }
        public void CountPoints ()
        {
            int countX=0, count0 = 0;
            if (!isPlaying)
            {
                while (IsWin("X")==true)
                {
                    countX++;
                }
                while (IsWin("0")==true)
                {
                    count0++;
                }
                if (countX>count0)
                {
                    MessageBox.Show($"Гравець X вигравc");
                }
                else
                {
                    MessageBox.Show($"Гравець 0 вигравc");
                }
            }
        }
        private bool IsWin(string hod)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    try
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
                    }catch (NullReferenceException e)
                    {
                        continue;
                    }
                }
            }
            return false;
        }
    }
}
