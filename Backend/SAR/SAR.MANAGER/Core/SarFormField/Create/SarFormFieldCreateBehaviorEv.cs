using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormField.Create
{
    class SarFormFieldCreateBehaviorEv : BeanObjectBase, ISarFormFieldCreate
    {
        SAR_FORM_FIELD entity;

        internal SarFormFieldCreateBehaviorEv(CommonParam param, SAR_FORM_FIELD data)
            : base(param)
        {
            entity = data;
        }

        bool ISarFormFieldCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarFormFieldDAO.Create(entity);
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
