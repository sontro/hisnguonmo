using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleUser.Get.V
{
    class AcsRoleUserGetVBehaviorByCode : BeanObjectBase, IAcsRoleUserGetV
    {
        string code;

        internal AcsRoleUserGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_ACS_ROLE_USER IAcsRoleUserGetV.Run()
        {
            try
            {
                return DAOWorker.AcsRoleUserDAO.GetViewByCode(code, new AcsRoleUserViewFilterQuery().Query());
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
