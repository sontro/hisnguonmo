using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarForm.Get.Ev
{
    class SarFormGetEvBehaviorById : BeanObjectBase, ISarFormGetEv
    {
        long id;

        internal SarFormGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SAR_FORM ISarFormGetEv.Run()
        {
            try
            {
                return DAOWorker.SarFormDAO.GetById(id, new SarFormFilterQuery().Query());
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
