using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarRetyFofi.Get.V
{
    class SarRetyFofiGetVBehaviorById : BeanObjectBase, ISarRetyFofiGetV
    {
        long id;

        internal SarRetyFofiGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SAR_RETY_FOFI ISarRetyFofiGetV.Run()
        {
            try
            {
                return DAOWorker.SarRetyFofiDAO.GetViewById(id, new SarRetyFofiViewFilterQuery().Query());
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
