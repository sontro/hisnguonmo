using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormType.Get.Ev
{
    class SarFormTypeGetEvBehaviorById : BeanObjectBase, ISarFormTypeGetEv
    {
        long id;

        internal SarFormTypeGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SAR_FORM_TYPE ISarFormTypeGetEv.Run()
        {
            try
            {
                return DAOWorker.SarFormTypeDAO.GetById(id, new SarFormTypeFilterQuery().Query());
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
