using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaProvince;

namespace SDA.MANAGER.Core.SdaProvince.Lock
{
    class SdaProvinceChangeLockBehaviorEv : BeanObjectBase, ISdaProvinceChangeLock
    {
        SDA_PROVINCE entity;

        internal SdaProvinceChangeLockBehaviorEv(CommonParam param, SDA_PROVINCE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaProvinceChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_PROVINCE raw = new SdaProvinceBO().Get<SDA_PROVINCE>(entity.ID);
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
                    result = DAOWorker.SdaProvinceDAO.Update(raw);
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
