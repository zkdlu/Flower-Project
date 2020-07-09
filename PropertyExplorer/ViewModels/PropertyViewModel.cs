using PropertyExplorer.Models;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace PropertyExplorer.ViewModels
{
    public class PropertyViewModel : BaseViewModel
    {
        public PropertyViewModel()
        {
            Mediator.PropertyViewModel = this;
            Categories = new ObservableCollection<PropertyCategory>();
        }

        public void SetEntityViewModel(EntityViewModel entityViewModel)
        {
            Categories = entityViewModel.Categories;
        }

        public ObservableCollection<PropertyCategory> Categories { get; set; }
    }
}
