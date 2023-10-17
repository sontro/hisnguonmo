using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaCommune;

namespace SDA.MANAGER.Core.SdaCommune.Lock
{
    class SdaCommuneChangeLockBehaviorEv : BeanObjectBase, ISdaCommuneChangeLock
    {
        SDA_COMMUNE entity;

        internal SdaCommuneChangeLockBehaviorEv(CommonParam param, SDA_COMMUNE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaCommuneChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_COMMUNE raw = new SdaCommuneBO().Get<SDA_COMMUNE>(entity.ID);
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
                    result = DAOWorker.SdaCommuneDAO.Update(raw);
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
