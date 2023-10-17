using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaSqlParam;

namespace SDA.MANAGER.Core.SdaSqlParam.Lock
{
    class SdaSqlParamChangeLockBehaviorEv : BeanObjectBase, ISdaSqlParamChangeLock
    {
        SDA_SQL_PARAM entity;

        internal SdaSqlParamChangeLockBehaviorEv(CommonParam param, SDA_SQL_PARAM data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaSqlParamChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_SQL_PARAM raw = new SdaSqlParamBO().Get<SDA_SQL_PARAM>(entity.ID);
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
                    result = DAOWorker.SdaSqlParamDAO.Update(raw);
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
