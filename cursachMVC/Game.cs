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
        private string _gameMode; 
        private Grid[,] gameMap;
        private IPlayer _firstPlayer;
        private IPlayer _secondPlayer;
        public bool queue { get; set; } //змінна яка служить для чередування ходів
        //public bool isPlaying { get; set; }
        public bool isWin { get; set; }
        public Game(string mode, Grid[,] gameMap)
        {
            _gameMode = mode;
            this.gameMap = gameMap;
            queue = true;
            _secondPlayer = new Player("X");
            //створення гравців залежно від режиму гри
            if (mode == "two ")
            {
                _firstPlayer = new Player("X");
                _secondPlayer.move = "0";
            }
            else if (mode == "single ")
            {
                _firstPlayer = new EnemyBot(gameMap);
                _secondPlayer.move = "0";
            }
            else if (mode == "two timer")
            {
                _firstPlayer = new Player("X");
                _secondPlayer.move = "0";
            }
            else if (mode == "single timer")
            {
                _firstPlayer = new EnemyBot(gameMap);
                _secondPlayer.move = "0";
            }
        }
        //метод в якому визначається черга ходу, переможець
        private void Move(Grid grid, IPlayer player)
        {
            grid.Text = player.move;
            
            if (_gameMode.IndexOf('r') == -1)  //якщо гра не на таймер після кожного ходу перевіряється чи виграв гравець який походив
            {
                if (IsWin(player.move))
                {
                    isWin = true;
                    queue = true;
                    MessageBox.Show($"Гравець {player.move} виграв");
                    return;
                }
            }
        }
        public void GridClick(object sender, EventArgs e)
        {
            Grid grid = (Grid)sender;
            if (_gameMode.IndexOf("two") != -1)  //гра з іншим гравцем
            {
                if (queue)  //хід першого або другого гравця
                {
                    grid.BackColor = Color.Green;
                    queue = false;
                    Move(grid, _firstPlayer);
                    
                }
                else
                {
                    queue = true;
                    Move(grid, _secondPlayer);
                    
                }
            }
            else  //гра з ботом
            {
                grid.Text = _secondPlayer.move;
                if (_gameMode.IndexOf('r') == -1)
                {
                    if (IsWin(_secondPlayer.move))
                    {
                        isWin = true;
                        MessageBox.Show($"Гравець {_secondPlayer.move} виграв");
                    }
                }
                if (!isWin)
                    _firstPlayer.Move();  //ход бота
                if (_gameMode.IndexOf('r') == -1)
                {
                    if (IsWin(_firstPlayer.move))
                    {
                        isWin = true;
                        MessageBox.Show($"Гравець {_firstPlayer.move} виграв");

                    }
                }
            }
        }
        public void CountPoints ()  //метод який викликається при грі з таймером для підрахунку кількості ліній 5 в ряд
        {
            int countX=0, count0 = 0;
            while (IsWin("X") == true)
            {
                countX++;
            }
            while (IsWin("0") == true)
            {
                count0++;
            }
            if (countX > count0)
            {
                MessageBox.Show("Гравець X виграв");
            }
            else if (countX == count0)
            {
                MessageBox.Show("Нічия");
            }
            else
            {
                MessageBox.Show("Гравець 0 виграв");
            }
        }
        private bool IsWin(string hod)   //перевірити яи коректно визначає переможця
        {
            for (int i = 0; i < 20; i++) //перевірка кожної клітинки на те чи веред від неї ще 4 клітинки з одним символом в ряд
            {
                for (int j = 0; j < 20; j++)
                {
                    if (i + 4 < 20)
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
                    }
                    if (j + 4 < 20)
                    {
                        if (gameMap[i, j].Text == hod && gameMap[i, j + 1].Text == hod && gameMap[i, j + 2].Text == hod && gameMap[i, j + 3].Text == hod && gameMap[i, j + 4].Text == hod)
                        {
                            gameMap[i, j].Text = "";
                            gameMap[i, j + 1].Text = "";
                            gameMap[i, j + 2].Text = "";
                            gameMap[i, j + 3].Text = "";
                            gameMap[i, j + 4].Text = "";
                            return true;
                        }
                    }
                    if (j + 4 < 20 && i + 4 < 20)
                    {
                        if (gameMap[i, j].Text == hod && gameMap[i + 1, j + 1].Text == hod && gameMap[i + 2, j + 2].Text == hod && gameMap[i + 3, j + 3].Text == hod && gameMap[i + 4, j + 4].Text == hod)
                        {
                            gameMap[i, j].Text = "";
                            gameMap[i + 1, j + 1].Text = "";
                            gameMap[i + 2, j + 2].Text = "";
                            gameMap[i + 3, j + 3].Text = "";
                            gameMap[i + 4, j + 4].Text = "";
                            return true;
                        }
                    }
                    if (i + 4 < 20 && j - 4 > 0)
                    {
                        if (gameMap[i, j].Text == hod && gameMap[i + 1, j - 1].Text == hod && gameMap[i + 2, j - 2].Text == hod && gameMap[i + 3, j - 3].Text == hod && gameMap[i + 4, j - 4].Text == hod)
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
            }
            return false;
        }
    }
}
