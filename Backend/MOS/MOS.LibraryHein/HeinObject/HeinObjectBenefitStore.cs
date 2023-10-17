using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.LibraryHein.Bhyt.HeinObject
{
    /// <summary>
    /// Luu cau hinh ma quyen loi - doi tuong theo quy dinh cua bao hiem
    /// </summary>
    public class HeinObjectBenefitStore
    {
        private static readonly Dictionary<string, HeinBenefitData> HEIN_OBJECT_BENEFIT_STORE = new Dictionary<string, HeinBenefitData>()
        {
            {"1", new HeinBenefitData("1", new List<string>{
                HeinObjectCode.CC,
                HeinObjectCode.TE,
                HeinObjectCode.CK,
                HeinObjectCode.CB,
                HeinObjectCode.KC,
                HeinObjectCode.HN,
                HeinObjectCode.DT,
                HeinObjectCode.DK,
                HeinObjectCode.XD,
                HeinObjectCode.BT,
                HeinObjectCode.TS,
                HeinObjectCode.HT,
                HeinObjectCode.TC,
                HeinObjectCode.CN,
                HeinObjectCode.DN,
                HeinObjectCode.HX,
                HeinObjectCode.CH,
                HeinObjectCode.NN,
                HeinObjectCode.TK,
                HeinObjectCode.HC,
                HeinObjectCode.XK,
                HeinObjectCode.TB,
                HeinObjectCode.NO,
                HeinObjectCode.CT,
                HeinObjectCode.XB,
                HeinObjectCode.TN,
                HeinObjectCode.CS,
                HeinObjectCode.XN,
                HeinObjectCode.MS,
                HeinObjectCode.HD,
                HeinObjectCode.TQ,
                HeinObjectCode.TA,
                HeinObjectCode.TY,
                HeinObjectCode.HG,
                HeinObjectCode.LS,
                HeinObjectCode.HS,
                HeinObjectCode.SV,
                HeinObjectCode.GB,
                HeinObjectCode.GD,
                HeinObjectCode.QN,
                HeinObjectCode.CA,
                HeinObjectCode.CY,
                HeinObjectCode.PV,
                HeinObjectCode.XV,
                HeinObjectCode.YE,
                HeinObjectCode.TV
                },
                false,
                false)},
            {"2", new HeinBenefitData("2", new List<string>{
                HeinObjectCode.CC,
                HeinObjectCode.TE,
                HeinObjectCode.CK,
                HeinObjectCode.CB,
                HeinObjectCode.KC,
                HeinObjectCode.HN,
                HeinObjectCode.DT,
                HeinObjectCode.DK,
                HeinObjectCode.XD,
                HeinObjectCode.BT,
                HeinObjectCode.TS,
                HeinObjectCode.HT,
                HeinObjectCode.TC,
                HeinObjectCode.CN,
                HeinObjectCode.DN,
                HeinObjectCode.HX,
                HeinObjectCode.CH,
                HeinObjectCode.NN,
                HeinObjectCode.TK,
                HeinObjectCode.HC,
                HeinObjectCode.XK,
                HeinObjectCode.TB,
                HeinObjectCode.NO,
                HeinObjectCode.CT,
                HeinObjectCode.XB,
                HeinObjectCode.TN,
                HeinObjectCode.CS,
                HeinObjectCode.XN,
                HeinObjectCode.MS,
                HeinObjectCode.HD,
                HeinObjectCode.TQ,
                HeinObjectCode.TA,
                HeinObjectCode.TY,
                HeinObjectCode.HG,
                HeinObjectCode.LS,
                HeinObjectCode.HS,
                HeinObjectCode.SV,
                HeinObjectCode.GB,
                HeinObjectCode.GD,
                HeinObjectCode.QN,
                HeinObjectCode.CA,
                HeinObjectCode.CY,
                HeinObjectCode.PV,
                HeinObjectCode.XV,
                HeinObjectCode.YE,
                HeinObjectCode.TV
                },
                true,
                true)},
            {"3", new HeinBenefitData("3", new List<string>{
                HeinObjectCode.CC,
                HeinObjectCode.TE,
                HeinObjectCode.CK,
                HeinObjectCode.CB,
                HeinObjectCode.KC,
                HeinObjectCode.HN,
                HeinObjectCode.DT,
                HeinObjectCode.DK,
                HeinObjectCode.XD,
                HeinObjectCode.BT,
                HeinObjectCode.TS,
                HeinObjectCode.HT,
                HeinObjectCode.TC,
                HeinObjectCode.CN,
                HeinObjectCode.DN,
                HeinObjectCode.HX,
                HeinObjectCode.CH,
                HeinObjectCode.NN,
                HeinObjectCode.TK,
                HeinObjectCode.HC,
                HeinObjectCode.XK,
                HeinObjectCode.TB,
                HeinObjectCode.NO,
                HeinObjectCode.CT,
                HeinObjectCode.XB,
                HeinObjectCode.TN,
                HeinObjectCode.CS,
                HeinObjectCode.XN,
                HeinObjectCode.MS,
                HeinObjectCode.HD,
                HeinObjectCode.TQ,
                HeinObjectCode.TA,
                HeinObjectCode.TY,
                HeinObjectCode.HG,
                HeinObjectCode.LS,
                HeinObjectCode.HS,
                HeinObjectCode.SV,
                HeinObjectCode.GB,
                HeinObjectCode.GD,
                HeinObjectCode.QN,
                HeinObjectCode.CA,
                HeinObjectCode.CY,
                HeinObjectCode.PV,
                HeinObjectCode.XV,
                HeinObjectCode.YE,
                HeinObjectCode.TV
                },
                true,
                true)},
            {"4", new HeinBenefitData("4", new List<string>{
                HeinObjectCode.CC,
                HeinObjectCode.TE,
                HeinObjectCode.CK,
                HeinObjectCode.CB,
                HeinObjectCode.KC,
                HeinObjectCode.HN,
                HeinObjectCode.DT,
                HeinObjectCode.DK,
                HeinObjectCode.XD,
                HeinObjectCode.BT,
                HeinObjectCode.TS,
                HeinObjectCode.HT,
                HeinObjectCode.TC,
                HeinObjectCode.CN,
                HeinObjectCode.DN,
                HeinObjectCode.HX,
                HeinObjectCode.CH,
                HeinObjectCode.NN,
                HeinObjectCode.TK,
                HeinObjectCode.HC,
                HeinObjectCode.XK,
                HeinObjectCode.TB,
                HeinObjectCode.NO,
                HeinObjectCode.CT,
                HeinObjectCode.XB,
                HeinObjectCode.TN,
                HeinObjectCode.CS,
                HeinObjectCode.XN,
                HeinObjectCode.MS,
                HeinObjectCode.HD,
                HeinObjectCode.TQ,
                HeinObjectCode.TA,
                HeinObjectCode.TY,
                HeinObjectCode.HG,
                HeinObjectCode.LS,
                HeinObjectCode.HS,
                HeinObjectCode.SV,
                HeinObjectCode.GB,
                HeinObjectCode.GD,
                HeinObjectCode.QN,
                HeinObjectCode.CA,
                HeinObjectCode.CY,
                HeinObjectCode.PV,
                HeinObjectCode.XV,
                HeinObjectCode.YE,
                HeinObjectCode.TV
                },
                true,
                true)},
            {"5", new HeinBenefitData("5", new List<string>{
                HeinObjectCode.CC,
                HeinObjectCode.TE,
                HeinObjectCode.CK,
                HeinObjectCode.CB,
                HeinObjectCode.KC,
                HeinObjectCode.HN,
                HeinObjectCode.DT,
                HeinObjectCode.DK,
                HeinObjectCode.XD,
                HeinObjectCode.BT,
                HeinObjectCode.TS,
                HeinObjectCode.HT,
                HeinObjectCode.TC,
                HeinObjectCode.CN,
                HeinObjectCode.DN,
                HeinObjectCode.HX,
                HeinObjectCode.CH,
                HeinObjectCode.NN,
                HeinObjectCode.TK,
                HeinObjectCode.HC,
                HeinObjectCode.XK,
                HeinObjectCode.TB,
                HeinObjectCode.NO,
                HeinObjectCode.CT,
                HeinObjectCode.XB,
                HeinObjectCode.TN,
                HeinObjectCode.CS,
                HeinObjectCode.XN,
                HeinObjectCode.MS,
                HeinObjectCode.HD,
                HeinObjectCode.TQ,
                HeinObjectCode.TA,
                HeinObjectCode.TY,
                HeinObjectCode.HG,
                HeinObjectCode.LS,
                HeinObjectCode.HS,
                HeinObjectCode.SV,
                HeinObjectCode.GB,
                HeinObjectCode.GD,
                HeinObjectCode.QN,
                HeinObjectCode.CA,
                HeinObjectCode.CY,
                HeinObjectCode.PV,
                HeinObjectCode.XV,
                HeinObjectCode.YE,
                HeinObjectCode.TV
                },
                true,
                true)}
        };

        internal static List<string> GetBenefit()
        {
            try
            {
                return HEIN_OBJECT_BENEFIT_STORE.Keys.ToList();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static List<string> GetObjectCodeWithBenefitCodes()
        {
            try
            {
                List<string> result = null;
                List<string> benefits = HeinObjectBenefitStore.GetBenefit();
                if (benefits != null && benefits.Count > 0)
                {
                    result = new List<string>();
                    foreach (string benefit in benefits)
                    {
                        HeinBenefitData benefitData = HEIN_OBJECT_BENEFIT_STORE[benefit];
                        if (benefitData.ProperHeinObjectCodes != null && benefitData.ProperHeinObjectCodes.Count > 0)
                        {
                            List<string> data = benefitData.ProperHeinObjectCodes.Select(o => o + benefit).ToList();
                            result.AddRange(data);
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

        internal static bool IsValidBenefit(string heinBenefitCode)
        {
            try
            {
                return HEIN_OBJECT_BENEFIT_STORE.ContainsKey(heinBenefitCode);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal static bool IsValid(HeinBenefitData heinBenefitData, string heinObjectCode)
        {
            try
            {
                //if (heinObjectCode == null || heinBenefitData == null || heinBenefitData.ProperHeinObjectCodes == null || !heinBenefitData.ProperHeinObjectCodes.Contains(heinObjectCode))
                //{
                //    LogSystem.Error("Ma 'Doi tuong' khong phu hop voi du lieu 'Quyen loi'");
                //    return false;
                //}
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal static HeinBenefitData GetHeinBenefitFromCardNumber(string heinCardNumber)
        {
            try
            {
                if (heinCardNumber == null || heinCardNumber.Length < 3)
                {
                    return null;
                }

                //chuyen doi tu the cu sang the moi
                heinCardNumber = HeinBenefitConverter.Convert(heinCardNumber);

                //Lay ma doi tuong BHYT (chinh la 2 ky tu dau cua so the BHYT)
                string heinObjectCode = heinCardNumber.Substring(0, 2);
                //Lay ma quyen loi BHYT (chinh la ky tu thu 3 cua so the BHYT)
                string heinBenefitCode = heinCardNumber.Substring(2, 1);
                HeinBenefitData heinBenefitData = HEIN_OBJECT_BENEFIT_STORE.ContainsKey(heinBenefitCode) ? HEIN_OBJECT_BENEFIT_STORE[heinBenefitCode] : null;
                return IsValid(heinBenefitData, heinObjectCode) ? heinBenefitData : null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        internal static bool IsChild(string heinCardNumber)
        {
            if (heinCardNumber == null || heinCardNumber.Length < 3)
            {
                return false;
            }

            //Lay ma doi tuong BHYT (chinh la 2 ky tu dau cua so the BHYT)
            string heinObjectCode = heinCardNumber.Substring(0, 2);
            return HeinObjectCode.TE.Equals(heinObjectCode);
        }

        internal static bool IsQn(string heinCardNumber)
        {
            if (heinCardNumber == null || heinCardNumber.Length < 3)
            {
                return false;
            }

            //Lay ma doi tuong BHYT (chinh la 2 ky tu dau cua so the BHYT)
            string heinObjectCode = heinCardNumber.Substring(0, 2);
            return HeinObjectCode.QN.Equals(heinObjectCode);
        }
    }
}
