using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class SecondaryIcdADO
    {
        public DelegateRefeshIcdChandoanphu DelegateRefeshIcdChandoanphu { get; set; }
        public string IcdCodes { get; set; }
        public string IcdNames { get; set; }

        public SecondaryIcdADO(DelegateRefeshIcdChandoanphu refeshIcdChandoanphu, string icdCodes, string icdNames)            
        {
            this.DelegateRefeshIcdChandoanphu = refeshIcdChandoanphu;
            this.IcdCodes = icdCodes;
            this.IcdNames = icdNames;
        }

    }
}
