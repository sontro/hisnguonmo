using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarForm.Get.V
{
    class SarFormGetVBehaviorByCode : BeanObjectBase, ISarFormGetV
    {
        string code;

        internal SarFormGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SAR_FORM ISarFormGetV.Run()
        {
            try
            {
                return DAOWorker.SarFormDAO.GetViewByCode(code, new SarFormViewFilterQuery().Query());
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
