using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrint.Delete
{
    class SarPrintDeleteBehaviorEv : BeanObjectBase, ISarPrintDelete
    {
        SAR_PRINT entity;

        internal SarPrintDeleteBehaviorEv(CommonParam param, SAR_PRINT data)
            : base(param)
        {
            entity = data;
        }

        bool ISarPrintDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarPrintDAO.Truncate(entity);
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
                result = result && SarPrintCheckVerifyIsUnlock.Verify(param, entity.ID);
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
