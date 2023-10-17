using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServicePatyList.ADO
{
    class ServicePatyAdo:HIS_SERVICE_PATY
    {
        public string BRANCH_CODE { get; set; }
        public short? DAY_FROM { get; set; }
        public short? DAY_TO { get; set; }
        public string EXECUTE_ROOM_CODES { get; set; }
        public long? FROM_TIME { get; set; }
        public string HOUR_FROM { get; set; }
        public string HOUR_TO { get; set; }
        public long? INTRUCTION_NUMBER_FROM { get; set; }
        public long? INTRUCTION_NUMBER_TO { get; set; }
        public string MODIFIER { get; set; }
        public long? MODIFY_TIME { get; set; }
        public string PATIENT_TYPE_CODE { get; set; }
        public decimal PRICE { get; set; }
        public decimal? OVERTIME_PRICE { get; set; }
        public long? PRIORITY { get; set; }
        public string REQUEST_DEPARMENT_CODES { get; set; }
        public string REQUEST_ROOM_CODES { get; set; }
        public string SERVICE_CODE { get; set; }
        public long? TO_TIME { get; set; }
        public long? TREATMENT_FROM_TIME { get; set; }
        public long? TREATMENT_TO_TIME { get; set; }
        public decimal VAT_RATIO { get; set; }
    }
}
