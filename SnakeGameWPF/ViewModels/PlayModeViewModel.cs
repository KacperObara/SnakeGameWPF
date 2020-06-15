using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Caliburn.Micro;
using SnakeGameWPF.Models;

namespace SnakeGameWPF.ViewModels
{
    class PlayModeViewModel : PropertyChangedBase
    {
        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public IPlayModeView View;
        private BackgroundManager backgroundManager = new BackgroundManager();
        private ShellViewModel shellViewModel;
        private GameOptionsModel gameOptions;

        private Random random = new Random();

        // DispatcherPriority.Send as timer priority to avoid stuttering
        private DispatcherTimer tickTimer = new DispatcherTimer(DispatcherPriority.Send);

        private List<Vector> snakeBodyPositions = new List<Vector>();
        private List<Vector> applePositions = new List<Vector>();

        private readonly int stepSize = 5;

        private int Score = 0;

        private Vector headPosition;
        private Vector nextStep;
        private Direction lastDirection;
        private Vector startPoint = new Vector(100, 200);

        // Prevents the bug in which pressing 2 movement buttons at once allowed the snake to make 180 degree turn in place and die
        private bool movedOnTick = false;

        public PlayModeViewModel(ShellViewModel shellViewModel, GameOptionsModel gameOptions)
        {
            this.shellViewModel = shellViewModel;
            this.gameOptions = gameOptions;

            headPosition = startPoint;

            lastDirection = Direction.Right;
            nextStep.X = stepSize;

            tickTimer.Tick += new EventHandler(OnTimerTick);
            tickTimer.Interval = new TimeSpan((int)gameOptions.Difficulty);
            tickTimer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            movedOnTick = true;
            headPosition += nextStep;

            snakeBodyPositions.Add(headPosition);
            DrawNextMove(snakeBodyPositions[snakeBodyPositions.Count - 1]);

            if (CheckIfHitTail() || CheckIfLeftPlayArea())
            {
                OnGameOver();
            }

            if (CheckIfAteApple(out int appleId))
            {
                View.RemoveAppleBoardElement(appleId + 1);
                applePositions.RemoveAt(appleId);

                if (gameOptions.BackgroundsEnabled)
                {
                    BitmapImage bitmap = backgroundManager.ChooseRandomBitmap();
                    View.UpdateBackgroundImage(bitmap);
                }

                gameOptions.SnakeLength++;
                Score++;
            }

            // Removes the snake tail from the end
            if (View.SnakeBoard.Children.Count > gameOptions.SnakeLength)
            {
                View.RemoveSnakeBoardElement(0);
                snakeBodyPositions.RemoveAt(0);
            }

            if (applePositions.Count < gameOptions.AppleCount)
            {
                SpawnApple();
            }
        }

        private void SpawnApple()
        {
            Vector NewApple = new Vector(random.Next(30, 770), random.Next(30, 390));

            // Restrict positions to the grid
            NewApple.X -= NewApple.X % stepSize;
            NewApple.Y -= NewApple.Y % stepSize;

            applePositions.Add(NewApple);
            DrawApple(NewApple);
        }

        private void DrawNextMove(Vector bodyPart)
        {
            Rectangle rectangle = new Rectangle();

            rectangle.Fill = gameOptions.SnakeColor;
            rectangle.Width = stepSize;
            rectangle.Height = stepSize;

            Canvas.SetLeft(rectangle, bodyPart.X);
            Canvas.SetTop(rectangle, bodyPart.Y);

            View.UpdateSnakeBoard(rectangle);
        }

        private void DrawApple(Vector applePosition)
        {
            Ellipse ellipse = new Ellipse();

            ellipse.Fill = Brushes.Red;
            ellipse.Width = stepSize;
            ellipse.Height = stepSize;

            Canvas.SetLeft(ellipse, applePosition.X);
            Canvas.SetTop(ellipse, applePosition.Y);

            View.UpdateAppleBoard(ellipse);
        }

        private bool CheckIfAteApple(out int appleId)
        {
            for (int i = 0; i < applePositions.Count; i++)
            {
                if (CheckCollision(snakeBodyPositions[snakeBodyPositions.Count - 1], applePositions[i]))
                {
                    appleId = i;
                    return true;
                }
            }

            appleId = -1;
            return false;
        }

        private bool CheckIfLeftPlayArea()
        {
            if (headPosition.X < 0
             || headPosition.X > 525
             || headPosition.Y < 0
             || headPosition.Y > 410)
            {
                return true;
            }

            return false;
        }

        private bool CheckIfHitTail()
        {
            for (int i = snakeBodyPositions.Count - 2; i >= 0; --i)
            {
                if (CheckCollision(snakeBodyPositions[snakeBodyPositions.Count - 1], snakeBodyPositions[i]))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool CheckCollision(Vector v1, Vector v2)
        {
            if (v1.X == v2.X && v1.Y == v2.Y)
                return true;

            return false;
        }

        public void OnKeyInput(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                case Key.W:
                    if (lastDirection != Direction.Down && movedOnTick == true)
                    {
                        nextStep.X = 0;
                        nextStep.Y = -stepSize;
                        lastDirection = Direction.Up;
                        movedOnTick = false;
                    }
                    break;
                case Key.Down:
                case Key.S:
                    if (lastDirection != Direction.Up && movedOnTick == true)
                    {
                        nextStep.X = 0;
                        nextStep.Y = stepSize;
                        lastDirection = Direction.Down;
                        movedOnTick = true;
                    }
                    break;
                case Key.Left:
                case Key.A:
                    if (lastDirection != Direction.Right && movedOnTick == true)
                    {
                        nextStep.X = -stepSize;
                        nextStep.Y = 0;
                        lastDirection = Direction.Left;
                        movedOnTick = true;
                    }
                    break;
                case Key.Right:
                case Key.D:
                    if (lastDirection != Direction.Left && movedOnTick == true)
                    {
                        nextStep.X = stepSize;
                        nextStep.Y = 0;
                        lastDirection = Direction.Right;
                        movedOnTick = true;
                    }
                    break;
            }
        }

        private void OnGameOver()
        {
            tickTimer.Stop();

            if (Score == 1)
                MessageBox.Show("Game over, your scored 1 point!");
            else
                MessageBox.Show($"Game over, your scored {Score} points!");

            shellViewModel.ChangeToMenu();
        }
    }
}
