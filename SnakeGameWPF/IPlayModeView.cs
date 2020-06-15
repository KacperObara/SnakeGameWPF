using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SnakeGameWPF
{
    // Gives PlayModeView access to UI elements for updating canvases and background images
    interface IPlayModeView
    {
        Canvas SnakeBoard { get; }

        void UpdateAppleBoard(UIElement element);
        void UpdateSnakeBoard(UIElement element);

        void RemoveAppleBoardElement(int index);       
        void RemoveSnakeBoardElement(int index);

        void UpdateBackgroundImage(BitmapImage bitmap);
    }
}
