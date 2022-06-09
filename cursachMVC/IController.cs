using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cursachMVC
{
    internal interface IController  //інтерфейс для передавання і виведення даних з гри на форму
    {
        event EventHandler<EventArgs> OnGridClick;
        event EventHandler<EventArgs> OnStopTimer;
        string gameMode { get; }
        bool isFirstMove { get; set; }
        Grid [,] gameMap { get; }
        void EnabledButtons(bool status);
        void Reset();
        void StopTimer();

    }
}
