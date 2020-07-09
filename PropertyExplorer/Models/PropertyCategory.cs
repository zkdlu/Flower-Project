using System.Collections.Generic;

namespace PropertyExplorer.Models
{
    public class PropertyCategory
    {
        public string Name { get; }

        public List<PropertySet> Properties { get; } = new List<PropertySet>();

        public PropertyCategory(string categoryName)
        {
            Name = categoryName;
        }
    }
}
