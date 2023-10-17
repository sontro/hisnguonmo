using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormData.Get.Ev
{
    class SarFormDataGetEvBehaviorByCode : BeanObjectBase, ISarFormDataGetEv
    {
        string code;

        internal SarFormDataGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        SAR_FORM_DATA ISarFormDataGetEv.Run()
        {
            try
            {
                return DAOWorker.SarFormDataDAO.GetByCode(code, new SarFormDataFilterQuery().Query());
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
