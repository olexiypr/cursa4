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
        event EventHandler<EventArgs> StartGame;
        //void Hod();
        //string hod { get; set; }
        string mode { get; }
        Grid [,] map { get; }
        void EnabledButtons(bool status);
        void Reset();
    }
}
