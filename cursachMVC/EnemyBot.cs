using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cursachMVC
{
    internal class EnemyBot : IPlayer
    {
        public string move { get; set; }
        Grid[,] _gameMap;
        List<string> _patternsStrForX;
        List<string> _patternsStrForO;
        List<int> _patternsKey;
        public EnemyBot(Grid[,] gameMap)
        {
            move = "X";
            this._gameMap = gameMap;

            _patternsStrForX = new List<string>();
            _patternsStrForO = new List<string>();
            _patternsKey = new List<int>();
            FillPatterns();
        }
        private void FillPatterns() //заповнення шаблонів
        {
            _patternsKey.Add(100000);
            _patternsKey.Add(7000);
            _patternsKey.Add(4000);
            _patternsKey.Add(4000);
            _patternsKey.Add(2000);
            _patternsKey.Add(2000);
            _patternsKey.Add(2000);
            _patternsKey.Add(2000);
            _patternsKey.Add(2000);
            _patternsKey.Add(2000);
            _patternsKey.Add(3000);
            _patternsKey.Add(1500);
            _patternsKey.Add(1500);
            _patternsKey.Add(800);
            _patternsKey.Add(800);
            _patternsKey.Add(800);
            _patternsKey.Add(800);
            _patternsKey.Add(200);
            ////////////////////////////////
            _patternsStrForX.Add("XXXXX");
            _patternsStrForX.Add(" XXXX ");
            _patternsStrForX.Add("XXXX ");
            _patternsStrForX.Add(" XXXX");
            _patternsStrForX.Add(" X XXX");
            _patternsStrForX.Add(" XX XX");
            _patternsStrForX.Add(" XXX X");
            _patternsStrForX.Add("XXX X ");
            _patternsStrForX.Add("XX XX ");
            _patternsStrForX.Add("X XXX ");
            _patternsStrForX.Add(" XXX ");
            _patternsStrForX.Add(" XXX");
            _patternsStrForX.Add("XXX ");
            _patternsStrForX.Add(" XX X");
            _patternsStrForX.Add("XX X ");
            _patternsStrForX.Add(" X XX");
            _patternsStrForX.Add("X XX ");
            _patternsStrForX.Add(" XX ");
            ////////////////////////////
            foreach (var a in _patternsStrForX)
            {
                _patternsStrForO.Add(a.Replace('X', '0'));
            }
        }
        //(якщо вага ходу противника(людини) бульша за вагу ходу бота,
        //то бот робить хід для блокування можливої виграшної комбінації,
        //якщо навпаки то бот робить хід для своєї перемоги)
        public void Move() //метод для здійснення ходу
        {
            List<Grid> possibleMoves = new List<Grid>();
            List<int> grades = new List<int>();
            //possibleMoves = PosMov();
            for (int i = 0; i < _gameMap.GetLength(0); i++)
            {
                for (int j = 0; j < _gameMap.GetLength(1); j++)
                {

                    if (IsPossibleMove(_gameMap[i, j]))
                    {
                        possibleMoves.Add(_gameMap[i, j]);
                        //gameMap[i, j].BackColor = Color.Red;
                    }
                }
            }
            possibleMoves.ForEach(move =>  //оцінювання кожного можливого ходу
            {
                grades.Add(GetGrade(move));
            });
            
            int maxGrade = 0;
            int index = 0;
            for (int i=0; i<grades.Count; i++)
            {
                if (maxGrade<grades[i])
                {
                    maxGrade = grades[i];
                    index = i;
                }
            }
            possibleMoves[index].Text = "X";
            possibleMoves[index].Enabled = false;
            possibleMoves[index].BackColor = Color.Green;
        }
        private int GetGrade(Grid move) //метод для оцінювання кожного можливого ходу
        {
            List<int> grades = new List<int>(); //оцінки по одному паретну по всіх напрямках
            List<int> enemyGrades = new List<int>();  //оцінки моїх ходів по одному паттерну та всіх напрямках
            for (int i=0; i< _patternsStrForX.Count; i++)
            {   
                if (CheckDiagonalUpRight(_patternsStrForX[i], move) || CheckHorizontalRight(_patternsStrForX[i], move) || CheckDiagonalyDownRigth(_patternsStrForX[i], move) || 
                    CheckDown(_patternsStrForX[i], move) || CheckDiagonalyDownLeft(_patternsStrForX[i], move) || CheckHorizontalLeft(_patternsStrForX[i], move) ||
                    CheckDiagonalUplLeft(_patternsStrForX[i], move) || CheckUp(_patternsStrForX[i], move))
                {
                    grades.Add(_patternsKey[i]);
                }
                else
                {
                    grades.Add(0);
                }
                if (CheckDiagonalUpRight(_patternsStrForO[i], move) || CheckHorizontalRight(_patternsStrForO[i], move) || CheckDiagonalyDownRigth(_patternsStrForO[i], move) ||
                    CheckDown(_patternsStrForO[i], move) || CheckDiagonalyDownLeft(_patternsStrForO[i], move) || CheckHorizontalLeft(_patternsStrForO[i], move) ||
                    CheckDiagonalUplLeft(_patternsStrForO[i], move) || CheckUp(_patternsStrForO[i], move))
                {
                    enemyGrades.Add(_patternsKey[i]);
                }
                else
                {
                    enemyGrades.Add(0);
                }
            }
            if (grades.Max()>enemyGrades.Max())  //знаходження максимальної ваги ходу (знайдене співпадіння з паттерном який має найбільшу вагу)
                return grades.Max();     
            return enemyGrades.Max();
        }
        ///////////Методи для перевірки співпадіння з паттерном у всих напрямках від можливого ходу///////////
        ////
        private bool CheckDiagonalUpRight(string pattern, Grid move)
        {
            int count = 0;
            for (int i = 0; i < pattern.Length; i++)
            {
                if (move.indexX+pattern.Length-1 >=_gameMap.GetLength(0) || move.indexY - (pattern.Length - 1) >= _gameMap.GetLength(1) || move.indexY - (pattern.Length - 1)<0)
                    return false;
                /*try
                {*/
                    if ((_gameMap[move.indexY - i, move.indexX + i].Text == "" && pattern[i] == ' ') ||   /////ex
                    _gameMap[move.indexY - i, move.indexX + i].Text == pattern[i].ToString())
                    {
                        count++;
                    }
                /*}
                catch
                {
                    return false;
                }*/
            }
            if (count == pattern.Length)
            {
                return true;
            }
            return false;
        }
        private bool CheckHorizontalRight(string pattern, Grid move)
        {
            int count = 0;
            if (move.indexX + pattern.Length - 1 >= _gameMap.GetLength(1))
                return false;
            for (int i = 0; i < pattern.Length; i++)
            {
                /*try
                {*/
                    if ((_gameMap[move.indexY, move.indexX + i].Text == "" && pattern[i] == ' ') ||
                    _gameMap[move.indexY, move.indexX + i].Text == pattern[i].ToString())
                    {
                        count++;
                    }
                /*}
                catch { return false; }*/
            }
            if (count == pattern.Length)
            {
                return true;
            }
            return false;
        }
        
        private bool CheckDiagonalyDownRigth(string pattern, Grid move)
        {
            int count = 0;
            if (move.indexX + pattern.Length - 1 >= _gameMap.GetLength(0) || move.indexY + (pattern.Length - 1) >= _gameMap.GetLength(1))
                return false;
            for (int i = 0; i < pattern.Length; i++)
            {
                /*try
                {*/
                    if ((_gameMap[move.indexY + i, move.indexX + i].Text == "" && pattern[i] == ' ') ||
                    _gameMap[move.indexY + i, move.indexX + i].Text == pattern[i].ToString())
                    {
                        count++;
                    }
                /*}
                catch { return false; }*/
            }
            if (count == pattern.Length)
            {
                return true;
            }
            return false;
        }
        private bool CheckDown(string pattern, Grid move)
        {
            int count = 0;
            if (move.indexY + (pattern.Length - 1) >= _gameMap.GetLength(1))
                return false;
            for (int i = 0; i < pattern.Length; i++)
            {
                //try
               // {
                    if ((_gameMap[move.indexY + i, move.indexX].Text == "" && pattern[i] == ' ') ||
                    _gameMap[move.indexY + i, move.indexX].Text == pattern[i].ToString())
                    {
                        count++;
                    }
                //}
                //catch { return false;}
            }
            if (count == pattern.Length)
            {
                return true;
            }
            return false;
        }
        private bool CheckDiagonalyDownLeft(string pattern, Grid move)
        {
            int count = 0;
            if (move.indexX - (pattern.Length - 1) >= _gameMap.GetLength(0) || move.indexX- (pattern.Length - 1) < 0 || move.indexY + (pattern.Length - 1) >= _gameMap.GetLength(1))
                return false;
            for (int i = 0; i < pattern.Length; i++)
            {
                /*try
                {*/
                    if ((_gameMap[move.indexY + i, move.indexX - i].Text == "" && pattern[i] == ' ') ||
                    _gameMap[move.indexY + i, move.indexX - i].Text == pattern[i].ToString())
                    {
                        count++;
                    }
                /*}
                catch (IndexOutOfRangeException e)
                {
                    return false;
                }*/
            }
            if (count == pattern.Length)
            {
                return true;
            }
            return false;
        }
        private bool CheckHorizontalLeft (string pattern, Grid move)
        {
            int count = 0;
            if (move.indexX - (pattern.Length - 1) >= _gameMap.GetLength(0) || move.indexX - pattern.Length - 1 <0)
                return false;
            for (int i = 0; i < pattern.Length; i++)
            {
                /*try
                {*/
                    if ((_gameMap[move.indexY, move.indexX - i].Text == "" && pattern[i] == ' ') ||
                    _gameMap[move.indexY, move.indexX - i].Text == pattern[i].ToString())
                    {
                        count++;
                    }
                /*}
                catch (IndexOutOfRangeException e)
                {
                    return false;
                }*/
            }
            if (count == pattern.Length)
            {
                return true;
            }
            return false;
        }
        private bool CheckDiagonalUplLeft(string pattern, Grid move)
        {
            int count = 0;
            if (move.indexX - (pattern.Length - 1) >= _gameMap.GetLength(0) || move.indexY - (pattern.Length - 1) >= _gameMap.GetLength(1) || move.indexX - pattern.Length - 1<0 || move.indexY - (pattern.Length - 1)<0)
                return false;
            for (int i = 0; i < pattern.Length; i++)
            {
                /*try
                {*/
                    if ((_gameMap[move.indexY-i, move.indexX - i].Text == "" && pattern[i] == ' ') ||
                    _gameMap[move.indexY-i, move.indexX - i].Text == pattern[i].ToString())
                    {
                        count++;
                    }
                /*}
                catch (IndexOutOfRangeException e)
                {
                    return false;
                }*/
            }
            if (count == pattern.Length)
            {
                return true;
            }
            return false;
        }
        private bool CheckUp(string pattern, Grid move)
        {
            int count = 0;
            if (move.indexY - (pattern.Length - 1) >= _gameMap.GetLength(1) || move.indexY - (pattern.Length - 1)<0)
                return false;
            for (int i = 0; i < pattern.Length; i++)
            {
                if ((_gameMap[move.indexY - i, move.indexX].Text == "" && pattern[i] == ' ') ||    //////ex
                    _gameMap[move.indexY - i, move.indexX].Text == pattern[i].ToString())
                {
                    count++;
                }
            }
            if (count == pattern.Length)
            {
                return true;
            }
            return false;
        }
        ////
        ///////////Методи для перевірки співпадіння з паттерном у всих напрямках від можливого ходу///////////
        private bool IsPossibleMove (Grid a)  //метод для оцінювання можливості супротивника/бота походити в цю клітинку
        {
            int j = a.indexX;
            int i = a.indexY;
            int hard = 1;
            if (i == 0 && j != 0 && j < 19)
            {
                if (_gameMap[i, j].Text == "" && (_gameMap[i, j + hard].Text != "" || _gameMap[i + hard, j].Text != "" ||
                _gameMap[i + hard, j + hard].Text != "" || _gameMap[i, j - hard].Text != "" ||
                _gameMap[i + hard, j + hard].Text != "" || _gameMap[i + hard, j - hard].Text != ""))
                {
                    return true;
                }
            }
            else if (i != 0 && j == 0 && i < 19)
            {
                if (_gameMap[i, j].Text == "" && (_gameMap[i, j + hard].Text != "" || _gameMap[i + hard, j].Text != "" ||
                _gameMap[i + hard, j + hard].Text != "" || _gameMap[i - hard, j + hard].Text != "" ||
                _gameMap[i + hard, j + hard].Text != "" || _gameMap[i - hard, j + hard].Text != "" ||
                _gameMap[i - hard, j].Text != ""))
                {
                    return true;
                }
            }
            else if (i == 0 && j == 0)
            {
                if (_gameMap[i, j].Text == "" && (_gameMap[i, j + hard].Text != "" || _gameMap[i + hard, j].Text != "" ||
                _gameMap[i + hard, j + hard].Text != "" || _gameMap[i + hard, j + hard].Text != ""))
                {
                    return true;
                }
            }
            else if (i == 19 && j < 19)
            {
                if (_gameMap[i, j].Text == "" && (_gameMap[i, j + hard].Text != "" ||
                _gameMap[i - hard, j + hard].Text != "" || _gameMap[i - hard, j + hard].Text != "" ||
                _gameMap[i - hard, j].Text != ""))
                {
                    return true;
                }
            }
            else if (j == 19 && i < 19)
            {
                if (_gameMap[i, j].Text == "" && (_gameMap[i + hard, j].Text != "" ||
                _gameMap[i, j - hard].Text != "" || _gameMap[i + hard, j - hard].Text != ""))
                {
                    return true;
                }
            }
            else if (i == 19 && j == 19)
            {
                if (_gameMap[i, j].Text == "" && (_gameMap[i, j - hard].Text != "" || _gameMap[i - hard, j - hard].Text != "" ||
                _gameMap[i - hard, j].Text != ""))
                {
                    return true;
                }
            }
            else
            {
                if (_gameMap[i, j].Text == "" && (_gameMap[i, j + hard].Text != "" || _gameMap[i + hard, j].Text != "" ||
                _gameMap[i + hard, j + hard].Text != "" || _gameMap[i, j - hard].Text != "" || _gameMap[i - hard, j - hard].Text != "" ||
                _gameMap[i - hard, j + hard].Text != "" || _gameMap[i + hard, j + hard].Text != "" || _gameMap[i - hard, j + hard].Text != "" ||
                _gameMap[i + hard, j - hard].Text != "" || _gameMap[i - hard, j].Text != ""))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
