using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cursachMVC
{
    internal interface IPlayer  //інтерфейс гравця
    {
        void Move();  //метод для ходу
        string move { get; set; }  //поле яке визначає за кого буде грати гравець
    }
}
