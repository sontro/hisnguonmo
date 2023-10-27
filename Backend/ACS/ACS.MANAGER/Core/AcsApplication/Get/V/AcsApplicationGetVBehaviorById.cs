using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplication.Get.V
{
    class AcsApplicationGetVBehaviorById : BeanObjectBase, IAcsApplicationGetV
    {
        long id;

        internal AcsApplicationGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_ACS_APPLICATION IAcsApplicationGetV.Run()
        {
            try
            {
                return DAOWorker.AcsApplicationDAO.GetViewById(id, new AcsApplicationViewFilterQuery().Query());
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
