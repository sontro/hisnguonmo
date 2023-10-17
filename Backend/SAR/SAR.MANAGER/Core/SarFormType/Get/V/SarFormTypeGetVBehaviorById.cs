using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormType.Get.V
{
    class SarFormTypeGetVBehaviorById : BeanObjectBase, ISarFormTypeGetV
    {
        long id;

        internal SarFormTypeGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SAR_FORM_TYPE ISarFormTypeGetV.Run()
        {
            try
            {
                return DAOWorker.SarFormTypeDAO.GetViewById(id, new SarFormTypeViewFilterQuery().Query());
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
