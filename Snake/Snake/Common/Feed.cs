using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snake.Common
{
    public class Feed
    {
        public Feed(int snakeSize)
        {
            Rectangle = new Rectangle();
            Rectangle.Width = snakeSize;
            Rectangle.Height = snakeSize;
            Rectangle.Fill = new SolidColorBrush(Colors.Red);
        }
        public int PosX { get; set; }

        public int PosY { get; set; }

        public Rectangle Rectangle { get; set; }

    }
}
