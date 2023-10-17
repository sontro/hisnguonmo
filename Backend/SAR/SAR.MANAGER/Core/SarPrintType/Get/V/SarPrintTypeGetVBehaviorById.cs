using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintType.Get.V
{
    class SarPrintTypeGetVBehaviorById : BeanObjectBase, ISarPrintTypeGetV
    {
        long id;

        internal SarPrintTypeGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SAR_PRINT_TYPE ISarPrintTypeGetV.Run()
        {
            try
            {
                return DAOWorker.SarPrintTypeDAO.GetViewById(id, new SarPrintTypeViewFilterQuery().Query());
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
