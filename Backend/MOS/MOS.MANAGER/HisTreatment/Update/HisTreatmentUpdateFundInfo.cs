using AutoMapper;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentUpdate : BusinessBase
    {
        internal bool UpdateFundInfo(HIS_TREATMENT data, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT raw = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLockHein(raw);
                valid = valid && CheckFundTime(data.FUND_FROM_TIME, data.FUND_TO_TIME);
                if (valid)
                {
                    Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();

                    HIS_TREATMENT before = Mapper.Map<HIS_TREATMENT>(raw);
                    this.beforeUpdateHisTreatments.Add(before);

                    raw.FUND_ID = data.FUND_ID;
                    raw.FUND_BUDGET = data.FUND_BUDGET;
                    raw.FUND_COMPANY_NAME = data.FUND_COMPANY_NAME;
                    raw.FUND_CUSTOMER_NAME = data.FUND_CUSTOMER_NAME;
                    raw.FUND_FROM_TIME = data.FUND_FROM_TIME;
                    raw.FUND_ISSUE_TIME = data.FUND_ISSUE_TIME;
                    raw.FUND_NUMBER = data.FUND_NUMBER;
                    raw.FUND_PAY_TIME = data.FUND_PAY_TIME;
                    raw.FUND_TO_TIME = data.FUND_TO_TIME;
                    raw.FUND_TYPE_NAME = data.FUND_TYPE_NAME;

                    if (!DAOWorker.HisTreatmentDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatment that bai." + LogUtil.TraceData("data", data));
                    }
                    resultData = raw;
                    result = true;

                    //new EventLogGenerator(EventLog.Enum.HisTreatment_SuaThongTinFund, this.LogDataFund(before), this.LogDataFund(resultData))
                    //    .TreatmentCode(resultData.TREATMENT_CODE)
                    //    .Run();
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

        private bool CheckFundTime(long? fundFromTime, long? fundToTime)
        {
            bool result = true;
            try
            {
                if (fundFromTime.HasValue && fundToTime.HasValue && fundToTime.Value < fundFromTime.Value)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ThoigianHieuLucTuLonHonThoiGianHieuLucDen);
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
