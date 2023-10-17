using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintLog.Get.Ev
{
    class SarPrintLogGetEvBehaviorByCode : BeanObjectBase, ISarPrintLogGetEv
    {
        string code;

        internal SarPrintLogGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        SAR_PRINT_LOG ISarPrintLogGetEv.Run()
        {
            try
            {
                return DAOWorker.SarPrintLogDAO.GetByCode(code, new SarPrintLogFilterQuery().Query());
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
