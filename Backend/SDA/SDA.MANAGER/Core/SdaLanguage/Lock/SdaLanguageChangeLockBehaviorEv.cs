using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.SdaLanguage.EventLog;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaLanguage;

namespace SDA.MANAGER.Core.SdaLanguage.Lock
{
    class SdaLanguageChangeLockBehaviorEv : BeanObjectBase, ISdaLanguageChangeLock
    {
        SDA_LANGUAGE entity;

        internal SdaLanguageChangeLockBehaviorEv(CommonParam param, SDA_LANGUAGE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaLanguageChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_LANGUAGE raw = new SdaLanguageBO().Get<SDA_LANGUAGE>(entity.ID);
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
                    result = DAOWorker.SdaLanguageDAO.Update(raw);
                    if (result) 
                    {
                        if (raw.IS_ACTIVE == IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__FALSE)
                        {
                            SdaLanguageEventLogLock.Log(entity);
                        }
                        else
                        {
                            SdaLanguageEventLogUnLock.Log(entity);
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
