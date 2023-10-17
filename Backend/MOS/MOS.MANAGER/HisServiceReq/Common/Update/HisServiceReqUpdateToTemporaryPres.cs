using EMR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq
{
    partial class HisServiceReqUpdate : BusinessBase
    {
        /// <summary>
        /// Update IS_TEMPORARY_PRES = 1, TRACKING_ID  = null
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool UpdateToTemporaryPres(HIS_SERVICE_REQ data, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_SERVICE_REQ raw = null;
                HIS_TREATMENT treatment = null;
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                HisTreatmentCheck checkerTreatment = new HisTreatmentCheck(param);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checkerTreatment.HasTreatmentFinished(raw.TREATMENT_ID, ref treatment);
                valid = valid && validateUser(raw);
                valid = valid && checker.IsTypeDONDT(raw);
                valid = valid && validateEMRtreatment(raw);
                if (valid)
                {
                    raw.IS_TEMPORARY_PRES = MOS.UTILITY.Constant.IS_TRUE;
                    raw.TRACKING_ID = null;
                    if (this.Update(raw, true))
                    {
                        resultData = raw;
                        result = true;
                        this.WriteEventLog(treatment, raw);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void WriteEventLog(HIS_TREATMENT treatment, HIS_SERVICE_REQ raw)
        {
            new EventLogGenerator(EventLog.Enum.HisServiceReq_ChuyenDonDieuTriThanhDonTam).TreatmentCode(treatment.TREATMENT_CODE).ServiceReqCode(raw.SERVICE_REQ_CODE).Run();
        }

        private bool validateEMRtreatment(HIS_SERVICE_REQ raw)
        {
            bool result = true;
            try
            {
                string TokenCode = ResourceTokenManager.GetTokenCode();
                if (!String.IsNullOrWhiteSpace(TokenCode) && !String.IsNullOrWhiteSpace(raw.TDL_TREATMENT_CODE))
                {
                    CommonParam emrParam = new CommonParam();
                    var cosumer = ApiConsumerManager.ApiConsumerStore.EmrConsumer;
                    cosumer.SetTokenCode(TokenCode);
                    EMR.Filter.EmrTreatmentFilter filter = new EMR.Filter.EmrTreatmentFilter();
                    filter.TREATMENT_CODE__EXACT = raw.TDL_TREATMENT_CODE;

                    var apiresult = cosumer.Get<Inventec.Core.ApiResultObject<List<EMR_TREATMENT>>>(EMR.URI.EmrTreatment.GET, emrParam, filter);
                    if (IsNotNull(apiresult) && apiresult.Success && IsNotNullOrEmpty(apiresult.Data))
                    {
                        EMR_TREATMENT emrTreatment = apiresult.Data.First();
                        if (emrTreatment.STORE_TIME > 0)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HoSoBenhAnDaDong);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool validateUser(HIS_SERVICE_REQ data)
        {
            bool result = true;
            try
            {
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                if (!HisEmployeeUtil.IsAdmin(loginName) && loginName != data.REQUEST_LOGINNAME)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_TaiKhoanDangNhapKhongPhaiLaAdminHoacNguoiChiDinhKeDon);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
