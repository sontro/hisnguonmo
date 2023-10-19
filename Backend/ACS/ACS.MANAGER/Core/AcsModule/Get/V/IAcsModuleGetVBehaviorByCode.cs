using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModule.Get.V
{
    class AcsModuleGetVBehaviorByCode : BeanObjectBase, IAcsModuleGetV
    {
        string code;

        internal AcsModuleGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_ACS_MODULE IAcsModuleGetV.Run()
        {
            try
            {
                return DAOWorker.AcsModuleDAO.GetViewByCode(code, new AcsModuleViewFilterQuery().Query());
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
