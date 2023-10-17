using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaCommuneMap;

namespace SDA.MANAGER.Core.SdaCommuneMap.Lock
{
    class SdaCommuneMapChangeLockBehaviorEv : BeanObjectBase, ISdaCommuneMapChangeLock
    {
        SDA_COMMUNE_MAP entity;

        internal SdaCommuneMapChangeLockBehaviorEv(CommonParam param, SDA_COMMUNE_MAP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaCommuneMapChangeLock.Run()
        {
            bool result = false;
            try
            {
                SDA_COMMUNE_MAP raw = new SdaCommuneMapBO().Get<SDA_COMMUNE_MAP>(entity.ID);
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
                    result = DAOWorker.SdaCommuneMapDAO.Update(raw);
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
