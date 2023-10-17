using SDA.MANAGER.Core.SdaEventLog.CreateForLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEventLog
{
    partial class SdaEventLogCreateForLog : BeanObjectBase, IDelegacy
    {
        string description;

        internal SdaEventLogCreateForLog(CommonParam param, string _description)
            : base(param)
        {
            description = _description;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                ISdaEventLogCreateForLog behavior = SdaEventLogCreateForLogBehaviorFactory.MakeISdaEventLogCreateForLog(param, description);
                result = behavior != null ? behavior.Run() : false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
