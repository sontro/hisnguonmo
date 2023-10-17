using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
//using SDA.MANAGER.Core.SdaModuleField.EventLog;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaModuleField;

namespace SDA.MANAGER.Core.SdaModuleField.Lock
{
    class SdaModuleFieldChangeLockBehaviorEv : BeanObjectBase, ISdaModuleFieldChangeLock
    {
        SDA_MODULE_FIELD entity;

        internal SdaModuleFieldChangeLockBehaviorEv(CommonParam param, SDA_MODULE_FIELD data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaModuleFieldChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_MODULE_FIELD raw = new SdaModuleFieldBO().Get<SDA_MODULE_FIELD>(entity.ID);
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
                    result = DAOWorker.SdaModuleFieldDAO.Update(raw);
                    if (result) 
                    {
                        if (raw.IS_ACTIVE == IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__FALSE)
                        {
                            //SdaModuleFieldEventLogLock.Log(entity);
                        }
                        else
                        {
                            //SdaModuleFieldEventLogUnLock.Log(entity);
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
