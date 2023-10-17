using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormType.Delete
{
    class SarFormTypeDeleteBehaviorEv : BeanObjectBase, ISarFormTypeDelete
    {
        SAR_FORM_TYPE entity;

        internal SarFormTypeDeleteBehaviorEv(CommonParam param, SAR_FORM_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool ISarFormTypeDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarFormTypeDAO.Truncate(entity);
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
                result = result && SarFormTypeCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
