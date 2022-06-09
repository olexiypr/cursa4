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
    public partial class MenuForm : Form
    {
        string _gameMode;
        private GameForm _gameForm;
        public MenuForm()
        {
            InitializeComponent();
            Init();
        }
        
        private void Init ()
        {
            singleGameButton.Click += new EventHandler(OnClickSingleGameButton);//подія при натисканні на днопку "грати проти комп'ютера"
            twoPlayersButton.Click += new EventHandler(OnClickTwoPlayersButton);//подія при натисканні на днопку "грати вдвох"
        }
        
        private void OnClickTwoPlayersButton(object sender, EventArgs e) //метод при натисканні на днопку "грати вдвох"
        {
            _gameMode = "two ";
            if (timerCheckBox.Checked)
                _gameMode += "timer";
            this.Hide();
            _gameForm = new GameForm(_gameMode);
            Presenter presenter = new Presenter(_gameForm);
            _gameForm.Show();
        }

        private void OnClickSingleGameButton(object sender, EventArgs e)//метод при натисканні на днопку "грати проти комп'ютера"
        {
            _gameMode = "single ";
            if (timerCheckBox.Checked)
                _gameMode += "timer";
            this.Hide();
            _gameForm = new GameForm(_gameMode);
            Presenter presenter = new Presenter(_gameForm);
            _gameForm.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)//подія при закриванні форми
        {
            Application.Exit();
        }
    }
}
