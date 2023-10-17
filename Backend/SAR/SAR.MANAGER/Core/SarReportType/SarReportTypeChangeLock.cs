using SAR.MANAGER.Core.SarReportType.Lock;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportType
{
    partial class SarReportTypeChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportTypeChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarReportType.Contains(entity.GetType()))
                {
                    ISarReportTypeChangeLock behavior = SarReportTypeChangeLockBehaviorFactory.MakeISarReportTypeChangeLock(param, entity);
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
