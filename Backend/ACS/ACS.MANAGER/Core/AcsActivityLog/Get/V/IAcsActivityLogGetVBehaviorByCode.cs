using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityLog.Get.V
{
    class AcsActivityLogGetVBehaviorByCode : BeanObjectBase, IAcsActivityLogGetV
    {
        string code;

        internal AcsActivityLogGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_ACS_ACTIVITY_LOG IAcsActivityLogGetV.Run()
        {
            try
            {
                return DAOWorker.AcsActivityLogDAO.GetViewByCode(code, new AcsActivityLogViewFilterQuery().Query());
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
