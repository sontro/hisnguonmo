using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaHideControl;

namespace SDA.MANAGER.Core.SdaHideControl.Lock
{
    class SdaHideControlChangeLockBehaviorEv : BeanObjectBase, ISdaHideControlChangeLock
    {
        SDA_HIDE_CONTROL entity;

        internal SdaHideControlChangeLockBehaviorEv(CommonParam param, SDA_HIDE_CONTROL data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaHideControlChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_HIDE_CONTROL raw = new SdaHideControlBO().Get<SDA_HIDE_CONTROL>(entity.ID);
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
                    result = DAOWorker.SdaHideControlDAO.Update(raw);
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
