using SDA.MANAGER.Core.SdaSql.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSql
{
    partial class SdaSqlDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaSqlDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaSql.Contains(entity.GetType()))
                {
                    ISdaSqlDelete behavior = SdaSqlDeleteBehaviorFactory.MakeISdaSqlDelete(param, entity);
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
