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
        string mode;
        private void Init ()
        {
            singleGameButton.Click += new EventHandler(OnClickSingleGameButton);
            twoPlayersButton.Click += new EventHandler(OnClickTwoPlayersButton);
        }

        private void OnClickTwoPlayersButton(object sender, EventArgs e)
        {
            mode = "two ";
            if (timerCheckBox.Checked)
                mode += "timer";
            this.Hide();
            GameForm gameForm = new GameForm(mode);
            Presenter presenter = new Presenter(gameForm);
            gameForm.ShowDialog();
        }

        private void OnClickSingleGameButton(object sender, EventArgs e)
        {
            mode = "single ";
            if (timerCheckBox.Checked)
                mode += "timer";
            this.Hide();
            GameForm gameForm = new GameForm(mode);
            Presenter presenter = new Presenter(gameForm);
            gameForm.ShowDialog();
        }
    }
}
