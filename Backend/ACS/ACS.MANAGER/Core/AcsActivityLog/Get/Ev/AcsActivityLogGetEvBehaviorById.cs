using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityLog.Get.Ev
{
    class AcsActivityLogGetEvBehaviorById : BeanObjectBase, IAcsActivityLogGetEv
    {
        long id;

        internal AcsActivityLogGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        ACS_ACTIVITY_LOG IAcsActivityLogGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsActivityLogDAO.GetById(id, new AcsActivityLogFilterQuery().Query());
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
