using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleRole.Get.Ev
{
    class AcsModuleRoleGetEvBehaviorById : BeanObjectBase, IAcsModuleRoleGetEv
    {
        long id;

        internal AcsModuleRoleGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        ACS_MODULE_ROLE IAcsModuleRoleGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsModuleRoleDAO.GetById(id, new AcsModuleRoleFilterQuery().Query());
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
