using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using SAR.MANAGER.Core.SarReportTemplate;

namespace SAR.MANAGER.Core.SarReportTemplate.Lock
{
    class SarReportTemplateChangeLockBehaviorEv : BeanObjectBase, ISarReportTemplateChangeLock
    {
        SAR_REPORT_TEMPLATE entity;

        internal SarReportTemplateChangeLockBehaviorEv(CommonParam param, SAR_REPORT_TEMPLATE data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportTemplateChangeLock.Run()
        {
            bool result = false;
            try
            {
                SAR_REPORT_TEMPLATE raw = new SarReportTemplateBO().Get<SAR_REPORT_TEMPLATE>(entity.ID);
                if (raw != null)
                {
                    if (raw.IS_ACTIVE.HasValue && raw.IS_ACTIVE == IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__TRUE)
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__FALSE;
                    }
                    else
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__TRUE;
                    }
                    result = DAOWorker.SarReportTemplateDAO.Update(raw);
                    if (result) entity.IS_ACTIVE = raw.IS_ACTIVE;
                }
                else
                {
                    BugUtil.SetBugCode(param, SAR.LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
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
    }
}
