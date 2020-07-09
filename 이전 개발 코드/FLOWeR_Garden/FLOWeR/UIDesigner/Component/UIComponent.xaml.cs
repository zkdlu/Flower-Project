using FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Control;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Component
{
    /// <summary>
    /// UIComponent.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UIComponent : UserControl, INotifyPropertyChanged
    {
        private static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(UIComponent), new FrameworkPropertyMetadata(false));

        private readonly double minHeight;
        private readonly double minWidth;

        private readonly double maxHeight;
        private readonly double maxWidth;

        private bool bFlag;
                
        public bool CanVResize
        {
            get;
            private set;
        }

        public bool CanHResize
        {
            get;
            private set;
        }

        public new object Content
        {
            get
            {
                return contentPresenter.Content;
            }
            protected set
            {
                contentPresenter.Content = value;
            }
        }

        public bool IsSelected
        {
            get
            {
                return (bool)GetValue(IsSelectedProperty);
            }
            set
            {
                SetValue(IsSelectedProperty, value);
            }
        }

        public FWControl FWControl
        {
            get;
            private set;
        }

        public UIComponent(FWControl fwControl)
        {
            InitializeComponent();

            Rectangle content = new Rectangle
            {
                Fill = Brushes.Thistle,
                Opacity = 0.5
            };

            controlText.DataContext = fwControl;

            //최소값 최대값 설정 확인
            minWidth = content.MinWidth < 10.0 ? 10.0 : content.MinWidth;
            minHeight = content.MinHeight < 10.0 ? 10.0 : content.MinHeight;
            maxWidth = content.MaxWidth;
            maxHeight = content.MaxHeight;

            CanHResize = true;
            CanVResize = true;

            SetComponentSize(fwControl);
            SetComponentPosition(fwControl);

            this.Content = content;
            this.FWControl = fwControl;

            fwControl.PropertyChanged += FwControl_PropertyChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FwControl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!bFlag)
            {
                SetComponentSize(FWControl);
                SetComponentPosition(FWControl);

                OnPropertyChanged(e.PropertyName);
            }
        }

        private void SetComponentPosition(FWControl fwControl)
        {
            SetValue(Canvas.TopProperty, fwControl.Top);
            SetValue(Canvas.LeftProperty, fwControl.Left);
        }

        private void SetComponentSize(FWControl fwControl)
        {
            this.Width = fwControl.Width;
            this.Height = fwControl.Height;
        }

        private void SelectionThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            FWControl.Left = (double)GetValue(Canvas.LeftProperty) + e.HorizontalChange;
            FWControl.Top = (double)GetValue(Canvas.TopProperty) + e.VerticalChange;

            SetComponentPosition(FWControl);
        }

        private void ResizeThumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            string name = ((Thumb)sender).Name;

            if (name.Contains("Top"))
            {
                double newHeight = this.Height - e.VerticalChange;
                if (newHeight >= minHeight && newHeight <= maxHeight)
                {
                    this.Height = newHeight;
                    SetValue(Canvas.TopProperty, (double)GetValue(Canvas.TopProperty) + e.VerticalChange);
                }
            }

            if (name.Contains("Right"))
            {
                double newWidth = this.Width + e.HorizontalChange;
                if (newWidth >= minWidth && newWidth <= maxWidth)
                {
                    this.Width = newWidth;
                }
            }

            if (name.Contains("Bottom"))
            {
                double newHeight = this.Height + e.VerticalChange;
                if (newHeight >= minHeight && newHeight <= maxHeight)
                {
                    this.Height = newHeight;
                }
            }

            if (name.Contains("Left"))
            {
                double newWidth = this.Width - e.HorizontalChange;
                if (newWidth >= minWidth && newWidth <= maxWidth)
                {
                    this.Width = newWidth;
                    SetValue(Canvas.LeftProperty, (double)GetValue(Canvas.LeftProperty) + e.HorizontalChange);
                }
            }

            FWControl.Width = this.Width;
            FWControl.Height = this.Height;
        }

        private void Grid_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UIManager.Instance.FlowerProperty.TargetObject = FWControl;
            UIManager.Instance.FlowerEvent.TargetObject = FWControl;

            IsSelected = true;
            bFlag = true;
        }

        private void Grid_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            bFlag = false;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);

            window.KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                UIManager.Instance.FlowerProperty.TargetObject = null;
                UIManager.Instance.FlowerEvent.TargetObject = null;

                UIManager.Instance.FlowerUIDesigner.DeleteControl(this);
            }
        }
    }
}
