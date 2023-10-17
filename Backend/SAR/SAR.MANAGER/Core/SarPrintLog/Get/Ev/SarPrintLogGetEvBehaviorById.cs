using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintLog.Get.Ev
{
    class SarPrintLogGetEvBehaviorById : BeanObjectBase, ISarPrintLogGetEv
    {
        long id;

        internal SarPrintLogGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SAR_PRINT_LOG ISarPrintLogGetEv.Run()
        {
            try
            {
                return DAOWorker.SarPrintLogDAO.GetById(id, new SarPrintLogFilterQuery().Query());
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
