using FLOWeR_Garden_Ver02.DockablePane;
using FLOWeR_Garden_Ver02.Explorer;
using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner;
using FLOWeR_Garden_Ver02.FLOWeR.Interpreter;
using FLOWeR_Garden_Ver02.FLOWeR.UIDesigner;
using System.IO;
using System.Windows;

namespace FLOWeR_Garden_Ver02
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window, IQuit
    {
        public MainWindow()
        {
            System.Threading.Thread.Sleep(1000);

            Style = (Style)FindResource(typeof(Window));

            InitializeComponent();
        }

        private DockPane _mainDockPane = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //메인 독킹 패널
            DockPane dockPane = new DockPane();
            mainPanel.Children.Add(dockPane);
            dockPane.ParentWindow = this;

            //메인 독킹 패널 설정
            DockManager.Instance.RegistDockPane(dockPane);
            _mainDockPane = dockPane;
            DockManager.Instance.MainDockPane = _mainDockPane;

            //기본 창 생성
            InitDefaultWindow();

            //DebugMode();
        }

        private void DebugMode()
        {
            //UI 디자이너 생성
            UIManager.Instance.ShowUIDesigner(_mainDockPane);
            UIManager.Instance.ShowToolBox(_mainDockPane);
            UIManager.Instance.ShowPropertyGrid(_mainDockPane);
            UIManager.Instance.ShowEventGrid(_mainDockPane);

            //순서도 생성
            FlowManager.Instance.ShowFlowTool(_mainDockPane);
            //액션 생성
            FlowManager.Instance.ShowActionWindow(_mainDockPane);
            FlowManager.Instance.ShowActionTool(_mainDockPane);

            FlowchartDesigner flowchartDesigner = new FlowchartDesigner("Name", "Value");
            FlowManager.Instance.ShowFlowDesigner(flowchartDesigner);
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void InitDefaultWindow()
        {
            //프로젝트 탐색기
            ProjectManager.Instance.ProjectExplorerWindow = new ProjectExplorerWindow();
            ProjectManager.Instance.ProjectExplorerWindow.Show();
            //강제 도킹
            DockManager.Instance.ForceDocking(_mainDockPane, ProjectManager.Instance.ProjectExplorerWindow, EDock.Right);
        }

        public void RemoveSelf(DockPane dockPane)
        {
        }

        private void OnNewProject(object sender, RoutedEventArgs e)
        {
            NewProjectWindow newProjectWindow = new NewProjectWindow();
            newProjectWindow.MakeProject += NewProjectWindow_MakeProject;
            newProjectWindow.ShowDialog();
        }

        private void NewProjectWindow_MakeProject(object sender, MakeProjectEventArgs e)
        {
            //프로젝트 탐색기에 프로젝트 추가
            if (ProjectManager.Instance.MakeProject(e.ProjectName, e.ProjectPath))
            {
                //UI 디자이너 생성
                UIManager.Instance.ShowUIDesigner(_mainDockPane);
                UIManager.Instance.ShowToolBox(_mainDockPane);
                UIManager.Instance.ShowPropertyGrid(_mainDockPane);
                UIManager.Instance.ShowEventGrid(_mainDockPane);

                //순서도 생성
                FlowManager.Instance.ShowFlowTool(_mainDockPane);

                //액션 생성
                FlowManager.Instance.ShowActionWindow(_mainDockPane);
                FlowManager.Instance.ShowActionTool(_mainDockPane);
            }
        }

        private void OnLoadProjectExplorer(object sender, RoutedEventArgs e)
        {
            if (ProjectManager.Instance.ProjectExplorerWindow == null)
            {
                //프로젝트 탐색기
                ProjectManager.Instance.ProjectExplorerWindow = new ProjectExplorerWindow();
                ProjectManager.Instance.ProjectExplorerWindow.Show();
                //강제 도킹
                DockManager.Instance.ForceDocking(_mainDockPane, ProjectManager.Instance.ProjectExplorerWindow, EDock.Right);
            }
        }

        //private void OnLoadUIDesigner(object sender, RoutedEventArgs e)
        //{
        //    if (UIManager.Instance.FlowerUIDesigner == null)
        //    {
        //        UIWindow uiWindow = new UIWindow();
        //        uiWindow.Show();
        //        DockManager.Instance.ForceDocking(_mainDockPane, uiWindow, EDock.Center);

        //        return;
        //    }
        //}

        private void OnLoadToolBox(object sender, RoutedEventArgs e)
        {

        }

        private void OnCompile(object sender, RoutedEventArgs e)
        {
            if (ProjectManager.Instance.ProjectPath == null ||
                    ProjectManager.Instance.ProjectName == null)
            {
                return;
            }

            FlowerInterpreter.Instance.GenerateMain();
            FlowerInterpreter.Instance.GenerateForm();
            FlowerInterpreter.Instance.GenerateFormDesigner();

            FlowerInterpreter.Instance.CompileFlower();

            ExecuteProgram();
        }

        private void ExecuteProgram()
        {
            System.Diagnostics.Process.Start(string.Format("{0}/{1}/Flower/{1}.exe", 
                ProjectManager.Instance.ProjectPath, ProjectManager.Instance.ProjectName));
        }
    }
}
