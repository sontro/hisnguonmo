using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaDistrict.Create
{
    class SdaDistrictCreateBehaviorListEv : BeanObjectBase, ISdaDistrictCreate
    {
        List<SDA_DISTRICT> entities;

        internal SdaDistrictCreateBehaviorListEv(CommonParam param, List<SDA_DISTRICT> datas)
            : base(param)
        {
            entities = datas;
        }

        bool ISdaDistrictCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaDistrictDAO.CreateList(entities);
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
                result = result && SdaDistrictCheckVerifyValidData.Verify(param, entities);
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
