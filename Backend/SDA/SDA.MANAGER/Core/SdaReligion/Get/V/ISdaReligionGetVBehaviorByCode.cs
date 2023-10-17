using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaReligion.Get.V
{
    class SdaReligionGetVBehaviorByCode : BeanObjectBase, ISdaReligionGetV
    {
        string code;

        internal SdaReligionGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_RELIGION ISdaReligionGetV.Run()
        {
            try
            {
                return DAOWorker.SdaReligionDAO.GetViewByCode(code, new SdaReligionViewFilterQuery().Query());
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
