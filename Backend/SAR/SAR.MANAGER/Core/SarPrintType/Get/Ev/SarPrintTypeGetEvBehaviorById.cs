using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintType.Get.Ev
{
    class SarPrintTypeGetEvBehaviorById : BeanObjectBase, ISarPrintTypeGetEv
    {
        long id;

        internal SarPrintTypeGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SAR_PRINT_TYPE ISarPrintTypeGetEv.Run()
        {
            try
            {
                return DAOWorker.SarPrintTypeDAO.GetById(id, new SarPrintTypeFilterQuery().Query());
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
