using SAR.MANAGER.Core.SarUserReportType.Lock;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarUserReportType
{
    partial class SarUserReportTypeChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarUserReportTypeChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarUserReportType.Contains(entity.GetType()))
                {
                    ISarUserReportTypeChangeLock behavior = SarUserReportTypeChangeLockBehaviorFactory.MakeISarUserReportTypeChangeLock(param, entity);
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
