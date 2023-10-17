using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDistrictMap.Get.Ev
{
    class SdaDistrictMapGetEvBehaviorById : BeanObjectBase, ISdaDistrictMapGetEv
    {
        long id;

        internal SdaDistrictMapGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SDA_DISTRICT_MAP ISdaDistrictMapGetEv.Run()
        {
            try
            {
                return DAOWorker.SdaDistrictMapDAO.GetById(id, new SdaDistrictMapFilterQuery().Query());
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
