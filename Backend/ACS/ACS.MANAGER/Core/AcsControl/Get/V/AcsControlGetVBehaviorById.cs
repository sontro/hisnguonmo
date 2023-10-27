using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControl.Get.V
{
    class AcsControlGetVBehaviorById : BeanObjectBase, IAcsControlGetV
    {
        long id;

        internal AcsControlGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_ACS_CONTROL IAcsControlGetV.Run()
        {
            try
            {
                return DAOWorker.AcsControlDAO.GetViewById(id, new AcsControlViewFilterQuery().Query());
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
