using Snake.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum Direction
        {
            Left,
            Right,
            Up,
            Down,
        }

        const int snakeSize = 10;
        const int startPosition = 0;
        int delay = 500;
        Direction direction = Direction.Right;
        Feed feed = new Feed(snakeSize);

        SnakePart snakeHead = new SnakePart(snakeSize)
        {
            IsHead = true,
            PosX = startPosition,
            PosY = startPosition,
        };

        SnakePart snakePart1 = new SnakePart(snakeSize)
        {
            IsHead = false,
            PosX = startPosition - snakeSize,
            PosY = startPosition,
        };

        SnakePart snakePart2 = new SnakePart(snakeSize)
        {
            IsHead = false,
            PosX = startPosition - 2*snakeSize,
            PosY = startPosition,
        };

        List<SnakePart> snake = new List<SnakePart>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            GameField.Focus();
            snake.Add(snakeHead);
            snake.Add(snakePart1);
            snake.Add(snakePart2);
            RenderGame();
        }

        private async void RenderGame()
        {
            Canvas.SetLeft(snakeHead.Rectangle, snakeHead.PosX);
            Canvas.SetTop(snakeHead.Rectangle, snakeHead.PosY);
            GameField.Children.Add(snakeHead.Rectangle);

            GenerateFeed(snake);

            while (true)
            {
                for (int i = snake.Count - 1; i > 0; i--)
                {
                    snake[i].PosX = snake[i - 1].PosX;
                    snake[i].PosY = snake[i - 1].PosY;
                }

                await Task.Delay(delay);

                if (direction == Direction.Left)
                    snake[0].PosX -= snakeSize;
                else if (direction == Direction.Right)
                    snake[0].PosX += snakeSize;
                else if (direction == Direction.Up)
                    snake[0].PosY -= snakeSize;
                else if (direction == Direction.Down)
                    snake[0].PosY += snakeSize;


                for (int i = 0; i < snake.Count; i++)
                {
                    GameField.Children.Remove(snake[i].Rectangle);
                    Canvas.SetLeft(snake[i].Rectangle, snake[i].PosX);
                    Canvas.SetTop(snake[i].Rectangle, snake[i].PosY);
                    GameField.Children.Add(snake[i].Rectangle);

                }

                if (snake[0].PosX == feed.PosX && snake[0].PosY == feed.PosY)
                {
                    SnakePart snakePart = new SnakePart(snakeSize)
                    {
                        IsHead = false,
                        PosX = snake.Last().PosX,
                        PosY = snake.Last().PosY,
                    };
                    snake.Add(snakePart);

                    GameField.Children.Remove(feed.Rectangle);

                    GenerateFeed(snake);
                }

            }
        }

        private void GameField_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (direction != Direction.Right && e.Key == Key.Left)
            {
                direction = Direction.Left;
            }
            else if(direction != Direction.Left && e.Key == Key.Right)
            {
                direction = Direction.Right;
            }
            else if (direction != Direction.Down && e.Key == Key.Up)
            {
                direction = Direction.Up;
            }
            else if (direction != Direction.Up && e.Key == Key.Down)
            {
                direction = Direction.Down;
            }
        }

        private void GenerateFeed(List<SnakePart> snake)
        {
            SnakePart sp;
            do
            {
                Random random = new Random();
                feed.PosX = random.Next(0, 40) * 10;
                feed.PosY = random.Next(0, 50) * 10;
                sp = snake.Where(s => s.PosX == feed.PosX && s.PosY == feed.PosY).FirstOrDefault();
            }
            while (sp != null);

            Canvas.SetLeft(feed.Rectangle, feed.PosX);
            Canvas.SetTop(feed.Rectangle, feed.PosY);
            GameField.Children.Add(feed.Rectangle);
        }
    }
}
