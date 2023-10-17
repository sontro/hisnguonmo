using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDeleteData.Get.V
{
    class SdaDeleteDataGetVBehaviorByCode : BeanObjectBase, ISdaDeleteDataGetV
    {
        string code;

        internal SdaDeleteDataGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_DELETE_DATA ISdaDeleteDataGetV.Run()
        {
            try
            {
                return DAOWorker.SdaDeleteDataDAO.GetViewByCode(code, new SdaDeleteDataViewFilterQuery().Query());
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
