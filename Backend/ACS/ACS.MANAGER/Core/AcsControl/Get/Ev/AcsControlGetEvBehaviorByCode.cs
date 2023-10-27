using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControl.Get.Ev
{
    class AcsControlGetEvBehaviorByCode : BeanObjectBase, IAcsControlGetEv
    {
        string code;

        internal AcsControlGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        ACS_CONTROL IAcsControlGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsControlDAO.GetByCode(code, new AcsControlFilterQuery().Query());
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
