using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormField.Delete
{
    class SarFormFieldDeleteBehaviorEv : BeanObjectBase, ISarFormFieldDelete
    {
        SAR_FORM_FIELD entity;

        internal SarFormFieldDeleteBehaviorEv(CommonParam param, SAR_FORM_FIELD data)
            : base(param)
        {
            entity = data;
        }

        bool ISarFormFieldDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarFormFieldDAO.Truncate(entity);
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
                result = result && SarFormFieldCheckVerifyIsUnlock.Verify(param, entity.ID);
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
