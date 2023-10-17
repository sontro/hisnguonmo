using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SereServTeinBacterium.ADO
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

        public HisSereServTeinSDO(MOS.EFMODEL.DataModels.HIS_SERE_SERV_EXT data,V_HIS_SERE_SERV sereServ)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<ADO.HisSereServTeinSDO>(this, data);
            this.SERVICE_CODE = sereServ.TDL_SERVICE_CODE;
            this.SERVICE_NAME = sereServ.TDL_SERVICE_NAME;
            this.MICROCOPY_RESULT = data.MICROCOPY_RESULT;
            this.IMPLANTION_RESULT = data.IMPLANTION_RESULT;
        }
        public bool checkMinMax { get; set; }
        public long IS_PARENT { get; set; }
        public decimal? MIN_VALUE { get; set; }
        public decimal? MAX_VALUE { get; set; }
        public string VALUE_RANGE { get; set; }
        public long? LEVEL { get; set; }
        public string NOTE { get; set; }
        public string SERVICE_CODE { get; set; }
        public string SERVICE_NAME { get; set; }
        public string CONCLUDE { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public decimal AMOUNT { get; set; }
        public string PATIENT_TYPE_NAME { get; set; }
        public long INTRUCTION_TIME { get; set; }
        public string CHILD_ID { get; set; }
        public string PARENT_ID { get; set; }
        public string MICROCOPY_RESULT { get; set; }
        public string IMPLANTION_RESULT { get; set; }
        public string BACTERIUM_AMOUNT_DENSITY { get; set; }
    }
}
