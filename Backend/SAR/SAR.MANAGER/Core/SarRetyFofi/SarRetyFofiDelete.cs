using SAR.MANAGER.Core.SarRetyFofi.Delete;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarRetyFofi
{
    partial class SarRetyFofiDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarRetyFofiDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarRetyFofi.Contains(entity.GetType()))
                {
                    ISarRetyFofiDelete behavior = SarRetyFofiDeleteBehaviorFactory.MakeISarRetyFofiDelete(param, entity);
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
