using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaCustomizeUi;

namespace SDA.MANAGER.Core.SdaCustomizeUi.Lock
{
    class SdaCustomizeUiChangeLockBehaviorEv : BeanObjectBase, ISdaCustomizeUiChangeLock
    {
        SDA_CUSTOMIZE_UI entity;

        internal SdaCustomizeUiChangeLockBehaviorEv(CommonParam param, SDA_CUSTOMIZE_UI data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaCustomizeUiChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_CUSTOMIZE_UI raw = new SdaCustomizeUiBO().Get<SDA_CUSTOMIZE_UI>(entity.ID);
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
                    result = DAOWorker.SdaCustomizeUiDAO.Update(raw);
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
