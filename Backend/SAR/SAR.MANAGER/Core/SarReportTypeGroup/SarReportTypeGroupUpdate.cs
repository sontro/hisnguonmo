using SAR.MANAGER.Core.SarReportTypeGroup.Update;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTypeGroup
{
    partial class SarReportTypeGroupUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportTypeGroupUpdate(CommonParam param, object data)
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
                    ISarReportTypeGroupUpdate behavior = SarReportTypeGroupUpdateBehaviorFactory.MakeISarReportTypeGroupUpdate(param, entity);
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
