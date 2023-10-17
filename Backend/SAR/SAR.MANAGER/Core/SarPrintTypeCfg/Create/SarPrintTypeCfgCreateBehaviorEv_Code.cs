using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintTypeCfg.Create
{
    class SarPrintTypeCfgCreateBehaviorEv : BeanObjectBase, ISarPrintTypeCfgCreate
    {
        SAR_PRINT_TYPE_CFG entity;

        internal SarPrintTypeCfgCreateBehaviorEv(CommonParam param, SAR_PRINT_TYPE_CFG data)
            : base(param)
        {
            entity = data;
        }

        bool ISarPrintTypeCfgCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarPrintTypeCfgDAO.Create(entity);
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
                result = result && SarPrintTypeCfgCheckVerifyExistsCode.Verify(param, entity.PRINT_TYPE_CFG_CODE, null);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
