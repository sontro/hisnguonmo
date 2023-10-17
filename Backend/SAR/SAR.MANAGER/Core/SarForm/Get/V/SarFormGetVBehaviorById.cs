using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarForm.Get.V
{
    class SarFormGetVBehaviorById : BeanObjectBase, ISarFormGetV
    {
        long id;

        internal SarFormGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SAR_FORM ISarFormGetV.Run()
        {
            try
            {
                return DAOWorker.SarFormDAO.GetViewById(id, new SarFormViewFilterQuery().Query());
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
