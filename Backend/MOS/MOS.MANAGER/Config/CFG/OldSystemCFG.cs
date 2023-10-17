using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.OldSystem.HMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class OldSystemCFG
    {
        /// <summary>
        /// Chon cac he thong PM cu can tich hop
        /// </summary>
        public enum IntegrationType
        {
            /// <summary>
            /// He thong phan mem PMS cua DK TW Can Tho
            /// </summary>
            PMS = 1,
            /// <summary>
            /// Ko tich hop he thong cu
            /// </summary>
            NONE = 0
        }

        private class HmsExamStyleMappingConfig
        {
            public string S { get; set; }
            public string P { get; set; }
            public int E { get; set; }
        }

        public class HmsExamStyleMapping
        {
            public long ServiceId { get; set; }
            public long PatientTypeId { get; set; }
            public int ExamStyleId { get; set; }
        }

        private const string ADDRESS_CFG = "MOS.OLD_SYSTEM.ADDRESS";
        private const string INTEGRATION_TYPE_CFG = "MOS.OLD_SYSTEM.INTEGRATION_TYPE";
        /// <summary>
        /// Cau hinh anh xa kieu kham
        /// </summary>
        private const string HMS_EXAM_STYLE_MAPPING_CFG = "MOS.OLD_SYSTEM.HMS.EXAM_STYLE_MAPPING";

        private static string address;
        internal static string ADDRESS
        {
            get
            {
                if (address == null)
                {
                    address = ConfigUtil.GetStrConfig(ADDRESS_CFG);
                }
                return address;
            }
        }

        private static IntegrationType integrationType;
        public static IntegrationType INTEGRATION_TYPE
        {
            get
            {
                if (integrationType == 0)
                {
                    integrationType = (IntegrationType)ConfigUtil.GetIntConfig(INTEGRATION_TYPE_CFG);
                }
                return integrationType;
            }
        }

        private static List<HmsExamStyleMapping> hmsExamStyleMapping;
        public static List<HmsExamStyleMapping> HMS_EXAM_STYLE_MAPPING
        {
            get
            {
                if (hmsExamStyleMapping == null)
                {
                    hmsExamStyleMapping = OldSystemCFG.ParseHmsExamStyleMapping(HMS_EXAM_STYLE_MAPPING_CFG);
                }
                return hmsExamStyleMapping;
            }
        }

        private static List<HmsExamStyleMapping> ParseHmsExamStyleMapping(string code)
        {
            try
            {
                List<HmsExamStyleMappingConfig> listConfig = new List<HmsExamStyleMappingConfig>();
                List<HmsExamStyleMapping> result = new List<HmsExamStyleMapping>();
                string data = ConfigUtil.GetStrConfig(code);
                if (!string.IsNullOrWhiteSpace(data))
                {
                    listConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HmsExamStyleMappingConfig>>(data);
                    if (listConfig != null && listConfig.Count > 0)
                    {
                        foreach (HmsExamStyleMappingConfig cf in listConfig)
                        {
                            V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.SERVICE_CODE == cf.S).FirstOrDefault();
                            HIS_PATIENT_TYPE patientType = HisPatientTypeCFG.DATA.Where(o => o.PATIENT_TYPE_CODE == cf.P).FirstOrDefault();
                            if (service == null)
                            {
                                LogSystem.Warn("MOS.OLD_SYSTEM.HMS.EXAM_STYLE_MAPPING. Ko co dich vu tuong ung voi ma: " + cf.S);
                            }
                            if (patientType == null)
                            {
                                LogSystem.Warn("MOS.OLD_SYSTEM.HMS.EXAM_STYLE_MAPPING. Ko co doi tuong tuong ung voi ma: " + cf.P);
                            }
                            if (cf.E <= 0)
                            {
                                LogSystem.Warn("MOS.OLD_SYSTEM.HMS.EXAM_STYLE_MAPPING. ExamStyleId <= 0: " + cf.E);
                            }
                            if (patientType != null && service != null && cf.E > 0)
                            {
                                HmsExamStyleMapping m = new HmsExamStyleMapping();
                                m.ExamStyleId = cf.E;
                                m.ServiceId = service.ID;
                                m.PatientTypeId = patientType.ID;
                                result.Add(m);
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static void Reload()
        {
            integrationType = (IntegrationType)ConfigUtil.GetIntConfig(INTEGRATION_TYPE_CFG);
            address = ConfigUtil.GetStrConfig(ADDRESS);
            hmsExamStyleMapping = OldSystemCFG.ParseHmsExamStyleMapping(HMS_EXAM_STYLE_MAPPING_CFG);
        }

    }
}
