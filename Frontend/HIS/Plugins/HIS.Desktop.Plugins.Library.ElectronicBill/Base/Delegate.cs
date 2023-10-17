using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.Base
{
    public delegate bool DelegateSignAndRelease(Data.SignDelegate data, ref string errorMessage);
}
