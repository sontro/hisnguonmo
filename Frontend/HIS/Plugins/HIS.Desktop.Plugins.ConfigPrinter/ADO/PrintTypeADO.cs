using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ConfigPrinter.ADO
{
    public class PrintTypeADO : SAR_PRINT_TYPE
    {
        public string PRINTER_NAME { get; set; }
        public int Action { get; set; }
        public long? PRINT_TYPE_ID { get; set; }

        public PrintTypeADO() { }

        public PrintTypeADO(SAR_PRINT_TYPE type)
        {
            try
            {
                if (type != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<PrintTypeADO>(this, type);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
