using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSql.Get.V
{
    class SdaSqlGetVBehaviorById : BeanObjectBase, ISdaSqlGetV
    {
        long id;

        internal SdaSqlGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SDA_SQL ISdaSqlGetV.Run()
        {
            try
            {
                return DAOWorker.SdaSqlDAO.GetViewById(id, new SdaSqlViewFilterQuery().Query());
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
