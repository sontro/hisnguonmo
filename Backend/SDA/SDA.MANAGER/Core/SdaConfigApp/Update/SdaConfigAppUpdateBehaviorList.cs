using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaConfigApp.EventLog;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaConfigApp.Update
{
    class SdaConfigAppUpdateBehaviorList : BeanObjectBase, ISdaConfigAppUpdate
    {
        List<SDA_CONFIG_APP> current;
        List<SDA_CONFIG_APP> entity;

        internal SdaConfigAppUpdateBehaviorList(CommonParam param, List<SDA_CONFIG_APP> data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaConfigAppUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaConfigAppDAO.UpdateList(entity);
                if (result) { SdaConfigAppEventLogUpdate.Log(current, entity); }
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
                foreach (var item in entity)
                {
                    SDA_CONFIG_APP raw = new SDA_CONFIG_APP();
                    result = result && SdaConfigAppCheckVerifyIsUnlock.Verify(param, item.ID, ref raw);
                    result = result && SdaConfigAppCheckVerifyExistsCode.Verify(param, item.KEY, item.ID);
                    if (result)
                    {
                        if (current == null) current = new List<SDA_CONFIG_APP>();
                        current.Add(raw);
                    }
                }
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
