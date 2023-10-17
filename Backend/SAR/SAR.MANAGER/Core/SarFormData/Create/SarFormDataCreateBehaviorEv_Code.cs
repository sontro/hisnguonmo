using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using SAR.MANAGER.Core.SarFormData.EventLog;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormData.Create
{
    class SarFormDataCreateBehaviorEv : BeanObjectBase, ISarFormDataCreate
    {
        SAR_FORM_DATA entity;

        internal SarFormDataCreateBehaviorEv(CommonParam param, SAR_FORM_DATA data)
            : base(param)
        {
            entity = data;
        }

        bool ISarFormDataCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarFormDataDAO.Create(entity);
                if (result) { SarFormDataEventLogCreate.Log(entity); }
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
                result = result && SarFormDataCheckVerifyValidData.Verify(param, entity);
                result = result && SarFormDataCheckVerifyExistsCode.Verify(param, entity.FORM_DATA_CODE, null);
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
