using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrint.Get.V
{
    class SarPrintGetVBehaviorByCode : BeanObjectBase, ISarPrintGetV
    {
        string code;

        internal SarPrintGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SAR_PRINT ISarPrintGetV.Run()
        {
            try
            {
                return DAOWorker.SarPrintDAO.GetViewByCode(code, new SarPrintViewFilterQuery().Query());
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
