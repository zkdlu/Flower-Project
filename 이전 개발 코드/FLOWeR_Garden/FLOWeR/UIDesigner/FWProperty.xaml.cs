using FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Control;
using System.Windows;
using System.Windows.Controls;

namespace FLOWeR_Garden_Ver02.FLOWeR.UIDesigner
{
    /// <summary>
    /// FWProperty.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FWProperty : UserControl
    {
        public FWProperty()
        {
            InitializeComponent();
        }

        public Window DockedWindow
        {
            get;
            set;
        }

        public object TargetObject
        {
            get
            {
                return propertyGrid.SelectedObject;
            }
            set
            {
                propertyGrid.SelectedObject = value;
            }
        }
    }
}
