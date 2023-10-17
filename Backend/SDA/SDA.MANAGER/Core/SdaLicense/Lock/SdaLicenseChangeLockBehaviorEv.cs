using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaLicense;

namespace SDA.MANAGER.Core.SdaLicense.Lock
{
    class SdaLicenseChangeLockBehaviorEv : BeanObjectBase, ISdaLicenseChangeLock
    {
        SDA_LICENSE entity;

        internal SdaLicenseChangeLockBehaviorEv(CommonParam param, SDA_LICENSE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaLicenseChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_LICENSE raw = new SdaLicenseBO().Get<SDA_LICENSE>(entity.ID);
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
                    result = DAOWorker.SdaLicenseDAO.Update(raw);
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
