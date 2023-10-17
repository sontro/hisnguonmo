using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using SAR.MANAGER.Core.SarForm.EventLog;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarForm.Update
{
    class SarFormUpdateBehaviorEv : BeanObjectBase, ISarFormUpdate
    {
        SAR_FORM current;
        SAR_FORM entity;

        internal SarFormUpdateBehaviorEv(CommonParam param, SAR_FORM data)
            : base(param)
        {
            entity = data;
        }

        bool ISarFormUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarFormDAO.Update(entity);
                if (result) { SarFormEventLogUpdate.Log(current, entity); }
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
                result = result && SarFormCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
                result = result && SarFormCheckVerifyExistsCode.Verify(param, entity.FORM_CODE, entity.ID);
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
