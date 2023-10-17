using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintType.Get.V
{
    class SarPrintTypeGetVBehaviorByCode : BeanObjectBase, ISarPrintTypeGetV
    {
        string code;

        internal SarPrintTypeGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SAR_PRINT_TYPE ISarPrintTypeGetV.Run()
        {
            try
            {
                return DAOWorker.SarPrintTypeDAO.GetViewByCode(code, new SarPrintTypeViewFilterQuery().Query());
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
