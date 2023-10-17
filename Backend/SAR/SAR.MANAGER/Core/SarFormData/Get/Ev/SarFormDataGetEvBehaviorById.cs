using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormData.Get.Ev
{
    class SarFormDataGetEvBehaviorById : BeanObjectBase, ISarFormDataGetEv
    {
        long id;

        internal SarFormDataGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SAR_FORM_DATA ISarFormDataGetEv.Run()
        {
            try
            {
                return DAOWorker.SarFormDataDAO.GetById(id, new SarFormDataFilterQuery().Query());
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
