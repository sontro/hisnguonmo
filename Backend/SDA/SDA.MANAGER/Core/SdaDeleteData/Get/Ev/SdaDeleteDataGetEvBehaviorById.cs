using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDeleteData.Get.Ev
{
    class SdaDeleteDataGetEvBehaviorById : BeanObjectBase, ISdaDeleteDataGetEv
    {
        long id;

        internal SdaDeleteDataGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SDA_DELETE_DATA ISdaDeleteDataGetEv.Run()
        {
            try
            {
                return DAOWorker.SdaDeleteDataDAO.GetById(id, new SdaDeleteDataFilterQuery().Query());
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
