using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintType.Delete
{
    class SarPrintTypeDeleteBehaviorEv : BeanObjectBase, ISarPrintTypeDelete
    {
        SAR_PRINT_TYPE entity;

        internal SarPrintTypeDeleteBehaviorEv(CommonParam param, SAR_PRINT_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool ISarPrintTypeDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarPrintTypeDAO.Truncate(entity);
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
                result = result && SarPrintTypeCheckVerifyIsUnlock.Verify(param, entity.ID);
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
