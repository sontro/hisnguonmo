using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityLog.Get.V
{
    class AcsActivityLogGetVBehaviorById : BeanObjectBase, IAcsActivityLogGetV
    {
        long id;

        internal AcsActivityLogGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_ACS_ACTIVITY_LOG IAcsActivityLogGetV.Run()
        {
            try
            {
                return DAOWorker.AcsActivityLogDAO.GetViewById(id, new AcsActivityLogViewFilterQuery().Query());
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
