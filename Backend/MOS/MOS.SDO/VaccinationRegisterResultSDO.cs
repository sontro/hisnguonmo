using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.SDO
{
    /// <summary>
    /// Ket qua chi dinh vaccin
    /// </summary>
    public class VaccinationRegisterResultSDO
    {
        public HIS_PATIENT Patient { get; set; }
        public HIS_VACCINATION_EXAM VaccinationExam { get; set; }
        public List<HIS_VACCINATION> Vaccinations { get; set; }
        public List<HIS_EXP_MEST_MEDICINE> Medicines { get; set; }
        public List<HIS_EXP_MEST> ExpMests { get; set; }
    }
}
