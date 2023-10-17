using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaConfigAppUser.EventLog;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaConfigAppUser.Delete
{
    class SdaConfigAppUserDeleteBehaviorList : BeanObjectBase, ISdaConfigAppUserDelete
    {
        List<SDA_CONFIG_APP_USER> processDatas;
        List<long> entity;

        internal SdaConfigAppUserDeleteBehaviorList(CommonParam param, List<long> data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaConfigAppUserDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaConfigAppUserDAO.TruncateList(processDatas);
                if (result) { SdaConfigAppUserEventLogDelete.Log(entity); }
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
                processDatas = new List<SDA_CONFIG_APP_USER>();
                foreach (var item in entity)
                {
                    bool valid = true;
                    SDA_CONFIG_APP_USER raw = new SDA_CONFIG_APP_USER();
                    valid = valid && SdaConfigAppUserCheckVerifyIsUnlock.Verify(param, item, ref raw);
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
