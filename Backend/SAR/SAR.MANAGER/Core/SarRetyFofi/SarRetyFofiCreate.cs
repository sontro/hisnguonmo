using SAR.MANAGER.Core.SarRetyFofi.Create;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarRetyFofi
{
    partial class SarRetyFofiCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarRetyFofiCreate(CommonParam param, object data)
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
                    ISarRetyFofiCreate behavior = SarRetyFofiCreateBehaviorFactory.MakeISarRetyFofiCreate(param, entity);
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
