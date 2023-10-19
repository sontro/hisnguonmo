using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControl.Get.Ev
{
    class AcsControlGetEvBehaviorById : BeanObjectBase, IAcsControlGetEv
    {
        long id;

        internal AcsControlGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        ACS_CONTROL IAcsControlGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsControlDAO.GetById(id, new AcsControlFilterQuery().Query());
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
