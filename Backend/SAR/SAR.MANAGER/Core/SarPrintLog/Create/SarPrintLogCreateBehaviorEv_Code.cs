using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintLog.Create
{
    class SarPrintLogCreateBehaviorEv : BeanObjectBase, ISarPrintLogCreate
    {
        SAR_PRINT_LOG entity;

        internal SarPrintLogCreateBehaviorEv(CommonParam param, SAR_PRINT_LOG data)
            : base(param)
        {
            entity = data;
        }

        bool ISarPrintLogCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarPrintLogDAO.Create(entity);
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
                result = result && SarPrintLogCheckVerifyValidData.Verify(param, entity);
                result = result && SarPrintLogCheckVerifyExistsCode.Verify(param, entity.PRINT_LOG_CODE, null);
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
