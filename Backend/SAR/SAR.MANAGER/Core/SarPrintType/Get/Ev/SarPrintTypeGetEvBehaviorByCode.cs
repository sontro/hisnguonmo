using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintType.Get.Ev
{
    class SarPrintTypeGetEvBehaviorByCode : BeanObjectBase, ISarPrintTypeGetEv
    {
        string code;

        internal SarPrintTypeGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        SAR_PRINT_TYPE ISarPrintTypeGetEv.Run()
        {
            try
            {
                return DAOWorker.SarPrintTypeDAO.GetByCode(code, new SarPrintTypeFilterQuery().Query());
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
