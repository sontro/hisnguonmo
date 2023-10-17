using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaTrouble;

namespace SDA.MANAGER.Core.SdaTrouble.Lock
{
    class SdaTroubleChangeLockBehaviorEv : BeanObjectBase, ISdaTroubleChangeLock
    {
        SDA_TROUBLE entity;

        internal SdaTroubleChangeLockBehaviorEv(CommonParam param, SDA_TROUBLE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaTroubleChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_TROUBLE raw = new SdaTroubleBO().Get<SDA_TROUBLE>(entity.ID);
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
                    result = DAOWorker.SdaTroubleDAO.Update(raw);
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
