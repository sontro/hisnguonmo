using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaNational.Update
{
    class SdaNationalUpdateBehaviorEv : BeanObjectBase, ISdaNationalUpdate
    {
        SDA_NATIONAL entity;

        internal SdaNationalUpdateBehaviorEv(CommonParam param, SDA_NATIONAL data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaNationalUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaNationalDAO.Update(entity);
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
                result = result && SdaNationalCheckVerifyValidData.Verify(param, entity);
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
