using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaMetadata.Get.V
{
    class SdaMetadataGetVBehaviorByCode : BeanObjectBase, ISdaMetadataGetV
    {
        string code;

        internal SdaMetadataGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_METADATA ISdaMetadataGetV.Run()
        {
            try
            {
                return DAOWorker.SdaMetadataDAO.GetViewByCode(code, new SdaMetadataViewFilterQuery().Query());
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
