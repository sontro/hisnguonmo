using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEthnic.Get.V
{
    class SdaEthnicGetVBehaviorById : BeanObjectBase, ISdaEthnicGetV
    {
        long id;

        internal SdaEthnicGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SDA_ETHNIC ISdaEthnicGetV.Run()
        {
            try
            {
                return DAOWorker.SdaEthnicDAO.GetViewById(id, new SdaEthnicViewFilterQuery().Query());
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
