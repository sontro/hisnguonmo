using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormType.Update
{
    class SarFormTypeUpdateBehaviorEv : BeanObjectBase, ISarFormTypeUpdate
    {
        SAR_FORM_TYPE current;
        SAR_FORM_TYPE entity;

        internal SarFormTypeUpdateBehaviorEv(CommonParam param, SAR_FORM_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool ISarFormTypeUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarFormTypeDAO.Update(entity);
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
                result = result && SarFormTypeCheckVerifyValidData.Verify(param, entity);
                result = result && SarFormTypeCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
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
