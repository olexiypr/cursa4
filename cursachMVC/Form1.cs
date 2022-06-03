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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Init();
        }
        string _gameMode;
        private void Init ()
        {
            singleGameButton.Click += new EventHandler(OnClickSingleGameButton);
            twoPlayersButton.Click += new EventHandler(OnClickTwoPlayersButton);
        }

        private void OnClickTwoPlayersButton(object sender, EventArgs e)
        {
            _gameMode = "two ";
            if (timerCheckBox.Checked)
                _gameMode += "timer";
            this.Hide();
            GameForm gameForm = new GameForm(_gameMode);
            Presenter presenter = new Presenter(gameForm);
            gameForm.ShowDialog();
        }

        private void OnClickSingleGameButton(object sender, EventArgs e)
        {
            _gameMode = "single ";
            if (timerCheckBox.Checked)
                _gameMode += "timer";
            this.Hide();
            GameForm gameForm = new GameForm(_gameMode);
            Presenter presenter = new Presenter(gameForm);
            gameForm.ShowDialog();
        }
    }
}
