using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaEthnic;

namespace SDA.MANAGER.Core.SdaEthnic.Lock
{
    class SdaEthnicChangeLockBehaviorEv : BeanObjectBase, ISdaEthnicChangeLock
    {
        SDA_ETHNIC entity;

        internal SdaEthnicChangeLockBehaviorEv(CommonParam param, SDA_ETHNIC data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaEthnicChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_ETHNIC raw = new SdaEthnicBO().Get<SDA_ETHNIC>(entity.ID);
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
                    result = DAOWorker.SdaEthnicDAO.Update(raw);
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
