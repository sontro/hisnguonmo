using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintLog.Get.V
{
    class SarPrintLogGetVBehaviorById : BeanObjectBase, ISarPrintLogGetV
    {
        long id;

        internal SarPrintLogGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SAR_PRINT_LOG ISarPrintLogGetV.Run()
        {
            try
            {
                return DAOWorker.SarPrintLogDAO.GetViewById(id, new SarPrintLogViewFilterQuery().Query());
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
