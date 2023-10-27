using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleUser.Get.V
{
    class AcsRoleUserGetVBehaviorById : BeanObjectBase, IAcsRoleUserGetV
    {
        long id;

        internal AcsRoleUserGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_ACS_ROLE_USER IAcsRoleUserGetV.Run()
        {
            try
            {
                return DAOWorker.AcsRoleUserDAO.GetViewById(id, new AcsRoleUserViewFilterQuery().Query());
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
