using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.SdaTranslate.EventLog;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaTranslate;

namespace SDA.MANAGER.Core.SdaTranslate.Lock
{
    class SdaTranslateChangeLockBehaviorEv : BeanObjectBase, ISdaTranslateChangeLock
    {
        SDA_TRANSLATE entity;

        internal SdaTranslateChangeLockBehaviorEv(CommonParam param, SDA_TRANSLATE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaTranslateChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_TRANSLATE raw = new SdaTranslateBO().Get<SDA_TRANSLATE>(entity.ID);
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
                    result = DAOWorker.SdaTranslateDAO.Update(raw);
                    if (result) 
                    {
                        if (raw.IS_ACTIVE == IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__FALSE)
                        {
                            SdaTranslateEventLogLock.Log(entity);
                        }
                        else
                        {
                            SdaTranslateEventLogUnLock.Log(entity);
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
