using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaNational.Create
{
    class SdaNationalCreateBehaviorEv : BeanObjectBase, ISdaNationalCreate
    {
        SDA_NATIONAL entity;

        internal SdaNationalCreateBehaviorEv(CommonParam param, SDA_NATIONAL data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaNationalCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaNationalDAO.Create(entity);
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
                result = result && SdaNationalCheckVerifyExistsCode.Verify(param, entity.NATIONAL_CODE, null);
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
