using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormType.Get.V
{
    class SarFormTypeGetVBehaviorByCode : BeanObjectBase, ISarFormTypeGetV
    {
        string code;

        internal SarFormTypeGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SAR_FORM_TYPE ISarFormTypeGetV.Run()
        {
            try
            {
                return DAOWorker.SarFormTypeDAO.GetViewByCode(code, new SarFormTypeViewFilterQuery().Query());
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
