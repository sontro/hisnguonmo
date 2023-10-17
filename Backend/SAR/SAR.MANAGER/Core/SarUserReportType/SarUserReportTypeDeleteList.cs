using SAR.MANAGER.Core.SarUserReportType.DeleteList;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarUserReportType
{
    partial class SarUserReportTypeDeleteList : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarUserReportTypeDeleteList(CommonParam param, object data)
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
                    ISarUserReportTypeDeleteList behavior = SarUserReportTypeDeleteListBehaviorFactory.MakeISarUserReportTypeDeleteList(param, entity);
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
