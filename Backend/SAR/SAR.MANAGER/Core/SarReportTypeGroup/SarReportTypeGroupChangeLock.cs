using SAR.MANAGER.Core.SarReportTypeGroup.Lock;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTypeGroup
{
    partial class SarReportTypeGroupChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportTypeGroupChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarReportTypeGroup.Contains(entity.GetType()))
                {
                    ISarReportTypeGroupChangeLock behavior = SarReportTypeGroupChangeLockBehaviorFactory.MakeISarReportTypeGroupChangeLock(param, entity);
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
