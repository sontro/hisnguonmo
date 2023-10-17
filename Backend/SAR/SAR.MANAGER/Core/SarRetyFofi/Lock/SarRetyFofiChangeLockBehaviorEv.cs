using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using SAR.MANAGER.Core.SarRetyFofi;

namespace SAR.MANAGER.Core.SarRetyFofi.Lock
{
    class SarRetyFofiChangeLockBehaviorEv : BeanObjectBase, ISarRetyFofiChangeLock
    {
        SAR_RETY_FOFI entity;

        internal SarRetyFofiChangeLockBehaviorEv(CommonParam param, SAR_RETY_FOFI data)
            : base(param)
        {
            entity = data;
        }

        bool ISarRetyFofiChangeLock.Run()
        {
            bool result = false;
            try
            {
                SAR_RETY_FOFI raw = new SarRetyFofiBO().Get<SAR_RETY_FOFI>(entity.ID);
                if (raw != null)
                {
                    if (raw.IS_ACTIVE.HasValue && raw.IS_ACTIVE == IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__TRUE)
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__FALSE;
                    }
                    else
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__TRUE;
                    }
                    result = DAOWorker.SarRetyFofiDAO.Update(raw);
                    if (result) entity.IS_ACTIVE = raw.IS_ACTIVE;
                }
                else
                {
                    BugUtil.SetBugCode(param, SAR.LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
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
