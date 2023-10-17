using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintLog.Update
{
    class SarPrintLogUpdateBehaviorEv : BeanObjectBase, ISarPrintLogUpdate
    {
        SAR_PRINT_LOG current;
        SAR_PRINT_LOG entity;

        internal SarPrintLogUpdateBehaviorEv(CommonParam param, SAR_PRINT_LOG data)
            : base(param)
        {
            entity = data;
        }

        bool ISarPrintLogUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarPrintLogDAO.Update(entity);
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
                result = result && SarPrintLogCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
                result = result && SarPrintLogCheckVerifyExistsCode.Verify(param, entity.PRINT_LOG_CODE, entity.ID);
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
