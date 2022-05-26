using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cursachMVC
{
    public partial class GameForm : Form, IController
    {
        public GameForm(string mode)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            gameMode = mode;
            InitMap();
            if (gameMode.IndexOf(' ') != -1 && gameMode.IndexOf("two") != -1) //гра вдвох на одному компі з таймером
            {
                InitTimer();
            }
            else if (gameMode == "two ") //гра вдвох на одному компі без таймера
            {

            }
            else if (gameMode == "single ")  //гра з ботом без таймеру
            {

            }
            else  //гра з ботом з таймером
            {
                InitTimer();
            }
            if (gameMode.IndexOf("r") == -1)
            {
                timer = new Timer();
            }
        }

        Grid[,] gameMap;
        Size sizeGrid;
        Panel mapPanel;
        Panel controlsPanel;
        int sizeMap;
        Button startButton;
        private string gameMode;
        bool isPlaying;
        Timer timer;
        Label timeLabel;
        private bool isFirshHod;
        Button exit;
        private void InitMap ()
        {
            sizeGrid = new Size(30, 30);
            sizeMap = 20;
            mapPanel = new Panel ();
            controlsPanel = new Panel ();
            gameMap = new Grid[sizeMap, sizeMap];
            startButton = new Button();
            this.Width = (sizeGrid.Width + 10) * sizeMap;
            this.Height = (sizeGrid.Height) * sizeMap+50;
            mapPanel.Location = new Point(0, 0);
            mapPanel.Size = new Size(sizeGrid.Width*sizeMap+10, this.Height);
            mapPanel.Parent = this;
            /*controlsPanel.Location = new Point (mapPanel.Width, 0);
            controlsPanel.Size = new Size(50, this.Height);
            controlsPanel.Parent = this;*/
            //controlsPanel.BackColor = Color.Red;
            //mapPanel.BackColor = Color.Red;
            startButton.Text = "Почати гру";
            startButton.Size = new Size(80, 35);
            startButton.Location = new Point(mapPanel.Width+30, 30);
            startButton.Parent = this;
            startButton.Click += new EventHandler(OnStartButtonClick);
            exit = new Button();
            exit.Text = "Вийти з гри";
            exit.Size = new Size(80, 35);
            exit.Location = new Point(mapPanel.Width + 30, this.Height-160);
            exit.Parent = this;
            exit.Click += new EventHandler(OnExit);
            for (int i = 0; i < sizeMap; i++)
            {
                for (int j = 0; j < sizeMap; j++)
                {
                    gameMap[i, j] = new Grid(i,j);
                    gameMap[i, j].Size = sizeGrid;
                    gameMap[i, j].Location = new Point(sizeGrid.Width * i + 5, sizeGrid.Width * j + 5);
                    gameMap[i, j].Parent = mapPanel;
                    gameMap[i, j].Click += new EventHandler(OnClickGrid);
                    gameMap[i, j].Enabled = false;
                }
            }
        }

        public void OnExit(object sender, EventArgs e)
        {
           Application.Exit();
        }

        private void InitTimer ()
        {
            timer = new Timer();
            timeLabel = new Label();
            timeLabel.Size = new Size(this.Width - mapPanel.Width - 30, 60);
            timeLabel.Location = new Point (mapPanel.Width + 20, 140);
            timeLabel.Parent = this;
            timeLabel.Font = new Font(FontFamily.GenericSansSerif, 25, FontStyle.Regular);
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = 1000;
            countSecond = 20;
        }
        private int countSecond;
        private void timerTick(object sender, EventArgs e)
        {
            timeLabel.Text = $"00 : {countSecond}";
            if (countSecond == 0)
            {
                OnStopTimer(sender, e);
                isPlaying = false;
                EnabledButtons(false);
            }
            countSecond--;
        }
        public void StopTimer ()
        {
            timer.Enabled = false;
        }
        private void OnStartButtonClick(object sender, EventArgs e)
        {
            isFirshHod = true;
            Button grid = (Button)sender;
            //grid.Enabled = false;
            EnabledButtons(true);
            isPlaying = true;
            if (gameMode.IndexOf(' ')!=-1)
            timer.Enabled = true;
            Reset();
            //StartGame(sender, e);
        }

        private void OnClickGrid(object sender, EventArgs e)
        {
            Grid grid = (Grid)sender;
            if (isFirshHod)
            {
                EnabledButtons(false);
                isFirshHod=false;
            }
            DisableButtonsAfterHod(grid);
            GridClick(sender, e);
        }
        private void DisableButtonsAfterHod(Grid grid)
        {
            for (int i = 0; i < sizeMap; i++)
            {
                for (int j = 0; j < sizeMap; j++)
                {
                    //if (i < grid.indexY-5 || i> grid.indexY + 5 || j < grid.indexX - 5 || j > grid.indexX + 5)
                        //if (gameMap[i,j].Enabled)
                    if (i >= grid.indexY - 5 && i <= grid.indexY + 5 && j >= grid.indexX - 5 && j <= grid.indexX + 5 && gameMap[i, j].Text == "")
                        gameMap[i, j].Enabled = true;
                }
            }
            grid.Enabled = false;
        }
        public void EnabledButtons(bool status)
        {
            for (int i = 0; i < sizeMap; i++)
            {
                for (int j = 0; j < sizeMap; j++)
                {
                    gameMap[i, j].Enabled = status;
                }
            }
        }
        public void Reset ()
        {
            for (int i = 0; i < sizeMap; i++)
            {
                for (int j = 0; j < sizeMap; j++)
                {
                    gameMap[i, j].Text = "";
                }
            }            
        }
        
        public event EventHandler<EventArgs> GridClick;
        public event EventHandler<EventArgs> StartGame;
        public event EventHandler<EventArgs> OnStopTimer;

        public void Hod(Grid grid)
        {
            gameMap[grid.indexY, grid.indexX].Text = "";
        }

        /*public void Hod()
        {
            
        }*/
        /*public string hod { get => hod;
            set
            {
                hod = value;
            } }*/
        public Grid[,] map { get => gameMap; }

        public string mode => gameMode;
    }
}
