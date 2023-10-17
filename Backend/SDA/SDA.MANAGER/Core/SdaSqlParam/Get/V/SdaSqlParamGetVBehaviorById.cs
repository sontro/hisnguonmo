using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSqlParam.Get.V
{
    class SdaSqlParamGetVBehaviorById : BeanObjectBase, ISdaSqlParamGetV
    {
        long id;

        internal SdaSqlParamGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SDA_SQL_PARAM ISdaSqlParamGetV.Run()
        {
            try
            {
                return DAOWorker.SdaSqlParamDAO.GetViewById(id, new SdaSqlParamViewFilterQuery().Query());
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
