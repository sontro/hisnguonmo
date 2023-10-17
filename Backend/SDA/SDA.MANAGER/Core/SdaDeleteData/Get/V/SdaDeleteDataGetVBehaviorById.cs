using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDeleteData.Get.V
{
    class SdaDeleteDataGetVBehaviorById : BeanObjectBase, ISdaDeleteDataGetV
    {
        long id;

        internal SdaDeleteDataGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SDA_DELETE_DATA ISdaDeleteDataGetV.Run()
        {
            try
            {
                return DAOWorker.SdaDeleteDataDAO.GetViewById(id, new SdaDeleteDataViewFilterQuery().Query());
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
