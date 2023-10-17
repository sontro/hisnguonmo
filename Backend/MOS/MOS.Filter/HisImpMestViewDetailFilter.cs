using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisImpMestViewDetailFilter 
    {
        /// <summary>
        /// Truong sap xep (mac dinh: MODIFY_TIME)
        /// </summary>
        public string ORDER_FIELD { get; set; }
        /// <summary>
        /// Chieu sap xep (DESC/ASC, mac dinh DESC)
        /// </summary>
        public string ORDER_DIRECTION { get; set; }

        public long? IMP_MEST_TYPE_ID { get; set; }
        public long? IMP_MEST_STT_ID { get; set; }
        public long? MEDI_STOCK_ID { get; set; }
        public long? MEDI_STOCK_PERIOD_ID { get; set; }
        public long? REQ_ROOM_ID { get; set; }
        public long? REQ_DEPARTMENT_ID { get; set; }

        public string TDL_TREATMENT_CODE__EXACT { get; set; }
        public string TDL_PATIENT_CODE__EXACT { get; set; }
        public string MEDICINE_TYPE_CODE__EXACT { get; set; }
        public string IMP_MEST_CODE__EXACT { get; set; }
        public string DOCUMENT_NUMBER__EXACT { get; set; }

        public HisImpMestViewDetailFilter()
            : base()
        {
        }
    }
}
