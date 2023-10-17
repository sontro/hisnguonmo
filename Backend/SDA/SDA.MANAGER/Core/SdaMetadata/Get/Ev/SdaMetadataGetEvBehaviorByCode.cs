using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaMetadata.Get.Ev
{
    class SdaMetadataGetEvBehaviorByCode : BeanObjectBase, ISdaMetadataGetEv
    {
        string code;

        internal SdaMetadataGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        SDA_METADATA ISdaMetadataGetEv.Run()
        {
            try
            {
                return DAOWorker.SdaMetadataDAO.GetByCode(code, new SdaMetadataFilterQuery().Query());
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
