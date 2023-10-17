using SAR.MANAGER.Core.SarPrint.Delete;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrint
{
    partial class SarPrintDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarPrintDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarPrint.Contains(entity.GetType()))
                {
                    ISarPrintDelete behavior = SarPrintDeleteBehaviorFactory.MakeISarPrintDelete(param, entity);
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
