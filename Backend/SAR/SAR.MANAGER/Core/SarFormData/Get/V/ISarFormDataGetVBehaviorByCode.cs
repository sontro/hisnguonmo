using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormData.Get.V
{
    class SarFormDataGetVBehaviorByCode : BeanObjectBase, ISarFormDataGetV
    {
        string code;

        internal SarFormDataGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SAR_FORM_DATA ISarFormDataGetV.Run()
        {
            try
            {
                return DAOWorker.SarFormDataDAO.GetViewByCode(code, new SarFormDataViewFilterQuery().Query());
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
