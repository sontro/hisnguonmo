using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSql.Get.Ev
{
    class SdaSqlGetEvBehaviorById : BeanObjectBase, ISdaSqlGetEv
    {
        long id;

        internal SdaSqlGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SDA_SQL ISdaSqlGetEv.Run()
        {
            try
            {
                return DAOWorker.SdaSqlDAO.GetById(id, new SdaSqlFilterQuery().Query());
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
