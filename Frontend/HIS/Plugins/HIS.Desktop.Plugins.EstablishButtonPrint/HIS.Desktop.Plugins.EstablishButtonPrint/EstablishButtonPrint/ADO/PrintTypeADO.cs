using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EstablishButtonPrint.EstablishButtonPrint.ADO
{
    public class PrintTypeADO: SAR.EFMODEL.DataModels.SAR_PRINT_TYPE
    {
        public bool IsCheck { get; set; }
        public string CAPTION { get; set; }
        public PrintTypeADO()
            : base()
        {
            IsCheck = false;
        }

        public PrintTypeADO(SAR_PRINT_TYPE printType)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<PrintTypeADO>(this, printType);
            this.IsCheck = false;
        }
    }
}
