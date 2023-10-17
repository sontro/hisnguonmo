using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrint.Get.V
{
    class SarPrintGetVBehaviorById : BeanObjectBase, ISarPrintGetV
    {
        long id;

        internal SarPrintGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SAR_PRINT ISarPrintGetV.Run()
        {
            try
            {
                return DAOWorker.SarPrintDAO.GetViewById(id, new SarPrintViewFilterQuery().Query());
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
