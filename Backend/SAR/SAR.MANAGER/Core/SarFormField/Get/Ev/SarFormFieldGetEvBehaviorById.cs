using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormField.Get.Ev
{
    class SarFormFieldGetEvBehaviorById : BeanObjectBase, ISarFormFieldGetEv
    {
        long id;

        internal SarFormFieldGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SAR_FORM_FIELD ISarFormFieldGetEv.Run()
        {
            try
            {
                return DAOWorker.SarFormFieldDAO.GetById(id, new SarFormFieldFilterQuery().Query());
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
