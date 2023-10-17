using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaDistrictMap;

namespace SDA.MANAGER.Core.SdaDistrictMap.Lock
{
    class SdaDistrictMapChangeLockBehaviorEv : BeanObjectBase, ISdaDistrictMapChangeLock
    {
        SDA_DISTRICT_MAP entity;

        internal SdaDistrictMapChangeLockBehaviorEv(CommonParam param, SDA_DISTRICT_MAP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaDistrictMapChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_DISTRICT_MAP raw = new SdaDistrictMapBO().Get<SDA_DISTRICT_MAP>(entity.ID);
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
                    result = DAOWorker.SdaDistrictMapDAO.Update(raw);
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
