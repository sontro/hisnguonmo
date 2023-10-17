using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaReligion.Get.V
{
    class SdaReligionGetVBehaviorById : BeanObjectBase, ISdaReligionGetV
    {
        long id;

        internal SdaReligionGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SDA_RELIGION ISdaReligionGetV.Run()
        {
            try
            {
                return DAOWorker.SdaReligionDAO.GetViewById(id, new SdaReligionViewFilterQuery().Query());
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
