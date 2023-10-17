using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  HIS.Desktop.Plugins.HisImportServicePaty.ADO
{
    public class ServicePatyImportADO : V_HIS_SERVICE_PATY
    {
        public string OVERTIME_PRICE_STR { get; set; }
        public string PRICE_STR { get; set; }
        public string VAT_RATIO_STR { get; set; }
        public string PRIORITY_STR { get; set; }
        public string INTRUCTION_NUMBER_FROM_STR { get; set; }
        public string INTRUCTION_NUMBER_TO_STR { get; set; }
        public string DAY_FROM_STR { get; set; }
        public string DAY_TO_STR { get; set; }
        public string PACKAGE_CODE { get; set; }
        public string PACKAGE_NAME { get; set; }

        public string TREATMENT_FROM_TIME_STR { get; set; }
        public string TREATMENT_TO_TIME_STR { get; set; }
        public string FROM_TIME_STR { get; set; }
        public string TO_TIME_STR { get; set; }
        public string EXECUTE_ROOM_CODES { get; set; }
        public string REQUEST_DEPARMENT_CODES { get; set; }
        public string REQUEST_ROOM_CODES { get; set; }
        public string SERVICE_CONDITION_CODE { get; set; }
        

        public string ERROR { get; set; }

        public ServicePatyImportADO()
        {
        }

        public ServicePatyImportADO(V_HIS_SERVICE_PATY data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<ServicePatyImportADO>(this, data);
        }
    }
}
