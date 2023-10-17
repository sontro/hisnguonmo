using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaNational;

namespace SDA.MANAGER.Core.SdaNational.Lock
{
    class SdaNationalChangeLockBehaviorEv : BeanObjectBase, ISdaNationalChangeLock
    {
        SDA_NATIONAL entity;

        internal SdaNationalChangeLockBehaviorEv(CommonParam param, SDA_NATIONAL data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaNationalChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_NATIONAL raw = new SdaNationalBO().Get<SDA_NATIONAL>(entity.ID);
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
                    result = DAOWorker.SdaNationalDAO.Update(raw);
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
