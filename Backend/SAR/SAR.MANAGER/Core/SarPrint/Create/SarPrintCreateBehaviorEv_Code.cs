using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrint.Create
{
    class SarPrintCreateBehaviorEv : BeanObjectBase, ISarPrintCreate
    {
        SAR_PRINT entity;

        internal SarPrintCreateBehaviorEv(CommonParam param, SAR_PRINT data)
            : base(param)
        {
            entity = data;
        }

        bool ISarPrintCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarPrintDAO.Create(entity);
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
                result = result && SarPrintCheckVerifyValidData.Verify(param, entity);
                result = result && SarPrintCheckVerifyExistsCode.Verify(param, entity.PRINT_CODE, null);
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
