using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.TreatmentEndTypeExt.Run
{
    interface IRun
    {
        object Run(FormEnum.TYPE _formType, DelegateSelectData _delegateSelectData);
    }
}
