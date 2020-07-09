using FLOWeR_Garden_Ver02.FLOWeR.UIDesigner;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FLOWeR_Garden_Ver02.Explorer
{
    /// <summary>
    /// ProjectExplorer.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProjectExplorer : UserControl
    {
        public ProjectExplorer()
        {
            InitializeComponent();
        }

        public void RefreshProject()
        {
            treeView.Items.Clear();

            string projectName = ProjectManager.Instance.ProjectName;
            string projectPath = ProjectManager.Instance.ProjectPath;

            TreeViewItem newProjectItem = new TreeViewItem
            {
                Header = projectName,
                IsExpanded = true
            };
            TreeViewItem uiDesignerItem = NavigateDirectory(
                    new DirectoryInfo(
                        string.Format("{0}/{1}/{2}", projectPath, projectName, "UI Designer")));

            TreeViewItem flowchartDesigner = NavigateDirectory(
                    new DirectoryInfo(
                        string.Format("{0}/{1}/{2}", projectPath, projectName, "Flowchart Designer")));

            newProjectItem.Items.Add(uiDesignerItem);
            newProjectItem.Items.Add(flowchartDesigner);

            flowchartDesigner.MouseDoubleClick += FlowchartDesigner_MouseDoubleClick;
                        
            MenuItem menuLoad = new MenuItem
            {
                Header = "Load Image"
            };
            menuLoad.Click += MenuLoad_Click;

            ContextMenu contextMenu = new ContextMenu();
            contextMenu.Items.Add(menuLoad);

            uiDesignerItem.ContextMenu = contextMenu;
            uiDesignerItem.MouseDoubleClick += UiDesignerItem_MouseDoubleClick;

            treeView.Items.Add(newProjectItem);            
        }

        private void FlowchartDesigner_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = treeView.SelectedItem as TreeViewItem;

            string header = treeViewItem.Header.ToString();
            if (header.Contains(".xml"))
            {
                //창이 닫혀 있으면, 새로 열려야 하고, 
                //창이 열려 있으면 그대로.
            }
        }

        private TreeViewItem NavigateDirectory(DirectoryInfo directoryInfo)
        {
            TreeViewItem directoryNode = new TreeViewItem
            {
                Header = directoryInfo.Name,
                IsExpanded = true
            };

            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                TreeViewItem fileNode = new TreeViewItem { Header = fileInfo.Name, IsExpanded = false };
                directoryNode.Items.Add(fileNode);
            }

            foreach (DirectoryInfo dirInfo in directoryInfo.GetDirectories())
            {
                TreeViewItem dirNode = NavigateDirectory(dirInfo);
                directoryNode.Items.Add(dirNode);
            }

            return directoryNode;
        }

        private void UiDesignerItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //To do
            /*
             * UI 디자이너 창이 닫혀 있다면 다시 재 생성, 기존의 정보를 가지고 있어야 한다.
             */
        }

        private void MenuLoad_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };

            bool? result = fileDialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                string fileName = fileDialog.FileName;

                Uri uri = new Uri(fileName, UriKind.RelativeOrAbsolute);
                BitmapImage bitmapImage = new BitmapImage(uri);

                UIManager.Instance.ChangeBackgroundImage(bitmapImage);

                string projectName = ProjectManager.Instance.ProjectName;
                string projectPath = ProjectManager.Instance.ProjectPath;

                string imagePath = string.Format("{0}/{1}/{2}/{3}",
                    projectPath, projectName, "UI Designer", fileDialog.SafeFileName);

                File.Copy(fileName, imagePath);

                UIManager.Instance.BackgroundImage = imagePath;

                RefreshProject();
            }
        }
    }
}
