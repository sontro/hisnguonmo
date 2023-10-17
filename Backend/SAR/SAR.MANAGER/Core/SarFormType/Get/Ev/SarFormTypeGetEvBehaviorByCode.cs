using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormType.Get.Ev
{
    class SarFormTypeGetEvBehaviorByCode : BeanObjectBase, ISarFormTypeGetEv
    {
        string code;

        internal SarFormTypeGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        SAR_FORM_TYPE ISarFormTypeGetEv.Run()
        {
            try
            {
                return DAOWorker.SarFormTypeDAO.GetByCode(code, new SarFormTypeFilterQuery().Query());
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
