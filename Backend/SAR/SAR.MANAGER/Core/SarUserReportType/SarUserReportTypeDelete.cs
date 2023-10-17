using SAR.MANAGER.Core.SarUserReportType.Delete;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarUserReportType
{
    partial class SarUserReportTypeDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarUserReportTypeDelete(CommonParam param, object data)
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
                    ISarUserReportTypeDelete behavior = SarUserReportTypeDeleteBehaviorFactory.MakeISarUserReportTypeDelete(param, entity);
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
