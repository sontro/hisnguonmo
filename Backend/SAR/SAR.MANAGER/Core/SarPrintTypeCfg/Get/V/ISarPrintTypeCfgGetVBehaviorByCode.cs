using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintTypeCfg.Get.V
{
    class SarPrintTypeCfgGetVBehaviorByCode : BeanObjectBase, ISarPrintTypeCfgGetV
    {
        string code;

        internal SarPrintTypeCfgGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SAR_PRINT_TYPE_CFG ISarPrintTypeCfgGetV.Run()
        {
            try
            {
                return DAOWorker.SarPrintTypeCfgDAO.GetViewByCode(code, new SarPrintTypeCfgViewFilterQuery().Query());
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
