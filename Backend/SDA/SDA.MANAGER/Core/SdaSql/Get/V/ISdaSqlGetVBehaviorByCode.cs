using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSql.Get.V
{
    class SdaSqlGetVBehaviorByCode : BeanObjectBase, ISdaSqlGetV
    {
        string code;

        internal SdaSqlGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_SQL ISdaSqlGetV.Run()
        {
            try
            {
                return DAOWorker.SdaSqlDAO.GetViewByCode(code, new SdaSqlViewFilterQuery().Query());
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
