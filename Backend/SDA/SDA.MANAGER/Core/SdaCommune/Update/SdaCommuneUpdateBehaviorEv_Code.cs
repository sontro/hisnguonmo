using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCommune.Update
{
    class SdaCommuneUpdateBehaviorEv : BeanObjectBase, ISdaCommuneUpdate
    {
        SDA_COMMUNE entity;

        internal SdaCommuneUpdateBehaviorEv(CommonParam param, SDA_COMMUNE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaCommuneUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaCommuneDAO.Update(entity);
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
                result = result && SdaCommuneCheckVerifyValidData.Verify(param, entity);
                result = result && SdaCommuneCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && SdaCommuneCheckVerifyExistsCode.Verify(param, entity.COMMUNE_CODE, entity.ID);
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
