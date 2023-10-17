using SAR.MANAGER.Core.SarPrintType.Delete;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintType
{
    partial class SarPrintTypeDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarPrintTypeDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarPrintType.Contains(entity.GetType()))
                {
                    ISarPrintTypeDelete behavior = SarPrintTypeDeleteBehaviorFactory.MakeISarPrintTypeDelete(param, entity);
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
