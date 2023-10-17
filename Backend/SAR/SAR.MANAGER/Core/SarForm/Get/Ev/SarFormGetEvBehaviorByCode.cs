using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarForm.Get.Ev
{
    class SarFormGetEvBehaviorByCode : BeanObjectBase, ISarFormGetEv
    {
        string code;

        internal SarFormGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        SAR_FORM ISarFormGetEv.Run()
        {
            try
            {
                return DAOWorker.SarFormDAO.GetByCode(code, new SarFormFilterQuery().Query());
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
