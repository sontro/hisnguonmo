using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaDistrict;

namespace SDA.MANAGER.Core.SdaDistrict.Lock
{
    class SdaDistrictChangeLockBehaviorEv : BeanObjectBase, ISdaDistrictChangeLock
    {
        SDA_DISTRICT entity;

        internal SdaDistrictChangeLockBehaviorEv(CommonParam param, SDA_DISTRICT data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaDistrictChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_DISTRICT raw = new SdaDistrictBO().Get<SDA_DISTRICT>(entity.ID);
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
                    result = DAOWorker.SdaDistrictDAO.Update(raw);
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
