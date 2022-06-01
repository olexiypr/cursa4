using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cursachMVC
{
    internal interface IController
    {
        event EventHandler<EventArgs> GridClick;
        //event EventHandler<EventArgs> StartGame;
        event EventHandler<EventArgs> OnStopTimer;
        //void Hod();
        //string hod { get; set; }
        string gameMode { get; }
        Grid [,] gameMap { get; }
        void EnabledButtons(bool status);
        void Reset();
        void StopTimer();
    }
}
