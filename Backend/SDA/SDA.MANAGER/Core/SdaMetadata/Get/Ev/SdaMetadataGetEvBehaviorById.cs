using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaMetadata.Get.Ev
{
    class SdaMetadataGetEvBehaviorById : BeanObjectBase, ISdaMetadataGetEv
    {
        long id;

        internal SdaMetadataGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SDA_METADATA ISdaMetadataGetEv.Run()
        {
            try
            {
                return DAOWorker.SdaMetadataDAO.GetById(id, new SdaMetadataFilterQuery().Query());
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
