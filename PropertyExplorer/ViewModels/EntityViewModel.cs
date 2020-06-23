using PropertyExplorer.Models;
using System.Collections.ObjectModel;

namespace PropertyExplorer.ViewModels
{
    public class EntityViewModel : BaseViewModel
    {
        public IEntity Entity { get; private set; }

        public ObservableCollection<PropertyCategory> Categories { get; } = new ObservableCollection<PropertyCategory>();

        public Property Width { get; }

        public Property Height { get; }

        public EntityViewModel(IEntity entity)
        {
            Entity = entity;

            Width = new DoubleProperty(nameof(Width),
                () => Entity.Width,
                w => Entity.Width = w);

            Height = new DoubleProperty(nameof(Height),
                () => Entity.Height,
                h => Entity.Height = h);

            PropertyCategory size = new PropertyCategory("size");
            size.Properties.Add(Width);
            size.Properties.Add(Height);

            Categories.Add(size);
        }
    }
}
