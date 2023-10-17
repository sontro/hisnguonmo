using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrint.Update
{
    class SarPrintUpdateBehaviorEv : BeanObjectBase, ISarPrintUpdate
    {
        SAR_PRINT entity;

        internal SarPrintUpdateBehaviorEv(CommonParam param, SAR_PRINT data)
            : base(param)
        {
            entity = data;
        }

        bool ISarPrintUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarPrintDAO.Update(entity);
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
                result = result && SarPrintCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && SarPrintCheckVerifyExistsCode.Verify(param, entity.PRINT_CODE, entity.ID);
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
