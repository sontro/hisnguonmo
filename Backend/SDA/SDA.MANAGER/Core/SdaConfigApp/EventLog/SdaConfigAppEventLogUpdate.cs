using Inventec.Core;
using Inventec.Token.ResourceSystem;
using SDA.EFMODEL.DataModels;
using SDA.SDO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaConfigApp.EventLog
{
    class SdaConfigAppEventLogUpdate
    {
        internal static void Log(object beforeData, object afterData)
        {
            try
            {
                //SDA.MANAGER.Base.EventLogUtil.SetEventLog(LibraryEventLog.EventLog.Enum.SdaConfigAppEventLogUpdate, Newtonsoft.Json.JsonConvert.SerializeObject(beforeData), Newtonsoft.Json.JsonConvert.SerializeObject(afterData));

                if (beforeData != null && afterData != null)
                {
                    if (beforeData.GetType() == typeof(SDA_CONFIG_APP))
                    {
                        CreateSdaEventLog((SDA_CONFIG_APP)beforeData, (SDA_CONFIG_APP)afterData);
                    }
                    else if (beforeData.GetType() == typeof(List<SDA_CONFIG_APP>))
                    {
                        var lstBeforeData = (List<SDA_CONFIG_APP>)beforeData;
                        var lstAfterData = (List<SDA_CONFIG_APP>)afterData;
                        foreach (var before in lstBeforeData)
                        {
                            var after = lstAfterData.FirstOrDefault(o => o.ID == before.ID);
                            if (after != null)
                            {
                                CreateSdaEventLog(before, after);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static void CreateSdaEventLog(SDA_CONFIG_APP beforeData, SDA_CONFIG_APP afterData)
        {
            try
            {
                CommonParam param = new CommonParam();
                Manager.SdaEventLogManager evenLog = new Manager.SdaEventLogManager(param);
                SdaEventLogSDO sdo = new SdaEventLogSDO();

                sdo.AppCode = "SDA";
                try
                {
                    sdo.Ip = ResourceTokenManager.GetRequestAddress();
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Error(ex);
                }
                sdo.LogginName = ResourceTokenManager.GetLoginName();
                sdo.EventTime = Inventec.Common.DateTime.Get.Now().Value;

                List<string> description = new List<string>();

                if (beforeData.DEFAULT_VALUE != afterData.DEFAULT_VALUE)
                {
                    description.Add("DEFAULT_VALUE: " + beforeData.DEFAULT_VALUE + "|" + afterData.DEFAULT_VALUE);
                }

                if (beforeData.DESCRIPTION != afterData.DESCRIPTION)
                {
                    description.Add("DESCRIPTION: " + beforeData.DESCRIPTION + "|" + afterData.DESCRIPTION);
                }

                if (beforeData.KEY != afterData.KEY)
                {
                    description.Add("KEY: " + beforeData.KEY + "|" + afterData.KEY);
                }

                if (beforeData.VALUE_ALLOW_IN != afterData.VALUE_ALLOW_IN)
                {
                    description.Add("VALUE_ALLOW_IN: " + beforeData.VALUE_ALLOW_IN + "|" + afterData.VALUE_ALLOW_IN);
                }

                if (beforeData.VALUE_ALLOW_MAX != afterData.VALUE_ALLOW_MAX)
                {
                    description.Add("VALUE_ALLOW_MAX: " + beforeData.VALUE_ALLOW_MAX + "|" + afterData.VALUE_ALLOW_MAX);
                }

                if (beforeData.VALUE_ALLOW_MIN != afterData.VALUE_ALLOW_MIN)
                {
                    description.Add("VALUE_ALLOW_MIN: " + beforeData.VALUE_ALLOW_MIN + "|" + afterData.VALUE_ALLOW_MIN);
                }

                if (beforeData.VALUE_TYPE != afterData.VALUE_TYPE)
                {
                    description.Add("VALUE_TYPE:" + beforeData.VALUE_TYPE + "|" + afterData.VALUE_TYPE);
                }

                sdo.Description = "Sửa cấu hình tài khoản: " + beforeData.KEY + "-" + string.Join(";", description);

                evenLog.Create(sdo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
