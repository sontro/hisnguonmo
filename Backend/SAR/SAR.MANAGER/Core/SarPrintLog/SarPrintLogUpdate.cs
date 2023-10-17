using SAR.MANAGER.Core.SarPrintLog.Update;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintLog
{
    partial class SarPrintLogUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarPrintLogUpdate(CommonParam param, object data)
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
                    ISarPrintLogUpdate behavior = SarPrintLogUpdateBehaviorFactory.MakeISarPrintLogUpdate(param, entity);
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
