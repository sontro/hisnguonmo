using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintTypeCfg.Get.V
{
    class SarPrintTypeCfgGetVBehaviorById : BeanObjectBase, ISarPrintTypeCfgGetV
    {
        long id;

        internal SarPrintTypeCfgGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SAR_PRINT_TYPE_CFG ISarPrintTypeCfgGetV.Run()
        {
            try
            {
                return DAOWorker.SarPrintTypeCfgDAO.GetViewById(id, new SarPrintTypeCfgViewFilterQuery().Query());
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
