using PropertyExplorer.Models;
using System.Collections.ObjectModel;

namespace PropertyExplorer.ViewModels
{
    class PropertyViewModel : BaseViewModel
    {
        public ObservableCollection<PropertyCategory> Categories { get; } = Mediator.SelectedEntityViewModel.Categories;
    }
}
