using System;
using System.ComponentModel;
using FLOWeR_Garden_Ver02.DockablePane;
using FLOWeR_Garden_Ver02.FLOWeR;

namespace FLOWeR_Garden_Ver02.Explorer
{
    public class ProjectExplorerWindow : FloatingWindow
    {
        public ProjectExplorerWindow() : base()
        {
            Pane projectExplorerPane = new Pane("프로젝트 탐색기");
            ProjectExplorer = new ProjectExplorer();
            projectExplorerPane.MainContent = ProjectExplorer;

            ContentPane contentPane = new ContentPane();
            contentPane.AddPane(projectExplorerPane);

            base.MainContent = contentPane;

            ProjectManager.Instance.ProjectExplorerWindow = this;
        }

        public ProjectExplorer ProjectExplorer
        {
            get;
            set;
        }
    }
}
