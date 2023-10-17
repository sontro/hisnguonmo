using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarRetyFofi.Get.Ev
{
    class SarRetyFofiGetEvBehaviorByCode : BeanObjectBase, ISarRetyFofiGetEv
    {
        string code;

        internal SarRetyFofiGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        SAR_RETY_FOFI ISarRetyFofiGetEv.Run()
        {
            try
            {
                return DAOWorker.SarRetyFofiDAO.GetByCode(code, new SarRetyFofiFilterQuery().Query());
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
