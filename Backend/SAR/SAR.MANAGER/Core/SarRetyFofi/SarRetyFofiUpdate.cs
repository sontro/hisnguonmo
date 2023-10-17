using SAR.MANAGER.Core.SarRetyFofi.Update;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarRetyFofi
{
    partial class SarRetyFofiUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarRetyFofiUpdate(CommonParam param, object data)
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
                    ISarRetyFofiUpdate behavior = SarRetyFofiUpdateBehaviorFactory.MakeISarRetyFofiUpdate(param, entity);
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
