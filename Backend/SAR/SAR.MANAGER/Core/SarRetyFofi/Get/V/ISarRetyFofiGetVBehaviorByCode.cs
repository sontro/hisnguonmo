using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarRetyFofi.Get.V
{
    class SarRetyFofiGetVBehaviorByCode : BeanObjectBase, ISarRetyFofiGetV
    {
        string code;

        internal SarRetyFofiGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SAR_RETY_FOFI ISarRetyFofiGetV.Run()
        {
            try
            {
                return DAOWorker.SarRetyFofiDAO.GetViewByCode(code, new SarRetyFofiViewFilterQuery().Query());
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
