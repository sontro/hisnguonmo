using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaMetadata.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaMetadata.Update
{
    class SdaMetadataUpdateBehaviorEv : BeanObjectBase, ISdaMetadataUpdate
    {
        SDA_METADATA current;
        SDA_METADATA entity;

        internal SdaMetadataUpdateBehaviorEv(CommonParam param, SDA_METADATA data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaMetadataUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaMetadataDAO.Update(entity);
                if (result) { SdaMetadataEventLogUpdate.Log(current, entity); }
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
                result = result && SdaMetadataCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
                result = result && SdaMetadataCheckVerifyExistsCode.Verify(param, entity.METADATA_CODE, entity.ID);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
