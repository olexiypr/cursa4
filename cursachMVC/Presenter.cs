using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cursachMVC
{
    internal class Presenter
    {
        public IController controller;
        public Game game;
        public Presenter(IController controller)
        {
            this.controller = controller;
            game = new Game(controller.mode, controller.map);
            controller.GridClick += new EventHandler<EventArgs>(GridClick);
        }

        private void GridClick(object sender, EventArgs e)
        {
            Grid grid = (Grid)sender;
            game.GridClick(sender, e);
            if (game.isWin)
            {
                controller.EnabledButtons(false);
            }
        }
    }
}
