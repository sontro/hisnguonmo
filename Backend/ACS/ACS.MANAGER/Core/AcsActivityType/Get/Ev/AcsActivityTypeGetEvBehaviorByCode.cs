using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityType.Get.Ev
{
    class AcsActivityTypeGetEvBehaviorByCode : BeanObjectBase, IAcsActivityTypeGetEv
    {
        string code;

        internal AcsActivityTypeGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        ACS_ACTIVITY_TYPE IAcsActivityTypeGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsActivityTypeDAO.GetByCode(code, new AcsActivityTypeFilterQuery().Query());
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
