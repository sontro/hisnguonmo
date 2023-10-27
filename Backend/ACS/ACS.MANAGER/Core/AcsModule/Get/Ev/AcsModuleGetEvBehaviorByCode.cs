using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModule.Get.Ev
{
    class AcsModuleGetEvBehaviorByCode : BeanObjectBase, IAcsModuleGetEv
    {
        string code;

        internal AcsModuleGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        ACS_MODULE IAcsModuleGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsModuleDAO.GetByCode(code, new AcsModuleFilterQuery().Query());
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
