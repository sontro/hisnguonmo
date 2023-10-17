using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaReligion;

namespace SDA.MANAGER.Core.SdaReligion.Lock
{
    class SdaReligionChangeLockBehaviorEv : BeanObjectBase, ISdaReligionChangeLock
    {
        SDA_RELIGION entity;

        internal SdaReligionChangeLockBehaviorEv(CommonParam param, SDA_RELIGION data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaReligionChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_RELIGION raw = new SdaReligionBO().Get<SDA_RELIGION>(entity.ID);
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
                    result = DAOWorker.SdaReligionDAO.Update(raw);
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
