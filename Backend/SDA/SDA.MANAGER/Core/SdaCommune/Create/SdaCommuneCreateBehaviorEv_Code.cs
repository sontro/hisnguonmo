using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCommune.Create
{
    class SdaCommuneCreateBehaviorEv : BeanObjectBase, ISdaCommuneCreate
    {
        SDA_COMMUNE entity;

        internal SdaCommuneCreateBehaviorEv(CommonParam param, SDA_COMMUNE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaCommuneCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaCommuneDAO.Create(entity);
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
                result = result && SdaCommuneCheckVerifyExistsCode.Verify(param, entity.COMMUNE_CODE, null);
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
