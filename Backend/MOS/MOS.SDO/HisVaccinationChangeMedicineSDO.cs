using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.SDO
{
    /// <summary>
    /// Doi thong tin lo vaccin
    /// </summary>
    public class HisVaccinationChangeMedicineSDO
    {
        public long WorkingRoomId { get; set; }
        public long ExpMestMedicineId { get; set; }
        public long NewMedicineId { get; set; }
    }
}