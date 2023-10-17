using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTrouble.Get.V
{
    class SdaTroubleGetVBehaviorById : BeanObjectBase, ISdaTroubleGetV
    {
        long id;

        internal SdaTroubleGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SDA_TROUBLE ISdaTroubleGetV.Run()
        {
            try
            {
                return DAOWorker.SdaTroubleDAO.GetViewById(id, new SdaTroubleViewFilterQuery().Query());
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
