using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Utility;
namespace HIS.Desktop.Plugins.ModuleButtonCustomize.ADO
{
    class CustomizeADOs : ModuleControlADO
    {
        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorShort_Cut { get; set; }
        public string ErrorMessageShortCut { get; set; }
        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorCurrent_shortCut { get; set; }
        public string ErrorMessageCurrentShortCut { get; set; }
        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorCaption { get; set; }
        public string ErrorMessageCaption{ get; set; }
        
    }
}
