using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplication.Get.V
{
    class AcsApplicationGetVBehaviorByCode : BeanObjectBase, IAcsApplicationGetV
    {
        string code;

        internal AcsApplicationGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_ACS_APPLICATION IAcsApplicationGetV.Run()
        {
            try
            {
                return DAOWorker.AcsApplicationDAO.GetViewByCode(code, new AcsApplicationViewFilterQuery().Query());
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
