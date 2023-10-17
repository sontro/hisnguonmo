using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AntibioticRequest
{ 
    public delegate void DelegateRefeshIcdChandoanphu(string icdCodes, string icdNames);
    public delegate string DelegateGetIcdMain();
}
