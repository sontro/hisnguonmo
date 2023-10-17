using SAR.MANAGER.Core.SarUserReportType.CreateList;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarUserReportType
{
    partial class SarUserReportTypeCreateList : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarUserReportTypeCreateList(CommonParam param, object data)
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
                    ISarUserReportTypeCreateList behavior = SarUserReportTypeCreateListBehaviorFactory.MakeISarUserReportTypeCreateList(param, entity);
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
