using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSqlParam.Get.Ev
{
    class SdaSqlParamGetEvBehaviorById : BeanObjectBase, ISdaSqlParamGetEv
    {
        long id;

        internal SdaSqlParamGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SDA_SQL_PARAM ISdaSqlParamGetEv.Run()
        {
            try
            {
                return DAOWorker.SdaSqlParamDAO.GetById(id, new SdaSqlParamFilterQuery().Query());
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
