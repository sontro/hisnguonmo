using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using SAR.MANAGER.Core.SarPrintTypeCfg;

namespace SAR.MANAGER.Core.SarPrintTypeCfg.Lock
{
    class SarPrintTypeCfgChangeLockBehaviorEv : BeanObjectBase, ISarPrintTypeCfgChangeLock
    {
        SAR_PRINT_TYPE_CFG entity;

        internal SarPrintTypeCfgChangeLockBehaviorEv(CommonParam param, SAR_PRINT_TYPE_CFG data)
            : base(param)
        {
            entity = data;
        }

        bool ISarPrintTypeCfgChangeLock.Run()
        {
            bool result = false;
            try
            {
                SAR_PRINT_TYPE_CFG raw = new SarPrintTypeCfgBO().Get<SAR_PRINT_TYPE_CFG>(entity.ID);
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
                    result = DAOWorker.SarPrintTypeCfgDAO.Update(raw);
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
