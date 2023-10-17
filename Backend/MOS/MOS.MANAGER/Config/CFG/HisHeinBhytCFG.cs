using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    public class HisHeinBhytCFG
    {
        /// <summary>
        /// Cac option cau hinh cho phep tao nhieu ho so dieu tri trong 1 ngay
        /// </summary>
        public enum Calc2thExamDiffPriceOption
        {
            /// <summary>
            /// con so tien BN phai tra = so tien dich vu - so tien BHYT chi tra
            /// </summary>
            NORMAL = 0,
            /// <summary>
            /// so tien chenh lech cua dich vu thu 2 duoc tinh dung bang so tien 
            /// chenh lech cua dich vu do trong truong hop dich vu do duoc kham lan 1
            /// </summary>
            FIRST_DIFF_1 = 1,
            /// <summary>
            /// so tien chenh lech cua dich vu thu 2 duoc tinh dung bang so tien chenh lech cua dich vu thu nhat
            /// </summary>
            FIRST_DIFF_2 = 2
        }

        /// <summary>
        /// Cac tuy chon voi PTTT phat sinh
        /// </summary>
        public enum CalcArisingSurgPriceOption
        {
            /// <summary>
            /// PTTT phat sinh chi tinh (80 hoac 50%) va Bn ko phai tra phan chi phi con lai (khong tinh voi truong hop co chi phi phu thu)
            /// </summary>
            NOT_PAY_REMAIN = 0,

            /// <summary>
            /// PTTT phat sinh chi tinh (80 hoac 50%) va Bn phai tra phan chi phi con lai
            /// </summary>
            PAY_REMAIN = 1,
            /// <summary>
            /// PTTT phat sinh chi tinh (80 hoac 50%) va Bn ko phai tra phan chi phi con lai ke ca truong hop co chi phi phu thu
            /// </summary>
            NOT_PAY_REMAIN_ALL = 2,
        }

        public enum SecondStentRatioOption
        {
            NORMAL = 0,
            /// <summary>
            /// Thanh toan toan bo voi the dac biet
            /// </summary>
            ALL_FOR_SPECIAL_CARD = 1,
        }

        public enum CalcMaterialPackagePriceOption
        {
            /// <summary>
            /// Vat tu phat sinh phai do phong xu ly dich vu chi dinh, thi moi tinh vao goi VTYT
            /// </summary>
            SAME_ROOM = 0,
            /// <summary>
            /// Khong rang buoc ve khoa/phong chi dinh, chi can "dinh kem" vao dich vu la duoc
            /// </summary>
            NO_CONSTRAINT_ROOM = 1,
        }

        public enum EmergencyExamPolicyOption
        {
            BY_EXECUTE_ROOM = 1,
            BY_EXECUTE_DEPARTMENT = 2
        }

        //Co ap dung cong thuc tinh tien cong kham lan 2 trong ngay doi voi cac ho so dieu tri khac cua cung BN hay khong
        private const string CALC_2TH_EXAM_FOR_OTHER_TREATMENT_CFG = "MOS.BHYT.CALC_2TH_EXAM_IN_DAY_FOR_OTHER_TREATMENT";
        //Lua chon cach tinh gia chenh lech cua cong kham thu 2
        private const string CALC_2TH_EXAM_DIFF_PRICE_OPTION_CFG = "MOS.BHYT.CALC_2TH_EXAM_DIFF_PRICE_OPTION";
        //Co luon set 0d cho cong kham thu 2 cung chuyen khoa hay khong
        private const string SET_ZERO_TO_2TH_SAME_SPECIALITY_EXAM_PRICE_CFG = "MOS.BHYT.SET_ZERO_TO_2TH_SAME_SPECIALITY_EXAM_PRICE";
        private const string BHYT_NDS_ICD_CODE__OTHER_CFG = "MOS.BHYT.NDS_ICD_CODE__OTHER";
        private const string BHYT_NDS_ICD_CODE__TE_CFG = "MOS.BHYT.NDS_ICD_CODE__TE";
        private const string LIMIT_HEIN_MEDICINE_PRICE__RIGHT_MEDI_ORG_CFG = "MOS.BHYT.LIMIT_HEIN_MEDICINE_PRICE.RIGHT_MEDI_ORG";
        private const string LIMIT_HEIN_MEDICINE_PRICE__NOT_RIGHT_MEDI_ORG_CFG = "MOS.BHYT.LIMIT_HEIN_MEDICINE_PRICE.NOT_RIGHT_MEDI_ORG";
        private const string PAYMENT_RATIO__SERVICE__COMMUNE_LEVEL_CFG = "MOS.BHYT.PAYMENT_RATIO.SERVICE.COMMUNE_LEVEL";
        private const string EXCEED_DAY_ALLOW_FOR_IN_PATIENT_CFG = "MOS.BHYT.EXCEED_DAY_ALLOW_FOR_IN_PATIENT";
        private const string EMERGENCY_EXAM_POLICY_OPTION_CFG = "MOS.BHYT.EMERGENCY_EXAM_POLICY_OPTION";
        //Danh sach cac dau ma the ko gioi han so tien BHYT chi tra ky thuat cao
        private const string NO_LIMIT_HIGH_HEIN_SERVICE_PRICE__PREFIX_CFG = "MOS.BHYT.NO_LIMIT_HIGH_HEIN_SERVICE_PRICE__PREFIX";
        //Danh sach cac dau ma the ko ap dung tran thuoc/vat tu
        private const string NO_LIMIT_MEDICINE_MATERIAL_PRICE__PREFIX_CFG = "MOS.BHYT.NO_LIMIT_MEDICINE_MATERIAL_PRICE__PREFIX";
        //Tuy chon tinh ti le thanh toan doi voi stent thu 2 trong goi ky thuat cao
        private const string SECOND_STENT_RATIO_OPTION_CFG = "MOS.BHYT.SECOND_STENT_RATIO_OPTION";
        //Cach tinh tien BHYT voi dich vu phau thuat thu thuat thu 2
        private const string CALC_ARISING_SURG_PRICE_OPTION_CFG = "MOS.BHYT.CALC_ARISING_SURG_PRICE_OPTION";
        //Cach tinh tien BHYT voi goi vat tu y te
        private const string CALC_MATERIAL_PACKAGE_PRICE_OPTION_CFG = "MOS.BHYT.CALC_MATERIAL_PACKAGE_PRICE_OPTION";
        //Cach hien thi don gia cho vat tu trong xml
        private const string XML__4210__MATERIAL_PRICE_OPTION_CFG = "XML.EXPORT.4210.MATERIAL_PRICE_OPTION";
        private const string XML__4210__MATERIAL_STENT_RATIO_OPTION_CFG = "XML.EXPORT.4210.MATERIAL_STENT_RATIO_OPTION";
        private const string XML_EXPORT__TEN_BENH_OPTION_CFG = "HIS.Desktop.Plugins.ExportXml.TenBenhOption";
        private const string XML_EXPORT__HEIN_CODE_NO_TUTORIAL_CFG = "HIS.Desktop.Plugins.ExportXml.HeinServiceTypeCodeNoTutorial";
        private const string XML_EXPORT__XML_NUMBERS = "HIS.Desktop.Plugins.ExportXml.XmlNumbers";
        private const string MATERIAL_STENT2_LIMIT_OPTION = "XML.EXPORT.4210.MATERIAL_STENT2_LIMIT_OPTION";
        private const string IS_TREATMENT_DAY_COUNT_6556_KEY = "XML.EXPORT.4210.IS_TREATMENT_DAY_COUNT_6556";
        private const string MA_BAC_SI_EXAM_OPTION_KEY = "XML.EXPORT.4210.XML3.MA_BAC_SI_EXAM_OPTION";

        private static EmergencyExamPolicyOption? emergencyExamPolicyOption;
        public static EmergencyExamPolicyOption EMERGENCY_EXAM_POLICY_OPTION
        {
            get
            {
                if (!emergencyExamPolicyOption.HasValue)
                {
                    emergencyExamPolicyOption = (EmergencyExamPolicyOption)ConfigUtil.GetIntConfig(EMERGENCY_EXAM_POLICY_OPTION_CFG);
                }
                return emergencyExamPolicyOption.Value;
            }
        }

        private static bool? setZeroTo2thSameSpecialityExamPrice;
        public static bool SET_ZERO_TO_2TH_SAME_SPECIALITY_EXAM_PRICE
        {
            get
            {
                if (!setZeroTo2thSameSpecialityExamPrice.HasValue)
                {
                    setZeroTo2thSameSpecialityExamPrice = ConfigUtil.GetIntConfig(SET_ZERO_TO_2TH_SAME_SPECIALITY_EXAM_PRICE_CFG) == 1;
                }
                return setZeroTo2thSameSpecialityExamPrice.Value;
            }
        }

        private static decimal? limitHeinMedicinePriceRightMediOrg;
        public static decimal? LIMIT_HEIN_MEDICINE_PRICE__RIGHT_MEDI_ORG
        {
            get
            {
                if (!limitHeinMedicinePriceRightMediOrg.HasValue)
                {
                    limitHeinMedicinePriceRightMediOrg = ConfigUtil.GetDecimalConfig(LIMIT_HEIN_MEDICINE_PRICE__RIGHT_MEDI_ORG_CFG) == null ? -1 : ConfigUtil.GetDecimalConfig(LIMIT_HEIN_MEDICINE_PRICE__RIGHT_MEDI_ORG_CFG);//set bang -1 de ko query lai nhieu lan
                }
                return limitHeinMedicinePriceRightMediOrg;
            }
            set
            {
                limitHeinMedicinePriceRightMediOrg = value;
            }
        }

        private static decimal? limitHeinMedicinePriceNotRightMediOrg;
        public static decimal? LIMIT_HEIN_MEDICINE_PRICE__NOT_RIGHT_MEDI_ORG
        {
            get
            {
                if (!limitHeinMedicinePriceNotRightMediOrg.HasValue)
                {
                    limitHeinMedicinePriceNotRightMediOrg = ConfigUtil.GetDecimalConfig(LIMIT_HEIN_MEDICINE_PRICE__NOT_RIGHT_MEDI_ORG_CFG) == null ? -1 : ConfigUtil.GetDecimalConfig(LIMIT_HEIN_MEDICINE_PRICE__NOT_RIGHT_MEDI_ORG_CFG);//set bang -1 de ko query lai nhieu lan
                }
                return limitHeinMedicinePriceNotRightMediOrg;
            }
        }

        private static decimal? servicePaymentRatioCommuneLevel;
        public static decimal? PAYMENT_RATIO__SERVICE__COMMUNE_LEVEL
        {
            get
            {
                if (!servicePaymentRatioCommuneLevel.HasValue)
                {
                    servicePaymentRatioCommuneLevel = ConfigUtil.GetDecimalConfig(PAYMENT_RATIO__SERVICE__COMMUNE_LEVEL_CFG) == null ? -1 : ConfigUtil.GetDecimalConfig(PAYMENT_RATIO__SERVICE__COMMUNE_LEVEL_CFG);//set bang -1 de ko query lai nhieu lan
                }
                return servicePaymentRatioCommuneLevel;
            }
        }

        private static bool? calc2thExamForOtherTreatment;
        public static bool CALC_2TH_EXAM_FOR_OTHER_TREATMENT
        {
            get
            {
                if (!calc2thExamForOtherTreatment.HasValue)
                {
                    calc2thExamForOtherTreatment = ConfigUtil.GetIntConfig(CALC_2TH_EXAM_FOR_OTHER_TREATMENT_CFG) == 1;
                }
                return calc2thExamForOtherTreatment.Value;
            }
        }

        private static List<string> bhytNdsIcdCodeTe;
        public static List<string> BHYT_NDS_ICD_CODE__TE
        {
            get
            {
                if (bhytNdsIcdCodeTe != null)
                {
                    bhytNdsIcdCodeTe = ConfigUtil.GetStrConfigs(BHYT_NDS_ICD_CODE__TE_CFG);
                }
                return bhytNdsIcdCodeTe;
            }
        }

        private static List<string> bhytNdsIcdCodeOther;
        public static List<string> BHYT_NDS_ICD_CODE__OTHER
        {
            get
            {
                if (bhytNdsIcdCodeOther != null)
                {
                    bhytNdsIcdCodeOther = ConfigUtil.GetStrConfigs(BHYT_NDS_ICD_CODE__OTHER_CFG);
                }
                return bhytNdsIcdCodeOther;
            }
        }

        private static List<string> noLimitHighHeinServicePricePrefixs;
        public static List<string> NO_LIMIT_HIGH_HEIN_SERVICE_PRICE__PREFIXs
        {
            get
            {
                if (noLimitHighHeinServicePricePrefixs == null)
                {
                    noLimitHighHeinServicePricePrefixs = ConfigUtil.GetStrConfigs(NO_LIMIT_HIGH_HEIN_SERVICE_PRICE__PREFIX_CFG);
                }
                return noLimitHighHeinServicePricePrefixs;
            }
        }

        private static List<string> noLimitMedicineMaterialPricePrefixs;
        public static List<string> NO_LIMIT_MEDICINE_MATERIAL_PRICE__PREFIXs
        {
            get
            {
                if (noLimitMedicineMaterialPricePrefixs == null)
                {
                    noLimitMedicineMaterialPricePrefixs = ConfigUtil.GetStrConfigs(NO_LIMIT_MEDICINE_MATERIAL_PRICE__PREFIX_CFG);
                }
                return noLimitMedicineMaterialPricePrefixs;
            }
        }

        private static SecondStentRatioOption? secondStentRatioOption;
        public static SecondStentRatioOption? SECOND_STENT_RATIO_OPTION
        {
            get
            {
                if (!secondStentRatioOption.HasValue)
                {
                    secondStentRatioOption = (SecondStentRatioOption)ConfigUtil.GetIntConfig(SECOND_STENT_RATIO_OPTION_CFG);
                }
                return secondStentRatioOption;
            }
        }

        private static int? calc2thExamDiffPriceOption;
        public static int CALC_2TH_EXAM_DIFF_PRICE_OPTION
        {
            get
            {
                if (!calc2thExamDiffPriceOption.HasValue)
                {
                    calc2thExamDiffPriceOption = ConfigUtil.GetIntConfig(CALC_2TH_EXAM_DIFF_PRICE_OPTION_CFG);
                }
                return calc2thExamDiffPriceOption.Value;
            }
        }

        private static CalcArisingSurgPriceOption? calcArisingSurgPriceOption;
        public static CalcArisingSurgPriceOption? CALC_ARISING_SURG_PRICE_OPTION
        {
            get
            {
                if (!calcArisingSurgPriceOption.HasValue)
                {
                    calcArisingSurgPriceOption = (CalcArisingSurgPriceOption)ConfigUtil.GetIntConfig(CALC_ARISING_SURG_PRICE_OPTION_CFG);
                }
                return calcArisingSurgPriceOption;
            }
        }

        private static CalcMaterialPackagePriceOption? calcMaterialPackagePriceOption;
        public static CalcMaterialPackagePriceOption? CALC_MATERIAL_PACKAGE_PRICE_OPTION
        {
            get
            {
                if (!calcMaterialPackagePriceOption.HasValue)
                {
                    calcMaterialPackagePriceOption = (CalcMaterialPackagePriceOption)ConfigUtil.GetIntConfig(CALC_MATERIAL_PACKAGE_PRICE_OPTION_CFG);
                }
                return calcMaterialPackagePriceOption;
            }
        }

        private static string MaterialPriceOption;
        public static string XML__4210__MATERIAL_PRICE_OPTION
        {
            get
            {
                if (MaterialPriceOption == null)
                {
                    MaterialPriceOption = ConfigUtil.GetStrConfig(XML__4210__MATERIAL_PRICE_OPTION_CFG);
                }
                return MaterialPriceOption;
            }
        }

        private static string MaterialStentRatio;
        public static string XML__4210__MATERIAL_STENT_RATIO_OPTION
        {
            get
            {
                if (MaterialStentRatio == null)
                {
                    MaterialStentRatio = ConfigUtil.GetStrConfig(XML__4210__MATERIAL_STENT_RATIO_OPTION_CFG);
                }
                return MaterialStentRatio;
            }
        }

        private static string TenBenhOption;
        public static string XML_EXPORT__TEN_BENH_OPTION
        {
            get
            {
                if (TenBenhOption == null)
                {
                    TenBenhOption = ConfigUtil.GetStrConfig(XML_EXPORT__TEN_BENH_OPTION_CFG);
                }
                return TenBenhOption;
            }
        }

        private static int? exceedDayAllowForInPatient;
        public static int EXCEED_DAY_ALLOW_FOR_IN_PATIENT
        {
            get
            {
                if (!exceedDayAllowForInPatient.HasValue)
                {
                    exceedDayAllowForInPatient = ConfigUtil.GetIntConfig(EXCEED_DAY_ALLOW_FOR_IN_PATIENT_CFG);
                }
                return exceedDayAllowForInPatient.Value;
            }
        }

        private static string HeinCodeNoTutorial;
        public static string XML_EXPORT__HEIN_CODE_NO_TUTORIAL
        {
            get
            {
                if (HeinCodeNoTutorial == null)
                {
                    HeinCodeNoTutorial = ConfigUtil.GetStrConfig(XML_EXPORT__HEIN_CODE_NO_TUTORIAL_CFG);
                }
                return HeinCodeNoTutorial;
            }
        }

        private static string numbers;
        public static string XML_EXPORT__NUMBER
        {
            get
            {
                if (numbers == null)
                {
                    numbers = ConfigUtil.GetStrConfig(XML_EXPORT__XML_NUMBERS);
                }
                return numbers;
            }
        }

        private static string materialStent2LimitOption;
        public static string XML_EXPORT__MATERIAL_STENT2_LIMIT_OPTION
        {
            get
            {
                if (materialStent2LimitOption == null)
                {
                    materialStent2LimitOption = ConfigUtil.GetStrConfig(MATERIAL_STENT2_LIMIT_OPTION);
                }
                return materialStent2LimitOption;
            }
        }

        private static string isTreatmentDayCount6556;
        public static string IS_TREATMENT_DAY_COUNT_6556
        {
            get
            {
                if (isTreatmentDayCount6556 == null)
                {
                    isTreatmentDayCount6556 = ConfigUtil.GetStrConfig(IS_TREATMENT_DAY_COUNT_6556_KEY);
                }
                return isTreatmentDayCount6556;
            }
        }

        private static string maBacSiExamOption;
        public static string MA_BAC_SI_EXAM_OPTION
        {
            get
            {
                if (maBacSiExamOption == null)
                {
                    maBacSiExamOption = ConfigUtil.GetStrConfig(MA_BAC_SI_EXAM_OPTION_KEY);
                }
                return maBacSiExamOption;
            }
        }

        private static List<long> GetIds(string code)
        {
            List<long> result = null;
            try
            {
                result = new List<long>();
                List<string> data = ConfigUtil.GetStrConfigs(code);
                foreach (string t in data)
                {
                    string[] tmp = t.Split(':');
                    if (tmp != null && tmp.Length >= 2)
                    {
                        V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.SERVICE_TYPE_CODE == tmp[0] && o.SERVICE_CODE == tmp[1]).FirstOrDefault();
                        if (service != null)
                        {
                            result.Add(service.ID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public static void Reload()
        {
            calc2thExamForOtherTreatment = ConfigUtil.GetIntConfig(CALC_2TH_EXAM_FOR_OTHER_TREATMENT_CFG) == 1;

            bhytNdsIcdCodeTe = ConfigUtil.GetStrConfigs(BHYT_NDS_ICD_CODE__TE_CFG);

            bhytNdsIcdCodeOther = ConfigUtil.GetStrConfigs(BHYT_NDS_ICD_CODE__OTHER_CFG);

            calc2thExamDiffPriceOption = ConfigUtil.GetIntConfig(CALC_2TH_EXAM_DIFF_PRICE_OPTION_CFG);

            var priceMediOrg = ConfigUtil.GetDecimalConfig(LIMIT_HEIN_MEDICINE_PRICE__RIGHT_MEDI_ORG_CFG) == null ? -1 : ConfigUtil.GetDecimalConfig(LIMIT_HEIN_MEDICINE_PRICE__RIGHT_MEDI_ORG_CFG);

            var priceNotRightMediOrg = ConfigUtil.GetDecimalConfig(LIMIT_HEIN_MEDICINE_PRICE__NOT_RIGHT_MEDI_ORG_CFG) == null ? -1 : ConfigUtil.GetDecimalConfig(LIMIT_HEIN_MEDICINE_PRICE__NOT_RIGHT_MEDI_ORG_CFG);
            var ratioCommuneLevel = ConfigUtil.GetDecimalConfig(PAYMENT_RATIO__SERVICE__COMMUNE_LEVEL_CFG) == null ? -1 : ConfigUtil.GetDecimalConfig(PAYMENT_RATIO__SERVICE__COMMUNE_LEVEL_CFG);
            emergencyExamPolicyOption = (EmergencyExamPolicyOption)ConfigUtil.GetIntConfig(EMERGENCY_EXAM_POLICY_OPTION_CFG);
            exceedDayAllowForInPatient = ConfigUtil.GetIntConfig(EXCEED_DAY_ALLOW_FOR_IN_PATIENT_CFG);
            noLimitHighHeinServicePricePrefixs = ConfigUtil.GetStrConfigs(NO_LIMIT_HIGH_HEIN_SERVICE_PRICE__PREFIX_CFG);
            noLimitMedicineMaterialPricePrefixs = ConfigUtil.GetStrConfigs(NO_LIMIT_MEDICINE_MATERIAL_PRICE__PREFIX_CFG);
            limitHeinMedicinePriceNotRightMediOrg = priceNotRightMediOrg;
            limitHeinMedicinePriceRightMediOrg = priceMediOrg;
            servicePaymentRatioCommuneLevel = ratioCommuneLevel;
            calcArisingSurgPriceOption = (CalcArisingSurgPriceOption)ConfigUtil.GetIntConfig(CALC_ARISING_SURG_PRICE_OPTION_CFG);
            calcMaterialPackagePriceOption = (CalcMaterialPackagePriceOption)ConfigUtil.GetIntConfig(CALC_MATERIAL_PACKAGE_PRICE_OPTION_CFG);
            setZeroTo2thSameSpecialityExamPrice = ConfigUtil.GetIntConfig(SET_ZERO_TO_2TH_SAME_SPECIALITY_EXAM_PRICE_CFG) == 1;
            MaterialPriceOption = ConfigUtil.GetStrConfig(XML__4210__MATERIAL_PRICE_OPTION_CFG);
            MaterialStentRatio = ConfigUtil.GetStrConfig(XML__4210__MATERIAL_STENT_RATIO_OPTION_CFG);
            HeinCodeNoTutorial = ConfigUtil.GetStrConfig(XML_EXPORT__HEIN_CODE_NO_TUTORIAL_CFG);
            numbers = ConfigUtil.GetStrConfig(XML_EXPORT__XML_NUMBERS);
            materialStent2LimitOption = ConfigUtil.GetStrConfig(MATERIAL_STENT2_LIMIT_OPTION);
            isTreatmentDayCount6556 = ConfigUtil.GetStrConfig(IS_TREATMENT_DAY_COUNT_6556_KEY);
            maBacSiExamOption = ConfigUtil.GetStrConfig(MA_BAC_SI_EXAM_OPTION_KEY);

            secondStentRatioOption = (SecondStentRatioOption)ConfigUtil.GetIntConfig(SECOND_STENT_RATIO_OPTION_CFG);
        }
    }
}
