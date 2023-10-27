using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser.Get.V
{
    class AcsUserGetVBehaviorByCode : BeanObjectBase, IAcsUserGetV
    {
        string code;

        internal AcsUserGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_ACS_USER IAcsUserGetV.Run()
        {
            try
            {
                return DAOWorker.AcsUserDAO.GetViewByCode(code, new AcsUserViewFilterQuery().Query());
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
