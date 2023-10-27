using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplicationRole.Get.Ev
{
    class AcsApplicationRoleGetEvBehaviorByCode : BeanObjectBase, IAcsApplicationRoleGetEv
    {
        string code;

        internal AcsApplicationRoleGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        ACS_APPLICATION_ROLE IAcsApplicationRoleGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsApplicationRoleDAO.GetByCode(code, new AcsApplicationRoleFilterQuery().Query());
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
