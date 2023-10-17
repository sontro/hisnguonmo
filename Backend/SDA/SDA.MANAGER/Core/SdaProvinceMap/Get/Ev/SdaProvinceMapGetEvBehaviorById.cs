using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaProvinceMap.Get.Ev
{
    class SdaProvinceMapGetEvBehaviorById : BeanObjectBase, ISdaProvinceMapGetEv
    {
        long id;

        internal SdaProvinceMapGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SDA_PROVINCE_MAP ISdaProvinceMapGetEv.Run()
        {
            try
            {
                return DAOWorker.SdaProvinceMapDAO.GetById(id, new SdaProvinceMapFilterQuery().Query());
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
