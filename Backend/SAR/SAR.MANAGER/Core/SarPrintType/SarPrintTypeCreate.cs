using SAR.MANAGER.Core.SarPrintType.Create;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintType
{
    partial class SarPrintTypeCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarPrintTypeCreate(CommonParam param, object data)
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
                    ISarPrintTypeCreate behavior = SarPrintTypeCreateBehaviorFactory.MakeISarPrintTypeCreate(param, entity);
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
