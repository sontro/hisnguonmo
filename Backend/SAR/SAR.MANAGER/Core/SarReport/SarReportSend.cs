using SAR.MANAGER.Core.SarReport.Send;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReport
{
    partial class SarReportSend : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportSend(CommonParam param, object data)
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
                    ISarReportSend behavior = SarReportSendBehaviorFactory.MakeISarReportSend(param, entity);
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
