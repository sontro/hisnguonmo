using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaProvince.Create
{
    class SdaProvinceCreateBehaviorListEv : BeanObjectBase, ISdaProvinceCreate
    {
        List<SDA_PROVINCE> entities;

        internal SdaProvinceCreateBehaviorListEv(CommonParam param, List<SDA_PROVINCE> datas)
            : base(param)
        {
            entities = datas;
        }

        bool ISdaProvinceCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaProvinceDAO.CreateList(entities);
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
                result = result && SdaProvinceCheckVerifyValidData.Verify(param, entities);
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
