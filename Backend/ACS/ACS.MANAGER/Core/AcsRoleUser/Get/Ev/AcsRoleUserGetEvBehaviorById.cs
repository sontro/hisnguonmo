using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleUser.Get.Ev
{
    class AcsRoleUserGetEvBehaviorById : BeanObjectBase, IAcsRoleUserGetEv
    {
        long id;

        internal AcsRoleUserGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        ACS_ROLE_USER IAcsRoleUserGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsRoleUserDAO.GetById(id, new AcsRoleUserFilterQuery().Query());
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
