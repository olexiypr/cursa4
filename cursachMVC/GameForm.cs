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
    public partial class GameForm : Form, IController  //форма де відбувається гра
    {
        public GameForm(string mode)
        {
            InitializeComponent();
            this.ControlBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            gameMode = mode;
            InitMap();
            if (gameMode == "two timer" || gameMode == "single timer") //гра вдвох на одному компі з таймером
            {
                InitTimer();
            }
        }
        public Grid[,] gameMap { get; private set; }  //ігрове поле
        private Size _sizeGrid;  //розмір клітинки поля
        private Panel _mapPanel;  //панель на якій розміщується поле
        private Button _startButton; //кнопка для запуску гри
        private Timer _timer;  //таймер
        private Label _timeLabel;  //поле для виведення часу
        private Button _exitButton; //кнопка виходу з програми

        //ігровий режим (людина проти комп'ютера, людина проти комп'ютера з таймером,
        //людина проти людиин, людина проти людиин з таймером)
        public string gameMode { get; private set; }  
        private bool _isFirshHod;  //змінна для визначення чи був здійснений перший хід
        private int _sizeMap;  //розмір ігрового поля
        private int _countSecond;  //кількість секунд таймера
        public event EventHandler<EventArgs> OnGridClick;  //подія на натискання на игрове поле
        public event EventHandler<EventArgs> OnStopTimer;  //подія зупинки таймера
        private void InitMap ()  //створення ігрового поля і інтерфейсу
        {
            _sizeGrid = new Size(30, 30);
            _sizeMap = 20;
            _mapPanel = new Panel ();
            gameMap = new Grid[_sizeMap, _sizeMap];
            _startButton = new Button();
            this.Width = (_sizeGrid.Width + 10) * _sizeMap;
            this.Height = (_sizeGrid.Height) * _sizeMap+50;
            _mapPanel.Location = new Point(0, 0);
            _mapPanel.Size = new Size(_sizeGrid.Width*_sizeMap+10, this.Height);
            _mapPanel.Parent = this;
            _startButton.Text = "Почати гру";
            _startButton.Size = new Size(80, 35);
            _startButton.Location = new Point(_mapPanel.Width+30, 30);
            _startButton.Parent = this;
            _startButton.Click += new EventHandler(OnStartButtonClick);
            _exitButton = new Button();
            _exitButton.Text = "Вийти з гри";
            _exitButton.Size = new Size(80, 35);
            _exitButton.Location = new Point(_mapPanel.Width + 30, this.Height-160);
            _exitButton.Parent = this;
            _exitButton.Click += new EventHandler(OnExit);
            for (int i = 0; i < _sizeMap; i++)
            {
                for (int j = 0; j < _sizeMap; j++)
                {
                    gameMap[i, j] = new Grid(i,j);
                    gameMap[i, j].Size = _sizeGrid;
                    gameMap[i, j].Location = new Point(_sizeGrid.Width * j + 5, _sizeGrid.Width * i + 5);
                    gameMap[i, j].Parent = _mapPanel;
                    gameMap[i, j].Click += new EventHandler(OnClickGrid);
                    gameMap[i, j].Enabled = false;
                }
            }
        }

        public void OnExit(object sender, EventArgs e)  //закриття програми
        {
           Application.Exit();
        }

        private void InitTimer ()  //створення таймера
        {
            _timer = new Timer();
            _timeLabel = new Label();
            _timeLabel.Size = new Size(this.Width - _mapPanel.Width - 30, 60);
            _timeLabel.Location = new Point (_mapPanel.Width + 20, 140);
            _timeLabel.Parent = this;
            _timeLabel.Font = new Font(FontFamily.GenericSansSerif, 25, FontStyle.Regular);
            _timer.Tick += new EventHandler(TimerTick);
            _timer.Interval = 1000;
            _countSecond = 30;
        }
        
        private void TimerTick(object sender, EventArgs e)  //виведення часу таймера
        {
            _timeLabel.Text = $"00 : {_countSecond}";
            if (_countSecond == 0)
            {
                OnStopTimer(sender, e);  
                EnabledButtons(false);
            }
            _countSecond--;
        }
        public void StopTimer ()  //зупинка таймера
        {
            _timer.Enabled = false;
        }
        private void OnStartButtonClick(object sender, EventArgs e) //початок гри
        {
            _isFirshHod = true;
            Button grid = (Button)sender;
            EnabledButtons(true);
            if (gameMode.IndexOf("timer") !=-1)
                _timer.Enabled = true;
            Reset();
            if (gameMode.IndexOf("single") != -1)
            {
                gameMap[_sizeMap / 2, _sizeMap / 2].Text = "X";
                gameMap[_sizeMap / 2, _sizeMap / 2].BackColor = Color.Green;
                EnabledButtons(false);
                DisableButtonsAfterMove(new Grid(_sizeMap/2, _sizeMap / 2));
                _isFirshHod = false;
            }
        }
        private void OnClickGrid(object sender, EventArgs e)  //дія при натисканні на клітинку ігрового поля
        {
            Grid grid = (Grid)sender;
            if (_isFirshHod)
            {
                EnabledButtons(false);
                _isFirshHod = false;
            }
            DisableButtonsAfterMove(grid);
            OnGridClick(sender, e);
        }
        private void DisableButtonsAfterMove(Grid grid)  //обмеження доступних клітинок для ходу
        {
            for (int i = 0; i < _sizeMap; i++)
            {
                for (int j = 0; j < _sizeMap; j++)
                {
                    if (i >= grid.indexY - 5 && i <= grid.indexY + 5 && j >= grid.indexX - 5 && j <= grid.indexX + 5 && gameMap[i, j].Text == "")
                        gameMap[i, j].Enabled = true;
                }
            }
            grid.Enabled = false;
        }
        public void EnabledButtons(bool status)  //вимкнення або увімкнення всіх кнопок 
        {
            for (int i = 0; i < _sizeMap; i++)
            {
                for (int j = 0; j < _sizeMap; j++)
                {
                    gameMap[i, j].Enabled = status;
                }
            }
        }
        public void Reset ()  //очищення ігрового поля для початку гри заново 
        {
            for (int i = 0; i < _sizeMap; i++)
            {
                for (int j = 0; j < _sizeMap; j++)
                {
                    gameMap[i, j].Text = "";
                    gameMap[i, j].BackColor = Color.White;
                }
            }
            _countSecond = 30;
        }
        
    }
}
