using SnakeGameWPF.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SnakeGameWPF.Views
{
    public partial class PlayModeView : UserControl, IPlayModeView
    {
        public Canvas SnakeBoard
        {
            get { return SnakeCanvas; }
        }

        public PlayModeView()
        {
            InitializeComponent();
            
            this.Loaded += new RoutedEventHandler(OnLoad);
        }

        void OnLoad(object sender, RoutedEventArgs e)
        {
            (DataContext as PlayModeViewModel).View = this as IPlayModeView;
        }

        public void UpdateAppleBoard(UIElement element)
        {
            AppleCanvas.Children.Add(element);
        }

        public void RemoveAppleBoardElement(int index)
        {
            AppleCanvas.Children.RemoveAt(index);
        }

        public void UpdateSnakeBoard(UIElement element)
        {
            SnakeCanvas.Children.Add(element);
        }

        public void RemoveSnakeBoardElement(int index)
        {
            SnakeCanvas.Children.RemoveAt(index);
        }

        public void UpdateBackgroundImage(BitmapImage bitmap)
        {
            BackgroundImage.ImageSource = bitmap;
        }
    }
}
