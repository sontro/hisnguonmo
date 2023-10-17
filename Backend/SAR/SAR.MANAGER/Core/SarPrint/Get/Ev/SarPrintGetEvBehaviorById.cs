using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrint.Get.Ev
{
    class SarPrintGetEvBehaviorById : BeanObjectBase, ISarPrintGetEv
    {
        long id;

        internal SarPrintGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SAR_PRINT ISarPrintGetEv.Run()
        {
            try
            {
                return DAOWorker.SarPrintDAO.GetById(id, new SarPrintFilterQuery().Query());
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
