using SAR.MANAGER.Core.SarReport.Public;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReport
{
    partial class SarReportPublic : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportPublic(CommonParam param, object data)
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
                    ISarReportPublic behavior = SarReportPublicBehaviorFactory.MakeISarReportPublic(param, entity);
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
