using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCommuneMap.Get.Ev
{
    class SdaCommuneMapGetEvBehaviorById : BeanObjectBase, ISdaCommuneMapGetEv
    {
        long id;

        internal SdaCommuneMapGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SDA_COMMUNE_MAP ISdaCommuneMapGetEv.Run()
        {
            try
            {
                return DAOWorker.SdaCommuneMapDAO.GetById(id, new SdaCommuneMapFilterQuery().Query());
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
