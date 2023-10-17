using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaConfigApp.EventLog;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaConfigApp.Delete
{
    class SdaConfigAppDeleteBehaviorList : BeanObjectBase, ISdaConfigAppDelete
    {
        List<long> entity;
        List<SDA_CONFIG_APP> processDatas;

        internal SdaConfigAppDeleteBehaviorList(CommonParam param, List<long> data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaConfigAppDelete.Run()
        {
            bool result = false;
            try
            {
                if (Check())
                {
                    result = DAOWorker.SdaConfigAppDAO.TruncateList(processDatas);
                }
                if (result) { SdaConfigAppEventLogDelete.Log(entity); }
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
                processDatas = new List<SDA_CONFIG_APP>();
                foreach (var item in entity)
                {
                    bool valid = true;
                    SDA_CONFIG_APP raw = new SDA_CONFIG_APP();
                    valid = valid && SdaConfigAppCheckVerifyIsUnlock.Verify(param, item, ref raw);
                    if (valid)
                        processDatas.Add(raw);
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
