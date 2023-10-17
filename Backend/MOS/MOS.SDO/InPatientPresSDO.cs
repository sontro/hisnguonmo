using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.SDO
{
    /// <summary>
    /// Doi tuong phuc vu ke don noi tru
    /// </summary>
    public class InPatientPresSDO : PrescriptionSDO
    {
        public List<long> InstructionTimes { get; set; }
        public long? RemedyCount { get; set; }
        public long? ExecuteGroupId { get; set; }
        public bool IsHomePres { get; set; }

        public string TreatEyeTensionLeft { get; set; }
        public string TreatEyeTensionRight { get; set; }
        public string TreatEyesightLeft { get; set; }
        public string TreatEyesightRight { get; set; }
        public string TreatEyesightGlassLeft { get; set; }
        public string TreatEyesightGlassRight { get; set; }

        public PrescriptionType PrescriptionTypeId { get; set; }
        public List<TrackingInfoSDO> TrackingInfos { get; set; }

        public long? ExpMestReasonId { get; set; }
        public long? DrugStoreId { get; set; } //medi_stock_id
        public short? IsTemporaryPres { get; set; } //đơn tạm
    }
}
