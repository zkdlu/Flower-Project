using FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Control;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FLOWeR_Garden_Ver02.FLOWeR.UIDesigner
{
    /// <summary>
    /// ToolBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ToolBox : UserControl
    {
        public ToolBox()
        {
            InitializeComponent();

            ControlList = new List<FWControl>();

            InitControlList();
            RefreshControl();
        }
        
        public Window DockedWindow
        {
            get;
            set;
        }

        public Type SelectedFWControl
        {
            get;
            private set;
        }

        public List<FWControl> ControlList
        {
            get;
            private set;
        }

        public void Deselection()
        {
            if (treeView.SelectedItem is TreeViewItem treeViewItem)
            {
                treeViewItem.IsSelected = false;
            }

            SelectedFWControl = null;
        }

        private void InitControlList()
        {
            ControlList.Clear();

            ControlList.Add(new FWButton());
            ControlList.Add(new FWTextBox());
            ControlList.Add(new FWListBox());
        }

        private void RefreshControl()
        {
            foreach (FWControl fwControl in ControlList)
            {
                TextBlock textBlock = new TextBlock
                {
                    Text = fwControl.TypeName,
                    Tag = fwControl.GetType()
                };

                TreeViewItem item = new TreeViewItem
                {
                    Header = textBlock.Text,
                    Tag = textBlock
                };

                treeHeader.Items.Add(item);
            }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            object selectedNode = e.NewValue;

            if (selectedNode is TreeViewItem treeViewItem)
            {
                if (treeViewItem.Tag is TextBlock textBlock)
                {
                    object tag = textBlock.Tag;
                    if (tag == null)
                    {
                        return;
                    }

                    SelectedFWControl = (Type)tag;
                }
            }
        }

        private void TreeHeader_Collapsed(object sender, RoutedEventArgs e)
        {
            treeHeader.Items.Clear();

            RefreshControl();
        }

        private void TreeHeader_Selected(object sender, RoutedEventArgs e)
        {
            SelectedFWControl = null;

            treeHeader.IsSelected = false;
        }
    }
}
