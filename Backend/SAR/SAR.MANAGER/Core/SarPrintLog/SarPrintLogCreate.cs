using SAR.MANAGER.Core.SarPrintLog.Create;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintLog
{
    partial class SarPrintLogCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarPrintLogCreate(CommonParam param, object data)
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
                    ISarPrintLogCreate behavior = SarPrintLogCreateBehaviorFactory.MakeISarPrintLogCreate(param, entity);
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
