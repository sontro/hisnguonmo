using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCommune.Delete
{
    class SdaCommuneDeleteBehaviorEv : BeanObjectBase, ISdaCommuneDelete
    {
        SDA_COMMUNE entity;

        internal SdaCommuneDeleteBehaviorEv(CommonParam param, SDA_COMMUNE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaCommuneDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaCommuneDAO.Truncate(entity);
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
                result = result && SdaCommuneCheckVerifyIsUnlock.Verify(param, entity.ID);
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
