using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormField.Get.V
{
    class SarFormFieldGetVBehaviorByCode : BeanObjectBase, ISarFormFieldGetV
    {
        string code;

        internal SarFormFieldGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SAR_FORM_FIELD ISarFormFieldGetV.Run()
        {
            try
            {
                return DAOWorker.SarFormFieldDAO.GetViewByCode(code, new SarFormFieldViewFilterQuery().Query());
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
