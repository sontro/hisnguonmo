using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSqlParam.Get.Ev
{
    class SdaSqlParamGetEvBehaviorByCode : BeanObjectBase, ISdaSqlParamGetEv
    {
        string code;

        internal SdaSqlParamGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        SDA_SQL_PARAM ISdaSqlParamGetEv.Run()
        {
            try
            {
                return DAOWorker.SdaSqlParamDAO.GetByCode(code, new SdaSqlParamFilterQuery().Query());
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
