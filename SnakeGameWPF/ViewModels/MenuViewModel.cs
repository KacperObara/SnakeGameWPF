using Caliburn.Micro;
using SnakeGameWPF.Models;
using System.Windows.Media;

namespace SnakeGameWPF.ViewModels
{
    class MenuViewModel : PropertyChangedBase
    {
        #region properties
        private int _snakeLength;
        public int SnakeLength
        {
            get
            {
                return _snakeLength;
            }
            set
            {
                _snakeLength = value;
                NotifyOfPropertyChange(() => SnakeLength);
            }
        }

        private int _appleCount;
        public int AppleCount
        {
            get
            {
                return _appleCount;
            }
            set
            {
                _appleCount = value;
                NotifyOfPropertyChange(() => AppleCount);
            }
        }

        private Brush _snakeColor;
        public Brush SnakeColor
        {
            get
            {
                return _snakeColor;
            }
            set
            {
                _snakeColor = value;
                NotifyOfPropertyChange(() => SnakeColor);
            }
        }

        private bool _backgroundsEnabled;
        public bool BackgroundsEnabled
        {
            get
            {
                return _backgroundsEnabled;
            }
            set
            {
                _backgroundsEnabled = value;
                NotifyOfPropertyChange(() => BackgroundsEnabled);
            }
        }

        private Difficulty _difficulty;
        public Difficulty Difficulty
        {
            get
            {
                return _difficulty;
            }
            set
            {
                _difficulty = value;
                NotifyOfPropertyChange(() => Difficulty);
            }
        }
        #endregion

        private ShellViewModel shellViewModel;

        public MenuViewModel(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;

            Difficulty = Difficulty.Medium;
            AppleCount = 10;
            SnakeLength = 10;
        }

        public void OnStartButtonPressed()
        {
            GameOptionsModel gameOptions = new GameOptionsModel()
            {
                Difficulty = this.Difficulty,
                SnakeLength = this.SnakeLength,
                AppleCount = this.AppleCount,
                BackgroundsEnabled = this.BackgroundsEnabled,
                SnakeColor = Brushes.Green
            };

            shellViewModel.ChangeToPlayMode(gameOptions);
        }
    }
}
