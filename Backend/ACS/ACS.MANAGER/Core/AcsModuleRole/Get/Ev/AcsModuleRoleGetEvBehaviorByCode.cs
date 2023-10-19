using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleRole.Get.Ev
{
    class AcsModuleRoleGetEvBehaviorByCode : BeanObjectBase, IAcsModuleRoleGetEv
    {
        string code;

        internal AcsModuleRoleGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        ACS_MODULE_ROLE IAcsModuleRoleGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsModuleRoleDAO.GetByCode(code, new AcsModuleRoleFilterQuery().Query());
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
