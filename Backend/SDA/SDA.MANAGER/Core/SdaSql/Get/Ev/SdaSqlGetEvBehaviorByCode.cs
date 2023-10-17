using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSql.Get.Ev
{
    class SdaSqlGetEvBehaviorByCode : BeanObjectBase, ISdaSqlGetEv
    {
        string code;

        internal SdaSqlGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        SDA_SQL ISdaSqlGetEv.Run()
        {
            try
            {
                return DAOWorker.SdaSqlDAO.GetByCode(code, new SdaSqlFilterQuery().Query());
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
