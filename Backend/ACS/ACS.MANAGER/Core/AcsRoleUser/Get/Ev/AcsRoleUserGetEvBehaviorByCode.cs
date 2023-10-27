using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleUser.Get.Ev
{
    class AcsRoleUserGetEvBehaviorByCode : BeanObjectBase, IAcsRoleUserGetEv
    {
        string code;

        internal AcsRoleUserGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        ACS_ROLE_USER IAcsRoleUserGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsRoleUserDAO.GetByCode(code, new AcsRoleUserFilterQuery().Query());
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
