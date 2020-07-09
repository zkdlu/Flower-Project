using System;
using System.Windows;
using System.Windows.Forms;

namespace FLOWeR_Garden_Ver02
{
    /// <summary>
    /// ProjectWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NewProjectWindow : Window
    {
        public NewProjectWindow()
        {
            InitializeComponent();
        }

        public event MakeProjectEventHandler MakeProject = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tboProjectName.Focus();
        }

        private void OnClickProjectPath(object sender, RoutedEventArgs e)
        {
            using (var browserDialog = new FolderBrowserDialog())
            {
                if (browserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string projectPath = browserDialog.SelectedPath;

                    tboProjectPath.Text = projectPath;
                }
            }
        }

        private void OnClickCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnClickOk(object sender, RoutedEventArgs e)
        {
            int selectedIndex = lboProjectType.SelectedIndex;
            if (selectedIndex == -1)
            {
                return;
            }

            //Windows Forms App
            if (selectedIndex == 0)
            {
                string projectName = tboProjectName.Text;
                string projectPath = tboProjectPath.Text;

                if (string.IsNullOrWhiteSpace(projectName) || string.IsNullOrWhiteSpace(projectPath))
                {
                    System.Windows.MessageBox.Show("프로젝트 이름 또는 경로를 입력해주세요.");
                    return;
                }
                else if (!System.IO.Directory.Exists(projectPath))
                {
                    System.Windows.MessageBox.Show("유효하지 않은 경로입니다.");
                    return;
                }
                else if (int.TryParse(projectName.Substring(0, 1), out int result))
                {
                    System.Windows.MessageBox.Show("파일의 이름은 숫자로 시작할 수 없습니다.");
                    return;
                }

                //솔루션 탐색기에 프로젝트 생성
                if (MakeProject != null)
                {
                    MakeProject.Invoke(this, new MakeProjectEventArgs(projectName, projectPath));
                    this.Close();

                    return;
                }

                throw new Exception("프로젝트 탐색기에 이벤트 핸들러 미등록");
            }
        }
    }
}
