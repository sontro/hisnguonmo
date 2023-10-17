using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTrouble.Get.V
{
    class SdaTroubleGetVBehaviorByCode : BeanObjectBase, ISdaTroubleGetV
    {
        string code;

        internal SdaTroubleGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_TROUBLE ISdaTroubleGetV.Run()
        {
            try
            {
                return DAOWorker.SdaTroubleDAO.GetViewByCode(code, new SdaTroubleViewFilterQuery().Query());
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
