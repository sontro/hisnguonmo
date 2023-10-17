using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaGroupType;

namespace SDA.MANAGER.Core.SdaGroupType.Lock
{
    class SdaGroupTypeChangeLockBehaviorEv : BeanObjectBase, ISdaGroupTypeChangeLock
    {
        SDA_GROUP_TYPE entity;

        internal SdaGroupTypeChangeLockBehaviorEv(CommonParam param, SDA_GROUP_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaGroupTypeChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_GROUP_TYPE raw = new SdaGroupTypeBO().Get<SDA_GROUP_TYPE>(entity.ID);
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
                    result = DAOWorker.SdaGroupTypeDAO.Update(raw);
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
