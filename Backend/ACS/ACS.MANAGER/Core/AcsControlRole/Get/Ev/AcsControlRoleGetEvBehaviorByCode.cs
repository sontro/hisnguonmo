using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControlRole.Get.Ev
{
    class AcsControlRoleGetEvBehaviorByCode : BeanObjectBase, IAcsControlRoleGetEv
    {
        string code;

        internal AcsControlRoleGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        ACS_CONTROL_ROLE IAcsControlRoleGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsControlRoleDAO.GetByCode(code, new AcsControlRoleFilterQuery().Query());
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
