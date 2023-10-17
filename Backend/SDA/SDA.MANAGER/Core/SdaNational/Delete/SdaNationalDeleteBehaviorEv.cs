using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaNational.Delete
{
    class SdaNationalDeleteBehaviorEv : BeanObjectBase, ISdaNationalDelete
    {
        SDA_NATIONAL entity;

        internal SdaNationalDeleteBehaviorEv(CommonParam param, SDA_NATIONAL data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaNationalDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaNationalDAO.Truncate(entity);
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
                result = result && SdaNationalCheckVerifyIsUnlock.Verify(param, entity.ID);
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
