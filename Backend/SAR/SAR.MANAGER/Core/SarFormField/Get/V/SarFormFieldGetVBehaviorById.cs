using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormField.Get.V
{
    class SarFormFieldGetVBehaviorById : BeanObjectBase, ISarFormFieldGetV
    {
        long id;

        internal SarFormFieldGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SAR_FORM_FIELD ISarFormFieldGetV.Run()
        {
            try
            {
                return DAOWorker.SarFormFieldDAO.GetViewById(id, new SarFormFieldViewFilterQuery().Query());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
