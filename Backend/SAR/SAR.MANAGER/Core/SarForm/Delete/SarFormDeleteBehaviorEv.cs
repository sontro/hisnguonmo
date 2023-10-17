using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;
using SAR.MANAGER.Core.SarFormData.Get;

namespace SAR.MANAGER.Core.SarForm.Delete
{
    class SarFormDeleteBehaviorEv : BeanObjectBase, ISarFormDelete
    {
        SAR_FORM entity;

        internal SarFormDeleteBehaviorEv(CommonParam param, SAR_FORM data)
            : base(param)
        {
            entity = data;
        }

        bool ISarFormDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarFormDAO.Truncate(entity);
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
                result = result && SarFormCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
                result = result && DeleteFormData(entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool DeleteFormData(SAR_FORM data)
        {
            bool result = false;
            try
            {
                result = Base.DAOWorker.SqlDAO.Execute("DELETE SAR_FORM_DATA WHERE FORM_ID = :ID", data.ID);
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
