using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarRetyFofi.Create
{
    class SarRetyFofiCreateBehaviorEv : BeanObjectBase, ISarRetyFofiCreate
    {
        SAR_RETY_FOFI entity;

        internal SarRetyFofiCreateBehaviorEv(CommonParam param, SAR_RETY_FOFI data)
            : base(param)
        {
            entity = data;
        }

        bool ISarRetyFofiCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarRetyFofiDAO.Create(entity);
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
                result = result && SarRetyFofiCheckVerifyValidData.Verify(param, entity);
                result = result && SarRetyFofiCheckVerifyExistsCode.Verify(param, entity.RETY_FOFI_CODE, null);
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
