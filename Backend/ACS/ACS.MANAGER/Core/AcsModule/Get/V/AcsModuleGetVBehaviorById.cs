using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModule.Get.V
{
    class AcsModuleGetVBehaviorById : BeanObjectBase, IAcsModuleGetV
    {
        long id;

        internal AcsModuleGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_ACS_MODULE IAcsModuleGetV.Run()
        {
            try
            {
                return DAOWorker.AcsModuleDAO.GetViewById(id, new AcsModuleViewFilterQuery().Query());
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
