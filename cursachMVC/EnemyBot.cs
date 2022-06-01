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
        string hod { get; set; }
        string IPlayer.hod { get => hod; set => hod = value; }
        Grid[,] gameMap;
        public EnemyBot(Grid[,] gameMap)
        {
            hod = "X";
            this.gameMap = gameMap;

            patternsStrForX = new List<string>();
            patternsStrForO = new List<string>();
            patternsKey = new List<int>();
            FillPatterns();
            
        }
        private void FillPatterns() //заповнення шаблонів
        {
            patternsKey.Add(100000);
            patternsKey.Add(7000);
            patternsKey.Add(4000);
            patternsKey.Add(4000);
            patternsKey.Add(2000);
            patternsKey.Add(2000);
            patternsKey.Add(2000);
            patternsKey.Add(2000);
            patternsKey.Add(2000);
            patternsKey.Add(2000);
            patternsKey.Add(3000);
            patternsKey.Add(1500);
            patternsKey.Add(1500);
            patternsKey.Add(800);
            patternsKey.Add(800);
            patternsKey.Add(800);
            patternsKey.Add(800);
            patternsKey.Add(200);
            ////////////////////////////////
            patternsStrForX.Add("XXXXX");
            patternsStrForX.Add(" XXXX ");
            patternsStrForX.Add("XXXX ");
            patternsStrForX.Add(" XXXX");
            patternsStrForX.Add(" X XXX");
            patternsStrForX.Add(" XX XX");
            patternsStrForX.Add(" XXX X");
            patternsStrForX.Add("XXX X ");
            patternsStrForX.Add("XX XX ");
            patternsStrForX.Add("X XXX ");
            patternsStrForX.Add(" XXX ");
            patternsStrForX.Add(" XXX");
            patternsStrForX.Add("XXX ");
            patternsStrForX.Add(" XX X");
            patternsStrForX.Add("XX X ");
            patternsStrForX.Add(" X XX");
            patternsStrForX.Add("X XX ");
            patternsStrForX.Add(" XX ");
            ////////////////////////////
            foreach (var a in patternsStrForX)
            {
                patternsStrForO.Add(a.Replace('X', '0'));
            }
        }
        List<string> patternsStrForX;
        List<string> patternsStrForO;
        List<int> patternsKey;
        public void Hod()
        {
            List<Grid> possibleMoves = new List<Grid>();
            List<int> grades = new List<int>();
            for (int i = 1; i < gameMap.GetLength(0)-1 ; i++)
            {
                for (int j = 1; j < gameMap.GetLength(1)-1; j++)
                {

                    if (IsPossibleMove(gameMap[i, j]))
                    {
                        possibleMoves.Add(gameMap[i, j]);
                    }
                }
            }
            possibleMoves.ForEach(move =>
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
            for (int i=0; i< patternsStrForX.Count; i++)
            {   
                if (CheckDiagonalUpRight(patternsStrForX[i], move) || CheckHorizontalRight(patternsStrForX[i], move) || CheckDiagonalyDownRigth(patternsStrForX[i], move) || 
                    CheckDown(patternsStrForX[i], move) || CheckDiagonalyDownLeft(patternsStrForX[i], move) || CheckHorizontalLeft(patternsStrForX[i], move) ||
                    CheckDiagonalUplLeft(patternsStrForX[i], move) || CheckUp(patternsStrForX[i], move))
                {
                    grades.Add(patternsKey[i]);
                }
                else
                {
                    grades.Add(0);
                }
                if (CheckDiagonalUpRight(patternsStrForO[i], move) || CheckHorizontalRight(patternsStrForO[i], move) || CheckDiagonalyDownRigth(patternsStrForO[i], move) ||
                    CheckDown(patternsStrForO[i], move) || CheckDiagonalyDownLeft(patternsStrForO[i], move) || CheckHorizontalLeft(patternsStrForO[i], move) ||
                    CheckDiagonalUplLeft(patternsStrForO[i], move) || CheckUp(patternsStrForO[i], move))
                {
                    enemyGrades.Add(patternsKey[i]);
                }
                else
                {
                    enemyGrades.Add(0);
                }
            }
            if (grades.Max()>enemyGrades.Max())
                return grades.Max();
            return enemyGrades.Max();
        }
        private bool CheckDiagonalUpRight(string pattern, Grid move)
        {
            int count = 0;
            for (int i = 0; i < pattern.Length; i++)
            {
                if (move.indexX+pattern.Length-1 >=gameMap.GetLength(0) || move.indexY - (pattern.Length - 1) >= gameMap.GetLength(1) || move.indexY - (pattern.Length - 1)<0)
                    return false;
                /*try
                {*/
                    if ((gameMap[move.indexY - i, move.indexX + i].Text == "" && pattern[i] == ' ') ||   /////ex
                    gameMap[move.indexY - i, move.indexX + i].Text == pattern[i].ToString())
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
            if (move.indexX + pattern.Length - 1 >= gameMap.GetLength(1))
                return false;
            for (int i = 0; i < pattern.Length; i++)
            {
                /*try
                {*/
                    if ((gameMap[move.indexY, move.indexX + i].Text == "" && pattern[i] == ' ') ||
                    gameMap[move.indexY, move.indexX + i].Text == pattern[i].ToString())
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
            if (move.indexX + pattern.Length - 1 >= gameMap.GetLength(0) || move.indexY + (pattern.Length - 1) >= gameMap.GetLength(1))
                return false;
            for (int i = 0; i < pattern.Length; i++)
            {
                /*try
                {*/
                    if ((gameMap[move.indexY + i, move.indexX + i].Text == "" && pattern[i] == ' ') ||
                    gameMap[move.indexY + i, move.indexX + i].Text == pattern[i].ToString())
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
            if (move.indexY + (pattern.Length - 1) >= gameMap.GetLength(1))
                return false;
            for (int i = 0; i < pattern.Length; i++)
            {
                //try
               // {
                    if ((gameMap[move.indexY + i, move.indexX].Text == "" && pattern[i] == ' ') ||
                    gameMap[move.indexY + i, move.indexX].Text == pattern[i].ToString())
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
            if (move.indexX - (pattern.Length - 1) >= gameMap.GetLength(0) || move.indexX- (pattern.Length - 1) < 0 || move.indexY + (pattern.Length - 1) >= gameMap.GetLength(1))
                return false;
            for (int i = 0; i < pattern.Length; i++)
            {
                /*try
                {*/
                    if ((gameMap[move.indexY + i, move.indexX - i].Text == "" && pattern[i] == ' ') ||
                    gameMap[move.indexY + i, move.indexX - i].Text == pattern[i].ToString())
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
            if (move.indexX - (pattern.Length - 1) >= gameMap.GetLength(0) || move.indexX - pattern.Length - 1 <0)
                return false;
            for (int i = 0; i < pattern.Length; i++)
            {
                /*try
                {*/
                    if ((gameMap[move.indexY, move.indexX - i].Text == "" && pattern[i] == ' ') ||
                    gameMap[move.indexY, move.indexX - i].Text == pattern[i].ToString())
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
            if (move.indexX - (pattern.Length - 1) >= gameMap.GetLength(0) || move.indexY - (pattern.Length - 1) >= gameMap.GetLength(1) || move.indexX - pattern.Length - 1<0 || move.indexY - (pattern.Length - 1)<0)
                return false;
            for (int i = 0; i < pattern.Length; i++)
            {
                /*try
                {*/
                    if ((gameMap[move.indexY-i, move.indexX - i].Text == "" && pattern[i] == ' ') ||
                    gameMap[move.indexY-i, move.indexX - i].Text == pattern[i].ToString())
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
            if (move.indexY - (pattern.Length - 1) >= gameMap.GetLength(1) || move.indexY - (pattern.Length - 1)<0)
                return false;
            for (int i = 0; i < pattern.Length; i++)
            {
                if ((gameMap[move.indexY - i, move.indexX].Text == "" && pattern[i] == ' ') ||    //////ex
                    gameMap[move.indexY - i, move.indexX].Text == pattern[i].ToString())
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

        private bool IsPossibleMove (Grid a)
        {
            int j = a.indexX;
            int i = a.indexY;
            int hard = 1;
            try
            {
            if (gameMap[i, j].Text == "" && (gameMap[i, j + hard].Text != "" || gameMap[i + hard, j].Text != "" ||
                gameMap[i + hard, j + hard].Text != "" || gameMap[i, j - hard].Text != "" || gameMap[i - hard, j - hard].Text != "" ||
                gameMap[i - hard, j + hard].Text != "" || gameMap[i + hard, j + hard].Text != "" || gameMap[i - hard, j + hard].Text != "" || 
                gameMap[i + hard, j - hard].Text != "" || gameMap[i - hard, j].Text != ""))
                {
                    return true;
                }
            }
            catch (IndexOutOfRangeException e)
            {
                return true;
            }
            return false;
        }
    }
}
