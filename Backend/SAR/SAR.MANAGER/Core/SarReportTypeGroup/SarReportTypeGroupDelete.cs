using SAR.MANAGER.Core.SarReportTypeGroup.Delete;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTypeGroup
{
    partial class SarReportTypeGroupDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportTypeGroupDelete(CommonParam param, object data)
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
                    ISarReportTypeGroupDelete behavior = SarReportTypeGroupDeleteBehaviorFactory.MakeISarReportTypeGroupDelete(param, entity);
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
