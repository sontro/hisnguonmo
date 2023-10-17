using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.SdaConfigApp.EventLog;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaConfigApp;

namespace SDA.MANAGER.Core.SdaConfigApp.Lock
{
    class SdaConfigAppChangeLockBehaviorEv : BeanObjectBase, ISdaConfigAppChangeLock
    {
        SDA_CONFIG_APP entity;

        internal SdaConfigAppChangeLockBehaviorEv(CommonParam param, SDA_CONFIG_APP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaConfigAppChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_CONFIG_APP raw = new SdaConfigAppBO().Get<SDA_CONFIG_APP>(entity.ID);
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
                    result = DAOWorker.SdaConfigAppDAO.Update(raw);
                    if (result) 
                    {
                        if (raw.IS_ACTIVE == IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__FALSE)
                        {
                            SdaConfigAppEventLogLock.Log(entity);
                        }
                        else
                        {
                            SdaConfigAppEventLogUnLock.Log(entity);
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
