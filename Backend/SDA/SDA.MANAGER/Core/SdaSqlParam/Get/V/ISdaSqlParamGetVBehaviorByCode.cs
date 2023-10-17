using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSqlParam.Get.V
{
    class SdaSqlParamGetVBehaviorByCode : BeanObjectBase, ISdaSqlParamGetV
    {
        string code;

        internal SdaSqlParamGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_SQL_PARAM ISdaSqlParamGetV.Run()
        {
            try
            {
                return DAOWorker.SdaSqlParamDAO.GetViewByCode(code, new SdaSqlParamViewFilterQuery().Query());
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
