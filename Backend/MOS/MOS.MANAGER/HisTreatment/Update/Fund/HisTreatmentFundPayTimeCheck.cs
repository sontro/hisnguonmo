using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update.Fund
{
    class HisTreatmentFundPayTimeCheck : BusinessBase
    {
        internal HisTreatmentFundPayTimeCheck()
            : base()
        {

        }

        internal HisTreatmentFundPayTimeCheck(CommonParam param)
            : base(param)
        {

        }


        internal bool HasFundPayTime(List<HIS_TREATMENT> listRaw)
        {
            bool valid = true;
            try
            {
                List<string> notPayTimeCodes = listRaw != null ? listRaw.Where(o => !o.FUND_PAY_TIME.HasValue).Select(s => s.TREATMENT_CODE).ToList() : null;
                if (IsNotNullOrEmpty(notPayTimeCodes))
                {
                    string codes = String.Join(",", notPayTimeCodes);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_QuyChuaThanhToan, codes);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool HasNoFundPayTime(List<HIS_TREATMENT> listRaw)
        {
            bool valid = true;
            try
            {
                List<string> hasPayTimeCodes = listRaw != null ? listRaw.Where(o => o.FUND_PAY_TIME.HasValue).Select(s => s.TREATMENT_CODE).ToList() : null;
                if (IsNotNullOrEmpty(hasPayTimeCodes))
                {
                    string codes = String.Join(",", hasPayTimeCodes);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_QuyDaThanhToan, codes);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool HasFund(List<HIS_TREATMENT> listRaw)
        {
            bool valid = true;
            try
            {
                List<string> notFundCodes = listRaw != null ? listRaw.Where(o => !o.FUND_ID.HasValue).Select(s => s.TREATMENT_CODE).ToList() : null;
                if (IsNotNullOrEmpty(notFundCodes))
                {
                    string codes = String.Join(",", notFundCodes);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_KhongDuocQuyThanhToan, codes);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

    }
}
