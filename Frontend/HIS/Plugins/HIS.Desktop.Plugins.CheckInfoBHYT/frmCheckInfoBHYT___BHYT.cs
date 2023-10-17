using His.Bhyt.InsuranceExpertise;
using His.Bhyt.InsuranceExpertise.LDO;
using MOS.EFMODEL.DataModels;
using System;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.Library.CheckHeinGOV;


namespace HIS.Desktop.Plugins.CheckInfoBHYT
{
    public partial class frmCheckInfoBHYT : HIS.Desktop.Utility.FormBase
    {
        private async Task CheckTTFull(V_HIS_PATIENT_TYPE_ALTER _patientTypeAlter)
        {
            rsDataBHYT = new ResultDataADO();
            try
            {
                ApiInsuranceExpertise apiInsuranceExpertise = new ApiInsuranceExpertise();
                CheckHistoryLDO checkHistoryLDO = new CheckHistoryLDO();
                checkHistoryLDO.maThe = _patientTypeAlter.HEIN_CARD_NUMBER;
                checkHistoryLDO.ngaySinh = Inventec.Common.DateTime.Convert.TimeNumberToDateString(_HisPatient.DOB);
                checkHistoryLDO.hoTen = _HisPatient.VIR_PATIENT_NAME;
                if (!string.IsNullOrEmpty(BHXHLoginCFG.USERNAME)
                    || !string.IsNullOrEmpty(BHXHLoginCFG.PASSWORD)
                    || !string.IsNullOrEmpty(BHXHLoginCFG.ADDRESS))
                {
                    rsDataBHYT.ResultHistoryLDO = await apiInsuranceExpertise.CheckHistory(BHXHLoginCFG.USERNAME, BHXHLoginCFG.PASSWORD, BHXHLoginCFG.ADDRESS, checkHistoryLDO, BHXHLoginCFG.ADDRESS_OPTION);
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Error("Kiem tra lai cau hinh 'HIS.CHECK_HEIN_CARD.BHXH.LOGIN.USER_PASS'  -- 'HIS.CHECK_HEIN_CARD.BHXH__ADDRESS' ==>BHYT");
                }
            }
            catch (Exception ex)
            {
                rsDataBHYT = null;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public class GenderConvert
        {
            public static string TextToNumber(string ge)
            {
                return (ge == "Nữ") ? "2" : "1";
            }

            public static string HisToHein(string ge)
            {
                return (ge == "1") ? "2" : "1";
            }

            public static long HeinToHisNumber(string ge)
            {
                return (ge == "1" ? IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__MALE : IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE);
            }
        }
    }
}
