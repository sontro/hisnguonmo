using SAR.MANAGER.Core.SarReportStt.Lock;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportStt
{
    partial class SarReportSttChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportSttChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarReportStt.Contains(entity.GetType()))
                {
                    ISarReportSttChangeLock behavior = SarReportSttChangeLockBehaviorFactory.MakeISarReportSttChangeLock(param, entity);
                    result = behavior != null ? behavior.Run() : false;
                }
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
