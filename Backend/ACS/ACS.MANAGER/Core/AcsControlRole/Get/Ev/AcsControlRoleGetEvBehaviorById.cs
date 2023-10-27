using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControlRole.Get.Ev
{
    class AcsControlRoleGetEvBehaviorById : BeanObjectBase, IAcsControlRoleGetEv
    {
        long id;

        internal AcsControlRoleGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        ACS_CONTROL_ROLE IAcsControlRoleGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsControlRoleDAO.GetById(id, new AcsControlRoleFilterQuery().Query());
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
