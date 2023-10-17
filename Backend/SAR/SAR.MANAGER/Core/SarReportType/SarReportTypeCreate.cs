using SAR.MANAGER.Core.SarReportType.Create;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportType
{
    partial class SarReportTypeCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportTypeCreate(CommonParam param, object data)
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
                    ISarReportTypeCreate behavior = SarReportTypeCreateBehaviorFactory.MakeISarReportTypeCreate(param, entity);
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
