using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintType.Update
{
    class SarPrintTypeUpdateBehaviorEv : BeanObjectBase, ISarPrintTypeUpdate
    {
        SAR_PRINT_TYPE entity;

        internal SarPrintTypeUpdateBehaviorEv(CommonParam param, SAR_PRINT_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool ISarPrintTypeUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarPrintTypeDAO.Update(entity);
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
                result = result && SarPrintTypeCheckVerifyValidData.Verify(param, entity);
                result = result && SarPrintTypeCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && SarPrintTypeCheckVerifyExistsCode.Verify(param, entity.PRINT_TYPE_CODE, entity.ID);
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
