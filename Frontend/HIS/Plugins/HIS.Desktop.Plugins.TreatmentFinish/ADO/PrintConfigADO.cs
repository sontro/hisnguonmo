using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentFinish
{
    class PrintConfigADO 
    {
        public string PrintTypeCode { get; set; }
        public HIS.Desktop.Plugins.TreatmentFinish.FormTreatmentFinish.ModuleTypePrint ModuleTypePrint { get; set; }
        public string PrintTypeName { get; set; }
        public bool Visible { get; set; }
        public bool IsAutoPrint { get; set; }
        public PrintConfigADO()
        {
        }
    }
}
