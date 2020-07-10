using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snake.Common
{
    public class SnakePart
    {
        public SnakePart(int snakeSize)
        {
            Rectangle = new Rectangle();
            Rectangle.Width = snakeSize;
            Rectangle.Height = snakeSize;
            Rectangle.Fill = IsHead ? new SolidColorBrush(Colors.LightGreen)
                                    : new SolidColorBrush(Colors.Green);
        }

        public Rectangle Rectangle { get; set; }
        public int PosX { get; set; }

        public int PosY { get; set; }

        public bool IsHead { get; set; }
    }
}
