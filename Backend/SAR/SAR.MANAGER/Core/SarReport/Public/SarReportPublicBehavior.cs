using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReport.Public
{
    class SarReportPublicBehavior : BeanObjectBase, ISarReportPublic
    {
        SAR_REPORT entity;
        SAR_REPORT raw;

        internal SarReportPublicBehavior(CommonParam param, SAR_REPORT data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportPublic.Run()
        {
            bool result = false;
            try
            {
                if (Check())
                {
                    raw.IS_PUBLIC = entity.IS_PUBLIC;
                    if (DAOWorker.SarReportDAO.Update(raw))
                    {
                        result = true;
                        entity = raw;
                    }
                    else
                    {
                        Logging("DAO update report that bai." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity), LogType.Error);
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.SarReport_DAOUpdatePublicReportThatBai);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && SarReportCheckVerifyValidData.Verify(param, entity);
                result = result && SarReportCheckVerifyId.Verify(param, entity.ID, ref raw);
                result = result && SarReportCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && SarReportCheckVerifyExistsCode.Verify(param, entity.REPORT_CODE, entity.ID);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
