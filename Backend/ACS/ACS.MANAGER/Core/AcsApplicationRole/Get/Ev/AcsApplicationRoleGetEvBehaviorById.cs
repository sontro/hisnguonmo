using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplicationRole.Get.Ev
{
    class AcsApplicationRoleGetEvBehaviorById : BeanObjectBase, IAcsApplicationRoleGetEv
    {
        long id;

        internal AcsApplicationRoleGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        ACS_APPLICATION_ROLE IAcsApplicationRoleGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsApplicationRoleDAO.GetById(id, new AcsApplicationRoleFilterQuery().Query());
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
