using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.SDO
{
    /// <summary>
    /// Loại đơn thuốc (tân dược, y học cổ truyền)
    /// </summary>
    public enum PrescriptionType
    {
        /// <summary>
        /// Tân dược
        /// </summary>
        NEW = 1,
        /// <summary>
        /// Y học cổ truyền
        /// </summary>
        TRADITIONAL = 2,
        /// <summary>
        /// Don CLS
        /// </summary>
        SUBCLINICAL = 3
    }

    /// <summary>
    /// Doi tuong phuc vu ke don thuoc/vat tu
    /// </summary>
    public class PrescriptionSDO : HisServiceReqSDO
    {
        public string Advise { get; set; }
        public long? UseTime { get; set; }
        public long? NumOfDays { get; set; }
        public long? KidneyTimes { get; set; }
        public bool IsExecuteKidneyPres { get; set; }
        public long? SpecialMedicineType { get; set; }
        public string InteractionReason { get; set; }
        public List<PresMedicineSDO> Medicines { get; set; }
        public List<PresMaterialSDO> Materials { get; set; }
        public List<long> UseTimes { get; set; }
        
        //Trong truong hop ke don co chon vat tu theo serial_number
        public List<PresMaterialBySerialNumberSDO> SerialNumbers { get; set; }

        //Don ngoai kho (tu mua)
        public List<PresOutStockMatySDO> ServiceReqMaties { get; set; }
        public List<PresOutStockMetySDO> ServiceReqMeties { get; set; }
    }
}
