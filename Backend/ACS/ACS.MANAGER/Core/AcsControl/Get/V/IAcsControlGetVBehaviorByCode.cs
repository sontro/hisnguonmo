using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControl.Get.V
{
    class AcsControlGetVBehaviorByCode : BeanObjectBase, IAcsControlGetV
    {
        string code;

        internal AcsControlGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_ACS_CONTROL IAcsControlGetV.Run()
        {
            try
            {
                return DAOWorker.AcsControlDAO.GetViewByCode(code, new AcsControlViewFilterQuery().Query());
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
