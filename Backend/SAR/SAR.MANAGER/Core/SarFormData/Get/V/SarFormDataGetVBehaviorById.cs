using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormData.Get.V
{
    class SarFormDataGetVBehaviorById : BeanObjectBase, ISarFormDataGetV
    {
        long id;

        internal SarFormDataGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SAR_FORM_DATA ISarFormDataGetV.Run()
        {
            try
            {
                return DAOWorker.SarFormDataDAO.GetViewById(id, new SarFormDataViewFilterQuery().Query());
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
