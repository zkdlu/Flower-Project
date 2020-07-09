using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLOWeR_Garden_Ver02
{
    public class MakeProjectEventArgs : EventArgs
    {
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

        public MakeProjectEventArgs(string projectName, string projectPath)
        {
            ProjectName = projectName;
            ProjectPath = projectPath;
        }
    }

    public delegate void MakeProjectEventHandler(object sender, MakeProjectEventArgs e);
}
