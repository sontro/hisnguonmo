using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaProvinceMap;

namespace SDA.MANAGER.Core.SdaProvinceMap.Lock
{
    class SdaProvinceMapChangeLockBehaviorEv : BeanObjectBase, ISdaProvinceMapChangeLock
    {
        SDA_PROVINCE_MAP entity;

        internal SdaProvinceMapChangeLockBehaviorEv(CommonParam param, SDA_PROVINCE_MAP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaProvinceMapChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_PROVINCE_MAP raw = new SdaProvinceMapBO().Get<SDA_PROVINCE_MAP>(entity.ID);
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
                    result = DAOWorker.SdaProvinceMapDAO.Update(raw);
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
