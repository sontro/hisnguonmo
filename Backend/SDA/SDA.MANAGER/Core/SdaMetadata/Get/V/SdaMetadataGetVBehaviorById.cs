using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaMetadata.Get.V
{
    class SdaMetadataGetVBehaviorById : BeanObjectBase, ISdaMetadataGetV
    {
        long id;

        internal SdaMetadataGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SDA_METADATA ISdaMetadataGetV.Run()
        {
            try
            {
                return DAOWorker.SdaMetadataDAO.GetViewById(id, new SdaMetadataViewFilterQuery().Query());
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
