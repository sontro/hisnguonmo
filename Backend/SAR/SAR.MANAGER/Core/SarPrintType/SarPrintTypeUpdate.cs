using SAR.MANAGER.Core.SarPrintType.Update;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintType
{
    partial class SarPrintTypeUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarPrintTypeUpdate(CommonParam param, object data)
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
                    ISarPrintTypeUpdate behavior = SarPrintTypeUpdateBehaviorFactory.MakeISarPrintTypeUpdate(param, entity);
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
