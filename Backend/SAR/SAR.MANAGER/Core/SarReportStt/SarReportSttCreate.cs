using SAR.MANAGER.Core.SarReportStt.Create;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportStt
{
    partial class SarReportSttCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportSttCreate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarReportStt.Contains(entity.GetType()))
                {
                    ISarReportSttCreate behavior = SarReportSttCreateBehaviorFactory.MakeISarReportSttCreate(param, entity);
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
