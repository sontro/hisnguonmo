using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaConfig;

namespace SDA.MANAGER.Core.SdaConfig.Lock
{
    class SdaConfigChangeLockBehaviorEv : BeanObjectBase, ISdaConfigChangeLock
    {
        SDA_CONFIG entity;

        internal SdaConfigChangeLockBehaviorEv(CommonParam param, SDA_CONFIG data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaConfigChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_CONFIG raw = new SdaConfigBO().Get<SDA_CONFIG>(entity.ID);
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
                    result = DAOWorker.SdaConfigDAO.Update(raw);
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
