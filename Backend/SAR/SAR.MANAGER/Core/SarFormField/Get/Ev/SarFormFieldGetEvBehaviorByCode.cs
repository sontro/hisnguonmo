using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormField.Get.Ev
{
    class SarFormFieldGetEvBehaviorByCode : BeanObjectBase, ISarFormFieldGetEv
    {
        string code;

        internal SarFormFieldGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        SAR_FORM_FIELD ISarFormFieldGetEv.Run()
        {
            try
            {
                return DAOWorker.SarFormFieldDAO.GetByCode(code, new SarFormFieldFilterQuery().Query());
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
