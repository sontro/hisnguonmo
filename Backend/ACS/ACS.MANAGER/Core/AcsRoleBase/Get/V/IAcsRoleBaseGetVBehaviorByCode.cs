using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleBase.Get.V
{
    class AcsRoleBaseGetVBehaviorByCode : BeanObjectBase, IAcsRoleBaseGetV
    {
        string code;

        internal AcsRoleBaseGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_ACS_ROLE_BASE IAcsRoleBaseGetV.Run()
        {
            try
            {
                return DAOWorker.AcsRoleBaseDAO.GetViewByCode(code, new AcsRoleBaseViewFilterQuery().Query());
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
