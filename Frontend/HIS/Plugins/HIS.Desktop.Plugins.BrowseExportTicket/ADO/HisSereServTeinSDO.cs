using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TestServiceReqExcute.ADO
{
    public class HisSereServTeinSDO : MOS.EFMODEL.DataModels.V_HIS_SERE_SERV_TEIN
    {
        public HisSereServTeinSDO()
        {

        }

        public HisSereServTeinSDO(MOS.EFMODEL.DataModels.V_HIS_SERE_SERV_TEIN data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<ADO.HisSereServTeinSDO>(this, data);
        }
        public bool checkMinMax { get; set; }
        public long IS_PARENT { get; set; }
        public long HAS_ONE_CHILD { get; set; }
        public decimal? MIN_VALUE { get; set; }
        public decimal? MAX_VALUE { get; set; }
        //public string VALUE_RANGE { get; set; }
        public long? LEVEL { get; set; }
        //public string NOTE { get; set; }
        public string SERVICE_CODE { get; set; }
        public string SERVICE_NAME { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public decimal AMOUNT { get; set; }
        public string PATIENT_TYPE_NAME { get; set; }
        public long INTRUCTION_TIME { get; set; }
        public long? MACHINE_ID { get; set; }
        public long SERVICE_ID { get; set; }
        public bool? IS_HIGHER { get; set; }
        public bool? IS_LOWER { get; set; }
        public bool? IS_NORMAL { get; set; }
        public string SAMPLE_TIME { get; set; }

        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypeMachineId { get; set; }
        public string ErrorMessageMachineId { get; set; }
    }
}
