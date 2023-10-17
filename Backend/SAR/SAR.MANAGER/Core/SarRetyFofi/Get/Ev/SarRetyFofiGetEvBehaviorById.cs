using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarRetyFofi.Get.Ev
{
    class SarRetyFofiGetEvBehaviorById : BeanObjectBase, ISarRetyFofiGetEv
    {
        long id;

        internal SarRetyFofiGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SAR_RETY_FOFI ISarRetyFofiGetEv.Run()
        {
            try
            {
                return DAOWorker.SarRetyFofiDAO.GetById(id, new SarRetyFofiFilterQuery().Query());
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
