using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.FormMedicalRecord.Base
{
    public class BarItemADO
    {
        public bool IsVisible { get; set; }
        public string MediRecordName { get; set; }
        public MediRecordMenuPopupProcessor.MenuType Type { get; set; }
    }
}
