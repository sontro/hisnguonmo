using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaEventLog;

namespace SDA.MANAGER.Core.SdaEventLog.Lock
{
    class SdaEventLogChangeLockBehaviorEv : BeanObjectBase, ISdaEventLogChangeLock
    {
        SDA_EVENT_LOG entity;

        internal SdaEventLogChangeLockBehaviorEv(CommonParam param, SDA_EVENT_LOG data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaEventLogChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_EVENT_LOG raw = new SdaEventLogBO().Get<SDA_EVENT_LOG>(entity.ID);
                if (raw != null)
                {
                    if (raw.IS_ACTIVE.HasValue && raw.IS_ACTIVE == IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__TRUE)
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__FALSE;
                    }
                    else
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__TRUE;
                    }
                    result = DAOWorker.SdaEventLogDAO.Update(raw);
                    if (result) entity.IS_ACTIVE = raw.IS_ACTIVE;
                }
                else
                {
                    BugUtil.SetBugCode(param, SDA.LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
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
