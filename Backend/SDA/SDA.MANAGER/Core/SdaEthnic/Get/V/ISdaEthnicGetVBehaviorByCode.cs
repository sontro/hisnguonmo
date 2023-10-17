using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEthnic.Get.V
{
    class SdaEthnicGetVBehaviorByCode : BeanObjectBase, ISdaEthnicGetV
    {
        string code;

        internal SdaEthnicGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_ETHNIC ISdaEthnicGetV.Run()
        {
            try
            {
                return DAOWorker.SdaEthnicDAO.GetViewByCode(code, new SdaEthnicViewFilterQuery().Query());
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
