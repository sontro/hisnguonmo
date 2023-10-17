using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormField.Update
{
    class SarFormFieldUpdateBehaviorEv : BeanObjectBase, ISarFormFieldUpdate
    {
        SAR_FORM_FIELD entity;

        internal SarFormFieldUpdateBehaviorEv(CommonParam param, SAR_FORM_FIELD data)
            : base(param)
        {
            entity = data;
        }

        bool ISarFormFieldUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarFormFieldDAO.Update(entity);
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
                result = result && SarFormFieldCheckVerifyValidData.Verify(param, entity);
                result = result && SarFormFieldCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && SarFormFieldCheckVerifyExistsCode.Verify(param, entity.FORM_FIELD_CODE, entity.ID);
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
