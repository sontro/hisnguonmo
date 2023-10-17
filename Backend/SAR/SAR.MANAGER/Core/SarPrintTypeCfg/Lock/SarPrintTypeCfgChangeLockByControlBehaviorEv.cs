using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.SarPrintTypeCfg.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAR.MANAGER.Core.SarPrintTypeCfg.Lock
{
    class SarPrintTypeCfgChangeLockByControlBehaviorEv : BeanObjectBase, ISarPrintTypeCfgChangeLock
    {
        private SDO.ChangeLockByControlSDO entity;
        private List<SAR_PRINT_TYPE_CFG> RollbackData;

        public SarPrintTypeCfgChangeLockByControlBehaviorEv(Inventec.Core.CommonParam param, SDO.ChangeLockByControlSDO data)
            : base(param)
        {
            this.entity = data;
        }

        bool ISarPrintTypeCfgChangeLock.Run()
        {
            bool result = false;
            try
            {
                SarPrintTypeCfgFilterQuery filter = new SarPrintTypeCfgFilterQuery();
                filter.APP_CODE__EXACT = entity.AppCode;
                filter.BRANCH_CODE__EXACT = entity.BranckCode;
                filter.CONTROL_PATH__EXACT = entity.ControlPath;
                filter.MODULE_LINK__EXACT = entity.ModuleLink;

                List<SAR_PRINT_TYPE_CFG> raw = new SarPrintTypeCfgBO().Get<List<SAR_PRINT_TYPE_CFG>>(filter);
                if (raw != null && raw.Count > 0)
                {
                    RollbackData = new List<SAR_PRINT_TYPE_CFG>();
                    RollbackData.AddRange(raw);

                    short isActive = IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__TRUE;
                    if (raw.First().IS_ACTIVE.HasValue && raw.First().IS_ACTIVE == IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__TRUE)
                    {
                        isActive = IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__FALSE;
                    }

                    foreach (var item in raw)
                    {
                        //if (item.IS_ACTIVE.HasValue && item.IS_ACTIVE == IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__TRUE)
                        //{
                        //    item.IS_ACTIVE = IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__FALSE;
                        //}
                        //else
                        //{
                        //    item.IS_ACTIVE = IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__TRUE;
                        //}
                        item.IS_ACTIVE = isActive;
                    }

                    result = DAOWorker.SarPrintTypeCfgDAO.UpdateList(raw);
                    //if (result) entity.IS_ACTIVE = raw.IS_ACTIVE;
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
                Rollback();
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                if (RollbackData != null && RollbackData.Count > 0)
                {
                    DAOWorker.SarPrintTypeCfgDAO.UpdateList(RollbackData);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
