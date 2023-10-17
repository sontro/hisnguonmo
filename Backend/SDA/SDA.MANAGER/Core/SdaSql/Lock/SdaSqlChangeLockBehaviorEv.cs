using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaSql;

namespace SDA.MANAGER.Core.SdaSql.Lock
{
    class SdaSqlChangeLockBehaviorEv : BeanObjectBase, ISdaSqlChangeLock
    {
        SDA_SQL entity;

        internal SdaSqlChangeLockBehaviorEv(CommonParam param, SDA_SQL data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaSqlChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_SQL raw = new SdaSqlBO().Get<SDA_SQL>(entity.ID);
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
                    result = DAOWorker.SdaSqlDAO.Update(raw);
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
