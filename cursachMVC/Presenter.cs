using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cursachMVC
{
    internal class Presenter  //клас для передавання подій з форми в гру, та передавання даних для виведення на форму з гри
    {
        public IController controller;  //змінна яка забезпечує доступ до подій і змінних форми
        public Game game;
        public Presenter(IController controller)
        {
            this.controller = controller;
            game = new Game(controller.gameMode, controller.gameMap);
            controller.OnGridClick += new EventHandler<EventArgs>(GridClick);
            controller.OnStopTimer += new EventHandler<EventArgs>(StopTimer);
            game.isPlaying = true;
        }
        //в цьому методі відбувається обробка результату натискання на кнопку ігрового поля та передачі в форму
        //для виведення на екран
        private void GridClick(object sender, EventArgs e)
        {
            Grid grid = (Grid)sender;
            game.GridClick(sender, e);
            if (game.isWin)
            {
                controller.EnabledButtons(false);
                game.isWin = false;
            }
        }
        //у цьому методі обробляється зупинка таймера
        private void StopTimer (object sender, EventArgs e)
        {
            game.isPlaying = false;
            controller.StopTimer();
            game.CountPoints();  //метод підрахунку кількості балів
        }
    }
}
