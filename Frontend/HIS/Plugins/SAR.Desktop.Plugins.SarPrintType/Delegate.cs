using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.Desktop.Plugins.SarPrintType
{
    public delegate void RefeshReference();
    public delegate void DelegateNextFocus();
    public delegate void DelegateRefeshAcsUsers(string loginNames);
    public delegate string DelegateGetIcdMain();
}
