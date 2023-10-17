using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintLog.Delete
{
    class SarPrintLogDeleteBehaviorEv : BeanObjectBase, ISarPrintLogDelete
    {
        SAR_PRINT_LOG entity;

        internal SarPrintLogDeleteBehaviorEv(CommonParam param, SAR_PRINT_LOG data)
            : base(param)
        {
            entity = data;
        }

        bool ISarPrintLogDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarPrintLogDAO.Truncate(entity);
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
                result = result && SarPrintLogCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
