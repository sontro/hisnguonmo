using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintType.Create
{
    class SarPrintTypeCreateBehaviorEv : BeanObjectBase, ISarPrintTypeCreate
    {
        SAR_PRINT_TYPE entity;

        internal SarPrintTypeCreateBehaviorEv(CommonParam param, SAR_PRINT_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool ISarPrintTypeCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarPrintTypeDAO.Create(entity);
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
