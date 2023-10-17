using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.SdaConfigAppUser.EventLog;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaConfigAppUser;

namespace SDA.MANAGER.Core.SdaConfigAppUser.Lock
{
    class SdaConfigAppUserChangeLockBehaviorEv : BeanObjectBase, ISdaConfigAppUserChangeLock
    {
        SDA_CONFIG_APP_USER entity;

        internal SdaConfigAppUserChangeLockBehaviorEv(CommonParam param, SDA_CONFIG_APP_USER data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaConfigAppUserChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_CONFIG_APP_USER raw = new SdaConfigAppUserBO().Get<SDA_CONFIG_APP_USER>(entity.ID);
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
                    result = DAOWorker.SdaConfigAppUserDAO.Update(raw);
                    if (result) 
                    {
                        if (raw.IS_ACTIVE == IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__FALSE)
                        {
                            SdaConfigAppUserEventLogLock.Log(entity);
                        }
                        else
                        {
                            SdaConfigAppUserEventLogUnLock.Log(entity);
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
