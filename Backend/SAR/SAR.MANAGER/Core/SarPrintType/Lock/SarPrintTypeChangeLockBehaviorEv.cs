using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using SAR.MANAGER.Core.SarPrintType;

namespace SAR.MANAGER.Core.SarPrintType.Lock
{
    class SarPrintTypeChangeLockBehaviorEv : BeanObjectBase, ISarPrintTypeChangeLock
    {
        SAR_PRINT_TYPE entity;

        internal SarPrintTypeChangeLockBehaviorEv(CommonParam param, SAR_PRINT_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool ISarPrintTypeChangeLock.Run()
        {
            bool result = false;
            try
            {
                SAR_PRINT_TYPE raw = new SarPrintTypeBO().Get<SAR_PRINT_TYPE>(entity.ID);
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
                    result = DAOWorker.SarPrintTypeDAO.Update(raw);
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
