using SAR.MANAGER.Core.SarUserReportType.Update;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarUserReportType
{
    partial class SarUserReportTypeUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarUserReportTypeUpdate(CommonParam param, object data)
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
                    ISarUserReportTypeUpdate behavior = SarUserReportTypeUpdateBehaviorFactory.MakeISarUserReportTypeUpdate(param, entity);
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
