using SAR.MANAGER.Core.SarPrintLog.Delete;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintLog
{
    partial class SarPrintLogDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarPrintLogDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarPrintLog.Contains(entity.GetType()))
                {
                    ISarPrintLogDelete behavior = SarPrintLogDeleteBehaviorFactory.MakeISarPrintLogDelete(param, entity);
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
