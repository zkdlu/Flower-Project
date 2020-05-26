using Flower.Models;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace Flower.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public ComponentViewModel ComponentViewModel
        {
            get;
            set;
        }

        private ICommand unselectAllCommand;
        public ICommand UnSelectAllCommand
        {
            get
            {
                if (unselectAllCommand == null)
                {
                    unselectAllCommand = new RelayCommand(UnSelectAllComponent);
                }

                return unselectAllCommand;
            }
        }

        private void UnSelectAllComponent()
        {
            ComponentViewModel.IsSelected = false;
        }

        public MainViewModel()
        {
            ComponentViewModel = new ComponentViewModel(new Component());
        }
    }
}
