using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintTypeCfg.Update
{
    class SarPrintTypeCfgUpdateBehaviorEv : BeanObjectBase, ISarPrintTypeCfgUpdate
    {
        SAR_PRINT_TYPE_CFG current;
        SAR_PRINT_TYPE_CFG entity;

        internal SarPrintTypeCfgUpdateBehaviorEv(CommonParam param, SAR_PRINT_TYPE_CFG data)
            : base(param)
        {
            entity = data;
        }

        bool ISarPrintTypeCfgUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarPrintTypeCfgDAO.Update(entity);
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
                result = result && SarPrintTypeCfgCheckVerifyValidData.Verify(param, entity);
                result = result && SarPrintTypeCfgCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
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
