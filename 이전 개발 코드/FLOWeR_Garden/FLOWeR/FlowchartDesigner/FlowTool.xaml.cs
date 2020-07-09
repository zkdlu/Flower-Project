using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner.Symbol;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner
{
    /// <summary>
    /// FlowTool.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FlowTool : UserControl
    {
        public FlowTool()
        {
            InitializeComponent();

            SymbolList = new List<FWSymbol>();

            InitSymbolList();
            RefreshSymbol();
        }

        public Window DockedWindow
        {
            get;
            set;
        }

        public Type SelectedFWSymbol
        {
            get;
            private set;
        }

        public List<FWSymbol> SymbolList
        {
            get;
            private set;
        }

        public void Deselection()
        {
            SelectedFWSymbol = null;

            TreeViewItem treeViewItem = treeView.ItemContainerGenerator.ContainerFromIndex(0) as TreeViewItem;
            if (treeViewItem != null)
            {
                treeViewItem.IsSelected = true;
                treeViewItem.IsSelected = false;
            }
        }

        private void InitSymbolList()
        {
            SymbolList.Clear();

            SymbolList.Add(new FWTerminal());
            SymbolList.Add(new FWReady());
            //SymbolList.Add(new FWProcess());
            SymbolList.Add(new FWDecision());
            SymbolList.Add(new FWLine());
        }

        private void RefreshSymbol()
        {
            foreach (FWSymbol fwSymbol in SymbolList)
            {
                StackPanel stackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Tag = fwSymbol.GetType()
                };

                if (fwSymbol is FWTerminal)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        RadiusX = 10,
                        RadiusY = 10,
                        Width = 40,
                        Height = 20,
                        Fill = Brushes.LightBlue
                    };
                    stackPanel.Children.Add(rectangle);
                }
                else if (fwSymbol is FWReady)
                {
                    Polygon polygon = new Polygon
                    {
                        Points = new PointCollection(new Point[]
                        {
                            new Point(10,0),
                            new Point(30,0),
                            new Point(40,10),
                            new Point(30,20),
                            new Point(10,20),
                            new Point(0,10)
                        }),
                        Fill = Brushes.LightBlue
                    };
                    stackPanel.Children.Add(polygon);
                }
                else if (fwSymbol is FWProcess)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Width = 40,
                        Height = 20,
                        Fill = Brushes.LightBlue
                    };
                    stackPanel.Children.Add(rectangle);
                }
                else if (fwSymbol is FWDecision)
                {
                    Polygon polygon = new Polygon
                    {
                        Points = new PointCollection(new Point[]
                        {
                            new Point(20,0),
                            new Point(40,10),
                            new Point(20,20),
                            new Point(0,10)
                        }),
                        Fill = Brushes.LightBlue
                    };
                    stackPanel.Children.Add(polygon);
                }
                else if (fwSymbol is FWLine)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Width = 40,
                        Height = 2,
                        Fill = Brushes.LightBlue
                    };
                    Polygon polygon = new Polygon
                    {
                        Points = new PointCollection(new Point[]
                        {
                            new Point(-15, 0),
                            new Point(0,10),
                            new Point(-15, 20),
                        }),
                        Fill = Brushes.LightBlue
                    };
                    stackPanel.Children.Add(rectangle);
                    stackPanel.Children.Add(polygon);
                }

                TextBlock textBlock = new TextBlock
                {
                    Margin = new Thickness(10, 0, 0, 0),
                    Text = fwSymbol.Name
                };

                stackPanel.Children.Add(textBlock);
                treeHeader.Items.Add(stackPanel);
            }
        }

        private void FlowToolSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            object selectedNode = e.NewValue;

            if (selectedNode is StackPanel stackPanel)
            {
                object tag = stackPanel.Tag;
                if (tag == null)
                {
                    return;
                }

                SelectedFWSymbol = (Type)tag;

                FlowManager.Instance.ActionTool.DeselectListBox();
            }
        }

        private void TreeHeader_Collapsed(object sender, RoutedEventArgs e)
        {
            treeHeader.Items.Clear();

            RefreshSymbol();
        }

        private void TreeHeader_Selected(object sender, RoutedEventArgs e)
        {
            SelectedFWSymbol = null;

            treeHeader.IsSelected = false;
        }
    }
}
