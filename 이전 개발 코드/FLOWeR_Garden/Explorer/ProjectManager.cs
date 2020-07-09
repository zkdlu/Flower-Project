using FLOWeR_Garden_Ver02.FLOWeR;
using FLOWeR_Garden_Ver02.FLOWeR.FlowchartDesigner;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace FLOWeR_Garden_Ver02.Explorer
{
    //생성한 프로젝트들을 관리 한다. 그러면 이놈이 프로젝트 탐색기도 관리 하자.
    public class ProjectManager
    {
        public static ProjectManager Instance
        {
            get;
            private set;
        }
        static ProjectManager()
        {
            Instance = new ProjectManager();
        }
        private ProjectManager()
        {
            FlowerManagerList = new List<FlowerManager>();
        }

        public List<FlowerManager> FlowerManagerList
        {
            get;
            private set;
        }

        public ProjectExplorerWindow ProjectExplorerWindow
        {
            get;
            set;
        }

        public string ProjectName
        {
            get;
            private set;
        }

        public string ProjectPath
        {
            get;
            private set;
        }

        public bool MakeProject(string projectName, string projectPath)
        {
            projectName = projectName.Replace(' ', '_')
               .Replace("\\", "")
               .Replace("/", "")
               .Replace("?", "")
               .Replace("\"", "")
               .Replace("<", "")
               .Replace(">", "")
               .Replace("|", "")
               .Replace(":", "");

            ProjectName = projectName;
            ProjectPath = projectPath;

            if (CreateDirectory(projectPath, projectName))
            {
                ProjectExplorerWindow.ProjectExplorer.RefreshProject();

                return true;
            }

            return false;
        }

        private bool CreateDirectory(string path, string name)
        {
            string upperDirectory = string.Format("{0}/{1}", path, name);
            if (Directory.Exists(upperDirectory))
            {
                if (MessageBox.Show("이미 존재하는 파일명입니다. 이름을 변경하시겠습니까?", "새로 만들기", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    ProjectName = name + "1";

                    CreateDirectory(path, ProjectName);
                }
                else
                {
                    return false;
                }
            }

            Directory.CreateDirectory(upperDirectory);

            Directory.CreateDirectory(string.Format("{0}/{1}", upperDirectory, "Flower"));
            Directory.CreateDirectory(string.Format("{0}/{1}", upperDirectory, "UI Designer"));
            Directory.CreateDirectory(string.Format("{0}/{1}", upperDirectory, "Flowchart Designer"));

            return true;
        }

        public void CreateFlowchart(FlowchartDesigner flowchartDesigner)
        {
            // XML 파일의 형식으로 순서도 파일 저장 할 것이다.
            string upperDirectory = string.Format("{0}/{1}", ProjectPath, ProjectName);

            File.Create(string.Format("{0}/{1}/{2}.xml", upperDirectory, "Flowchart Designer", flowchartDesigner.FlowValue));

            ProjectExplorerWindow.ProjectExplorer.RefreshProject();
        }
    }
}
