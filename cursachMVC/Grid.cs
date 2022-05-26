using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cursachMVC
{
    public class Grid : Button
    {
        public Button button;
        public int indexX;
        public int indexY;
        public Grid(int indexY, int indexX)
        {
            button = new Button();
            button.Size = new Size(25, 25);
            this.indexX = indexX;
            this.indexY = indexY;
        }
    }
}
