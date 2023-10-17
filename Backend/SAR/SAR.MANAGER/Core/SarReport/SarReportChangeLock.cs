using SAR.MANAGER.Core.SarReport.Lock;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReport
{
    partial class SarReportChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarReport.Contains(entity.GetType()))
                {
                    ISarReportChangeLock behavior = SarReportChangeLockBehaviorFactory.MakeISarReportChangeLock(param, entity);
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
