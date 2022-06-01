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
            this.ControlBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            gameMode = mode;
            InitMap();
            if (gameMode == "two timer") //гра вдвох на одному компі з таймером
            {
                InitTimer();
            }
            else if (gameMode == "single timer")     //гра з ботом з таймером
            {
                InitTimer();
            }
            if (gameMode.IndexOf("r") == -1)
            {
                timer = new Timer();
            }
        }

        public Grid[,] gameMap { get; private set; }
        Size sizeGrid;
        Panel mapPanel;
        Panel controlsPanel;
        Button startButton;
        Timer timer;
        Label timeLabel;
        Button exit;
        public string gameMode { get; private set; }
        private bool isFirshHod;
        int sizeMap;
        private int countSecond;

        public event EventHandler<EventArgs> GridClick;
        public event EventHandler<EventArgs> OnStopTimer;
        //public event EventHandler<EventArgs> StartGame;
        //public Grid[,] map { get => gameMap; }
        //public string mode => gameMode;

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
                    gameMap[i, j].Location = new Point(sizeGrid.Width * j + 5, sizeGrid.Width * i + 5);
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
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = 1000;
            countSecond = 30;
        }
        
        private void TimerTick(object sender, EventArgs e)
        {
            timeLabel.Text = $"00 : {countSecond}";
            if (countSecond == 0)
            {
                OnStopTimer(sender, e);  
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
            EnabledButtons(true);
            if (gameMode.IndexOf(' ')!=-1)
                timer.Enabled = true;
            Reset();
            if (gameMode.IndexOf("single") != -1)
            {
                gameMap[sizeMap / 2, sizeMap / 2].Text = "X";
                gameMap[sizeMap / 2, sizeMap / 2].BackColor = Color.Green;
                EnabledButtons(false);
                DisableButtonsAfterHod(new Grid(sizeMap/2, sizeMap / 2));
                isFirshHod = false;
            }
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
            //MapExtension(grid);
            GridClick(sender, e);
        }
        private void MapExtension (Grid move)
        {
            if (move.indexX < 5 || move.indexY<5 || move.indexX > 15 || move.indexY > 15)
            {
                this.Width += sizeGrid.Width * 10;
                this.Height += sizeGrid.Width * 10;
                Grid[,] tempMap = gameMap;
                sizeMap += 8;
                mapPanel.Location = new Point((sizeGrid.Width + 5) * 4, (sizeGrid.Width + 5) * 4);
                mapPanel.Size = new Size((sizeGrid.Width + 5) * 28, (sizeGrid.Width + 5) * 28);
                gameMap = new Grid[sizeMap, sizeMap];
                for (int i = 0; i < gameMap.GetLength(0); i++)
                {
                    for (int j = 0; j < gameMap.GetLength(1); j++)
                    {
                        gameMap[i, j] = new Grid(i, j);
                        gameMap[i, j].Size = sizeGrid;
                        gameMap[i, j].Location = new Point(sizeGrid.Width * j + 5, sizeGrid.Width * i + 5);
                        gameMap[i, j].Parent = mapPanel;
                        gameMap[i, j].Click += new EventHandler(OnClickGrid);
                        gameMap[i, j].Enabled = false;
                    }
                }
                for (int i = 0; i < sizeMap; i++)
                {
                    for (int j = 0; j < sizeMap; j++)
                    {
                        /*gameMap[i, j].Location = new Point(gameMap[i, j].indexX += (sizeGrid.Width + 5) * 5, gameMap[i, j].indexY += (sizeGrid.Width + 5) * 5);*/
                        if (i < 20 || j< 20)
                            gameMap[i+sizeMap/2, j+sizeMap/2] = tempMap[i, j];
                    }
                }
                 
            }
        }
        private void DisableButtonsAfterHod(Grid grid)
        {
            for (int i = 0; i < sizeMap; i++)
            {
                for (int j = 0; j < sizeMap; j++)
                {
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
                    gameMap[i, j].BackColor = Color.White;
                }
            }
            countSecond = 30;
        }
        
    }
}
