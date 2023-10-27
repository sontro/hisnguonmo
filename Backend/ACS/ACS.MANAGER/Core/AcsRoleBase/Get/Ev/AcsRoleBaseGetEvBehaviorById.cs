using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleBase.Get.Ev
{
    class AcsRoleBaseGetEvBehaviorById : BeanObjectBase, IAcsRoleBaseGetEv
    {
        long id;

        internal AcsRoleBaseGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        ACS_ROLE_BASE IAcsRoleBaseGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsRoleBaseDAO.GetById(id, new AcsRoleBaseFilterQuery().Query());
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
