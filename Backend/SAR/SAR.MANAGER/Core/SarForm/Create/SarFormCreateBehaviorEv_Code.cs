using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using SAR.MANAGER.Core.SarForm.EventLog;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarForm.Create
{
    class SarFormCreateBehaviorEv : BeanObjectBase, ISarFormCreate
    {
        SAR_FORM entity;

        internal SarFormCreateBehaviorEv(CommonParam param, SAR_FORM data)
            : base(param)
        {
            entity = data;
        }

        bool ISarFormCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarFormDAO.Create(entity);
                if (result) { SarFormEventLogCreate.Log(entity); }
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
                result = result && SarFormCheckVerifyValidData.Verify(param, entity);
                result = result && SarFormCheckVerifyExistsCode.Verify(param, entity.FORM_CODE, null);
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
