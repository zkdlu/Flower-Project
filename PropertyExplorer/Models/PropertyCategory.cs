using System.Collections.Generic;

namespace PropertyExplorer.Models
{
    public class PropertyCategory
    {
        public string Name { get; }

        public List<Property> Properties { get; } = new List<Property>();

        public PropertyCategory(string categoryName)
        {
            Name = categoryName;
        }
    }
}
