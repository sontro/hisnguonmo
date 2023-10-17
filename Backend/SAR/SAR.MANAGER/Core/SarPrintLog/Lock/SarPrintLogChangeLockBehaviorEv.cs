using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using SAR.MANAGER.Core.SarPrintLog;

namespace SAR.MANAGER.Core.SarPrintLog.Lock
{
    class SarPrintLogChangeLockBehaviorEv : BeanObjectBase, ISarPrintLogChangeLock
    {
        SAR_PRINT_LOG entity;

        internal SarPrintLogChangeLockBehaviorEv(CommonParam param, SAR_PRINT_LOG data)
            : base(param)
        {
            entity = data;
        }

        bool ISarPrintLogChangeLock.Run()
        {
            bool result = false;
            try
            {
                SAR_PRINT_LOG raw = new SarPrintLogBO().Get<SAR_PRINT_LOG>(entity.ID);
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
                    result = DAOWorker.SarPrintLogDAO.Update(raw);
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
