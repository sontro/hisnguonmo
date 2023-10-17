using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisExpMestMaterialFilter : FilterBase
    {
        public long? EXP_MEST_ID { get; set; }
        public long? MATERIAL_ID { get; set; }
        public long? TDL_AGGR_EXP_MEST_ID { get; set; }
        public long? TDL_AGGR_EXP_MEST_ID__OR__EXP_MEST_ID { get; set; }
        public long? SERE_SERV_PARENT_ID { get; set; }
        public long? TDL_SERVICE_REQ_ID { get; set; }
        public long? TDL_TREATMENT_ID { get; set; }
        public long? PATIENT_TYPE_ID { get; set; }
        public long? TDL_MEDI_STOCK_ID { get; set; }

        public List<long> SERE_SERV_PARENT_IDs { get; set; }
        public List<long> TDL_AGGR_EXP_MEST_IDs { get; set; }
        public List<long> EXP_MEST_IDs { get; set; }
        public List<long> MATERIAL_IDs { get; set; }
        public List<long> TDL_SERVICE_REQ_IDs { get; set; }
        public List<long> TDL_TREATMENT_IDs { get; set; }
        public List<long> PATIENT_TYPE_IDs { get; set; }
        public List<long> TDL_MEDI_STOCK_IDs { get; set; }

        public long? EXP_TIME_FROM { get; set; }        
        public long? EXP_TIME_TO { get; set; }

        public long? EXP_DATE_FROM { get; set; }
        public long? EXP_DATE_TO { get; set; }

        public bool? IS_EXPORT { get; set; }
        public bool? IS_APPROVED { get; set; }

        public string SERIAL_NUMBER__EXACT { get; set; }
        public bool? HAS_SERIAL_NUMBER { get; set; }
        public bool? HAS_STENT_ORDER { get; set; }

        public HisExpMestMaterialFilter()
            : base()
        {
        }
    }
}
