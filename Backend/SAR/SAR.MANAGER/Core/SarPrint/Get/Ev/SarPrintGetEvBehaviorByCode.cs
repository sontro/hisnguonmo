using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrint.Get.Ev
{
    class SarPrintGetEvBehaviorByCode : BeanObjectBase, ISarPrintGetEv
    {
        string code;

        internal SarPrintGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        SAR_PRINT ISarPrintGetEv.Run()
        {
            try
            {
                return DAOWorker.SarPrintDAO.GetByCode(code, new SarPrintFilterQuery().Query());
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
