using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleBase.Get.Ev
{
    class AcsRoleBaseGetEvBehaviorByCode : BeanObjectBase, IAcsRoleBaseGetEv
    {
        string code;

        internal AcsRoleBaseGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        ACS_ROLE_BASE IAcsRoleBaseGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsRoleBaseDAO.GetByCode(code, new AcsRoleBaseFilterQuery().Query());
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
