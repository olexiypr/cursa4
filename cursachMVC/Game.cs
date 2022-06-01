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
        bool queue;
        public bool isPlaying { get; set; }
        public bool isWin { get; set; }
        public Game(string mode, Grid[,] gameMap)
        {
            this.mode = mode;
            this.gameMap = gameMap;
            queue = true;
            secondPlayer = new Player("X");
            if (mode == "two ")
            {
                firstPlayer = new Player("X");
                secondPlayer.hod = "0";
            }
            else if (mode == "single ")
            {
                firstPlayer = new EnemyBot(gameMap);
                //firstPlayer.FirstHod();
                secondPlayer.hod = "0";
            }
            else if (mode == "two timer")
            {
                firstPlayer = new Player("X");
                secondPlayer.hod = "0";
            }
            else if (mode == "single timer")
            {
                firstPlayer = new EnemyBot(gameMap);
                secondPlayer.hod = "0";
                //firstPlayer.FirstHod();
            }
        }

        public void GridClick(object sender, EventArgs e)
        {
            Grid grid = (Grid)sender;
            if (mode.IndexOf("two") != -1)
            {
                if (queue)
                {
                    grid.Text = firstPlayer.hod;
                    //CountPoints();
                    if (mode.IndexOf('r') == -1)
                    {
                        if (IsWin(firstPlayer.hod))
                        {
                            isWin = true;
                            queue = true;
                            MessageBox.Show($"Гравець {firstPlayer.hod} виграв");
                            return;
                        }
                    }
                    queue = false;
                }
                else
                {
                    grid.Text = secondPlayer.hod;
                    //CountPoints();
                    if (mode.IndexOf('r') == -1)
                    {
                        if (IsWin(secondPlayer.hod))
                        {
                            isWin = true;
                            queue = true;
                            MessageBox.Show($"Гравець {secondPlayer.hod} виграв");
                            return;
                        }
                    }
                    queue = true;
                }
            }
            else  //ботяра
            {
                grid.Text = secondPlayer.hod;
                //CountPoints();
                if (mode.IndexOf('r') == -1)
                {
                    if (IsWin(secondPlayer.hod))
                    {
                        isWin = true;
                        
                        MessageBox.Show($"Гравець {secondPlayer.hod} виграв");
                    }
                }
                if (!isWin)
                    firstPlayer.Hod();
                //CountPoints();
                if (mode.IndexOf('r') == -1)
                {
                    if (IsWin(firstPlayer.hod))
                    {
                        isWin = true;
                        
                        MessageBox.Show($"Гравець {firstPlayer.hod} виграв");

                    }
                }
            }
            
            /*if (firstPlayer.hod == "X")
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
                *//*if (isWin)
                    firstPlayer.hod = "0";*/
                /*else*//*
                    firstPlayer.hod = "X";
            }*/
        }
        public void CountPoints ()
        {
            int countX=0, count0 = 0;
            if (!isPlaying || isPlaying) //проблема з цією змінною
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
        private bool IsWin(string hod)   //перевірити яи коректно визначає переможця (можливі помилки через зміну індексів)
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
