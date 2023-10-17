using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDeleteData.Get.Ev
{
    class SdaDeleteDataGetEvBehaviorByCode : BeanObjectBase, ISdaDeleteDataGetEv
    {
        string code;

        internal SdaDeleteDataGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        SDA_DELETE_DATA ISdaDeleteDataGetEv.Run()
        {
            try
            {
                return DAOWorker.SdaDeleteDataDAO.GetByCode(code, new SdaDeleteDataFilterQuery().Query());
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
