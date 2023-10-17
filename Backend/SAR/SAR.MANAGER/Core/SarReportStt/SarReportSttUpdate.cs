using SAR.MANAGER.Core.SarReportStt.Update;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportStt
{
    partial class SarReportSttUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportSttUpdate(CommonParam param, object data)
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
                    ISarReportSttUpdate behavior = SarReportSttUpdateBehaviorFactory.MakeISarReportSttUpdate(param, entity);
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
