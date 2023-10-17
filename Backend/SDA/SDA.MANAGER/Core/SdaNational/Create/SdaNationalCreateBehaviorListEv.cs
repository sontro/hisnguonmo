using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaNational.Create
{
    class SdaNationalCreateBehaviorListEv : BeanObjectBase, ISdaNationalCreate
    {
        List<SDA_NATIONAL> entities;

        internal SdaNationalCreateBehaviorListEv(CommonParam param, List<SDA_NATIONAL> datas)
            : base(param)
        {
            entities = datas;
        }

        bool ISdaNationalCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaNationalDAO.CreateList(entities);
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
                result = result && SdaNationalCheckVerifyValidData.Verify(param, entities);
                result = result && SdaNationalCheckVerifyExistsCode.Verify(param, entities);
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
