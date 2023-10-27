using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser.Get.V
{
    class AcsUserGetVBehaviorById : BeanObjectBase, IAcsUserGetV
    {
        long id;

        internal AcsUserGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_ACS_USER IAcsUserGetV.Run()
        {
            try
            {
                return DAOWorker.AcsUserDAO.GetViewById(id, new AcsUserViewFilterQuery().Query());
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
