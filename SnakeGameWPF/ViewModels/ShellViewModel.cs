using Caliburn.Micro;
using SnakeGameWPF.Models;
using System.Windows.Input;

namespace SnakeGameWPF.ViewModels
{
    class ShellViewModel : Conductor<object>
    {
        private PlayModeViewModel playModeViewModel;

        public ShellViewModel()
        {
            ChangeToMenu();
        }

        public void ChangeToMenu()
        {
            ActivateItem(new MenuViewModel(this));
        }

        public void ChangeToPlayMode(GameOptionsModel gameOptions)
        {
            playModeViewModel = new PlayModeViewModel(this, gameOptions);
            ActivateItem(playModeViewModel);
        }

        public void OnKeyInput(KeyEventArgs e)
        {
            playModeViewModel.OnKeyInput(e);
        }
    }
}
