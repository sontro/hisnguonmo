using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisTreatmentBedRoomViewSDO : V_HIS_TREATMENT_BED_ROOM
    {
        public decimal? UnpaidAmount { get; set; }
        public bool IsUnpaidWarning { get; set; }
        public bool HasTodayPrescription { get; set; }
    }
}
