using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormData.Delete
{
    class SarFormDataDeleteBehaviorEv : BeanObjectBase, ISarFormDataDelete
    {
        SAR_FORM_DATA entity;

        internal SarFormDataDeleteBehaviorEv(CommonParam param, SAR_FORM_DATA data)
            : base(param)
        {
            entity = data;
        }

        bool ISarFormDataDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarFormDataDAO.Truncate(entity);
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
                result = result && SarFormDataCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
