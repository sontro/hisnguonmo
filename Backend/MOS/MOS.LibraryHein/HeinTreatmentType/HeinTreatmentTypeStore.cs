using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.LibraryHein.Bhyt.HeinTreatmentType
{
    public class HeinTreatmentTypeStore
    {
        private static readonly Dictionary<string, HeinTreatmentTypeData> HEIN_TREATMENT_TYPE_STORE = new Dictionary<string, HeinTreatmentTypeData>()
        {
            {HeinTreatmentTypeCode.EXAM, new HeinTreatmentTypeData(HeinTreatmentTypeCode.EXAM, "Khám")},
            {HeinTreatmentTypeCode.TREAT, new HeinTreatmentTypeData(HeinTreatmentTypeCode.TREAT, "Điều trị")},
        };

        public static List<HeinTreatmentTypeData> Get()
        {
            try
            {
                return HEIN_TREATMENT_TYPE_STORE.Values.ToList();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static List<string> GetCodes()
        {
            try
            {
                return HEIN_TREATMENT_TYPE_STORE.Keys.ToList();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static bool IsValidCode(string heinTreatmentTypeCode)
        {
            if (!GetCodes().Contains(heinTreatmentTypeCode))
            {
                LogSystem.Error("Ma 'Loai Kham/Dieu tri' khong hop le");
                return false;
            }
            return true;
        }
    }
}
