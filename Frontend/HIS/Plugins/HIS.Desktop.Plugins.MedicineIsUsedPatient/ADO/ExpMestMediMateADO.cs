using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineIsUsedPatient.ADO
{
    class ExpMestMediMateADO
    {
        public string CONCRETE_ID__IN_SETY { get; set; }
        public string SERVICE_REQ_CODE { get; set; }
        public string REQUEST_LOGINNAME { get; set; }
        public string REQUEST_USERNAME { get; set; }
        public string INTRUCTION_TIME { get; set; }
        public long? MEDIMATE_ID { get; set; }
        public string MEDIMATE_TYPE_CODE { get; set; }
        public string MEDIMATE_TYPE_NAME { get; set; }
        public bool? IS_USED { get; set; }
        public bool IS_MEDICINE { get; set; }
        public bool IS_MATERIAL { get; set; }
        public decimal? AMOUNT { get; set; }
        public string PARENT_ID__IN_SETY { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public long EXP_MEST_MEDI_MATE_ID { get; set; }
        public ExpMestMediMateADO()
        {
        }
    }
}
