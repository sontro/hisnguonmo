using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExamServiceReqResult.ADO
{
    public class HisSereServTeinADO : MOS.EFMODEL.DataModels.V_HIS_SERE_SERV_TEIN
    {
        public HisSereServTeinADO()
        {

        }

        public HisSereServTeinADO(MOS.EFMODEL.DataModels.V_HIS_SERE_SERV_TEIN data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<HisSereServTeinADO>(this, data);
        }
        public bool checkMinMax { get; set; }
        public long IS_PARENT { get; set; }
        public string MIN_VALUE { get; set; }
        public string MAX_VALUE { get; set; }
        public string VALUE_RANGE { get; set; }
        public long? LEVEL { get; set; }
        public string NOTE { get; set; }
        public string SERVICE_CODE { get; set; }
        public string SERVICE_NAME { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public decimal AMOUNT { get; set; }
        public string PATIENT_TYPE_NAME { get; set; }
        public long INTRUCTION_TIME { get; set; }
    }
}
