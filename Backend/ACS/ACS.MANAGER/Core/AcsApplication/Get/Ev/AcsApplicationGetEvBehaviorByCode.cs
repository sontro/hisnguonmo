using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplication.Get.Ev
{
    class AcsApplicationGetEvBehaviorByCode : BeanObjectBase, IAcsApplicationGetEv
    {
        string code;

        internal AcsApplicationGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        ACS_APPLICATION IAcsApplicationGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsApplicationDAO.GetByCode(code, new AcsApplicationFilterQuery().Query());
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
