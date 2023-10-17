using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintTypeCfg.Delete
{
    class SarPrintTypeCfgDeleteBehaviorEv : BeanObjectBase, ISarPrintTypeCfgDelete
    {
        SAR_PRINT_TYPE_CFG entity;

        internal SarPrintTypeCfgDeleteBehaviorEv(CommonParam param, SAR_PRINT_TYPE_CFG data)
            : base(param)
        {
            entity = data;
        }

        bool ISarPrintTypeCfgDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarPrintTypeCfgDAO.Truncate(entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && SarPrintTypeCfgCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
