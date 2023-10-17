using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormType.Create
{
    class SarFormTypeCreateBehaviorEv : BeanObjectBase, ISarFormTypeCreate
    {
        SAR_FORM_TYPE entity;

        internal SarFormTypeCreateBehaviorEv(CommonParam param, SAR_FORM_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool ISarFormTypeCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarFormTypeDAO.Create(entity);
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
