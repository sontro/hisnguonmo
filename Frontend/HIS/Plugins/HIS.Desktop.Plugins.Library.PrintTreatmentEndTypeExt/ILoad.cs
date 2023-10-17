using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintTreatmentEndTypeExt
{
    public interface ILoad
    {
       bool Run(string printCode, string fileName, bool PrintNow);
    }
}
