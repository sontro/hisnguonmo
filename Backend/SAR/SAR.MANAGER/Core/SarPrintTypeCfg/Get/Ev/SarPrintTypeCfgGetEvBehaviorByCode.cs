using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintTypeCfg.Get.Ev
{
    class SarPrintTypeCfgGetEvBehaviorByCode : BeanObjectBase, ISarPrintTypeCfgGetEv
    {
        string code;

        internal SarPrintTypeCfgGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        SAR_PRINT_TYPE_CFG ISarPrintTypeCfgGetEv.Run()
        {
            try
            {
                return DAOWorker.SarPrintTypeCfgDAO.GetByCode(code, new SarPrintTypeCfgFilterQuery().Query());
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
