using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using SAR.MANAGER.Core.SarFormData.EventLog;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormData.Update
{
    class SarFormDataUpdateBehaviorEv : BeanObjectBase, ISarFormDataUpdate
    {
        SAR_FORM_DATA current;
        SAR_FORM_DATA entity;

        internal SarFormDataUpdateBehaviorEv(CommonParam param, SAR_FORM_DATA data)
            : base(param)
        {
            entity = data;
        }

        bool ISarFormDataUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarFormDataDAO.Update(entity);
                if (result) { SarFormDataEventLogUpdate.Log(current, entity); }
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
                result = result && SarFormDataCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
                result = result && SarFormDataCheckVerifyExistsCode.Verify(param, entity.FORM_DATA_CODE, entity.ID);
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
