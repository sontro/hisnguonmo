using HIS.Desktop.Common;
using HIS.Desktop.Plugins.Library.PrintBordereau.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintBordereau.ADO
{
    public class ReloadMenuOption
    {
        public DelegateSelectData ReloadMenu { get; set; }
        public MenuType Type { get; set; }
        public BordereauPrint.Type? BordereauPrint { get; set; }

        public enum MenuType
        {
            NORMAL = 1,
            DYNAMIC = 2
        }
    }
}
