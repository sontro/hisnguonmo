using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.SdaDeleteData.EventLog;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaDeleteData;

namespace SDA.MANAGER.Core.SdaDeleteData.Lock
{
    class SdaDeleteDataChangeLockBehaviorEv : BeanObjectBase, ISdaDeleteDataChangeLock
    {
        SDA_DELETE_DATA entity;

        internal SdaDeleteDataChangeLockBehaviorEv(CommonParam param, SDA_DELETE_DATA data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaDeleteDataChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_DELETE_DATA raw = new SdaDeleteDataBO().Get<SDA_DELETE_DATA>(entity.ID);
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
                    result = DAOWorker.SdaDeleteDataDAO.Update(raw);
                    if (result) 
                    {
                        if (raw.IS_ACTIVE == IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__FALSE)
                        {
                            SdaDeleteDataEventLogLock.Log(entity);
                        }
                        else
                        {
                            SdaDeleteDataEventLogUnLock.Log(entity);
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
