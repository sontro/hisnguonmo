using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintTypeCfg.Get.Ev
{
    class SarPrintTypeCfgGetEvBehaviorById : BeanObjectBase, ISarPrintTypeCfgGetEv
    {
        long id;

        internal SarPrintTypeCfgGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SAR_PRINT_TYPE_CFG ISarPrintTypeCfgGetEv.Run()
        {
            try
            {
                return DAOWorker.SarPrintTypeCfgDAO.GetById(id, new SarPrintTypeCfgFilterQuery().Query());
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
