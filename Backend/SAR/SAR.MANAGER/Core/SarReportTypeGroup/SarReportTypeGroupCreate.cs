using SAR.MANAGER.Core.SarReportTypeGroup.Create;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTypeGroup
{
    partial class SarReportTypeGroupCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportTypeGroupCreate(CommonParam param, object data)
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
                    ISarReportTypeGroupCreate behavior = SarReportTypeGroupCreateBehaviorFactory.MakeISarReportTypeGroupCreate(param, entity);
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
