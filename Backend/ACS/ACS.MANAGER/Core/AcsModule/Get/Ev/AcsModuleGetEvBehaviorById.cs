using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModule.Get.Ev
{
    class AcsModuleGetEvBehaviorById : BeanObjectBase, IAcsModuleGetEv
    {
        long id;

        internal AcsModuleGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        ACS_MODULE IAcsModuleGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsModuleDAO.GetById(id, new AcsModuleFilterQuery().Query());
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
