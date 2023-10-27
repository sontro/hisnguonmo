using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityType.Get.Ev
{
    class AcsActivityTypeGetEvBehaviorById : BeanObjectBase, IAcsActivityTypeGetEv
    {
        long id;

        internal AcsActivityTypeGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        ACS_ACTIVITY_TYPE IAcsActivityTypeGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsActivityTypeDAO.GetById(id, new AcsActivityTypeFilterQuery().Query());
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
