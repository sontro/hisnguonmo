using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaConfigApp.EventLog;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaConfigApp.Create
{
    class SdaConfigAppCreateBehaviorList : BeanObjectBase, ISdaConfigAppCreate
    {
        List<SDA_CONFIG_APP> entity;

        internal SdaConfigAppCreateBehaviorList(CommonParam param, List<SDA_CONFIG_APP> data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaConfigAppCreate.Run()
        {
            bool result = false;
            try
            {
                if (Check())
                {
                    result = DAOWorker.SdaConfigAppDAO.CreateList(entity);
                }

                if (result) { SdaConfigAppEventLogCreate.Log(entity); }
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
                result = result && SdaConfigAppCheckVerifyValidData.Verify(param, entity);
                foreach (var data in entity)
                {
                    result = result && SdaConfigAppCheckVerifyExistsCode.Verify(param, data.KEY, null);
                }
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
