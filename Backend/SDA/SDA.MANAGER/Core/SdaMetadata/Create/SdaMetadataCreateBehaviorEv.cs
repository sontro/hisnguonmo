using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaMetadata.Create
{
    class SdaMetadataCreateBehaviorEv : BeanObjectBase, ISdaMetadataCreate
    {
        SDA_METADATA entity;

        internal SdaMetadataCreateBehaviorEv(CommonParam param, SDA_METADATA data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaMetadataCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaMetadataDAO.Create(entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && SdaMetadataCheckVerifyValidData.Verify(param, entity);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
