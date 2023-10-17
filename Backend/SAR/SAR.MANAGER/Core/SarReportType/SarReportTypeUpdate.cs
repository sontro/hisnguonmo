using SAR.MANAGER.Core.SarReportType.Update;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportType
{
    partial class SarReportTypeUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportTypeUpdate(CommonParam param, object data)
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
                    ISarReportTypeUpdate behavior = SarReportTypeUpdateBehaviorFactory.MakeISarReportTypeUpdate(param, entity);
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
