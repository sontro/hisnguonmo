using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleBase.Get.V
{
    class AcsRoleBaseGetVBehaviorById : BeanObjectBase, IAcsRoleBaseGetV
    {
        long id;

        internal AcsRoleBaseGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_ACS_ROLE_BASE IAcsRoleBaseGetV.Run()
        {
            try
            {
                return DAOWorker.AcsRoleBaseDAO.GetViewById(id, new AcsRoleBaseViewFilterQuery().Query());
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
