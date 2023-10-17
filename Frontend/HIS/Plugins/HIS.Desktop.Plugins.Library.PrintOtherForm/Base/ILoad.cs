using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintOtherForm.Base
{
    public interface ILoad
    {
        bool Load(string printTypeCode, UpdateType.TYPE updateType);
    }
}
