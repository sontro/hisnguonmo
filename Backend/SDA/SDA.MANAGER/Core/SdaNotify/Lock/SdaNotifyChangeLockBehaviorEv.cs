using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.SdaNotify.EventLog;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaNotify;

namespace SDA.MANAGER.Core.SdaNotify.Lock
{
    class SdaNotifyChangeLockBehaviorEv : BeanObjectBase, ISdaNotifyChangeLock
    {
        SDA_NOTIFY entity;

        internal SdaNotifyChangeLockBehaviorEv(CommonParam param, SDA_NOTIFY data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaNotifyChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_NOTIFY raw = new SdaNotifyBO().Get<SDA_NOTIFY>(entity.ID);
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
                    result = DAOWorker.SdaNotifyDAO.Update(raw);
                    if (result) 
                    {
                        if (raw.IS_ACTIVE == IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__FALSE)
                        {
                            SdaNotifyEventLogLock.Log(entity);
                        }
                        else
                        {
                            SdaNotifyEventLogUnLock.Log(entity);
                        }
                    }
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
