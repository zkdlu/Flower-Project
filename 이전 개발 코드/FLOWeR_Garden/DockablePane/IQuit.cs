using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLOWeR_Garden_Ver02.DockablePane
{
    public interface IQuit
    {
        void RemoveSelf(DockPane dockPane);
    }
}
