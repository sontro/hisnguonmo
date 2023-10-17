using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintLog.Get.V
{
    class SarPrintLogGetVBehaviorByCode : BeanObjectBase, ISarPrintLogGetV
    {
        string code;

        internal SarPrintLogGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SAR_PRINT_LOG ISarPrintLogGetV.Run()
        {
            try
            {
                return DAOWorker.SarPrintLogDAO.GetViewByCode(code, new SarPrintLogViewFilterQuery().Query());
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
