using SDA.MANAGER.Core.SdaGroup.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaGroup
{
    partial class SdaGroupDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaGroupDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaGroup.Contains(entity.GetType()))
                {
                    ISdaGroupDelete behavior = SdaGroupDeleteBehaviorFactory.MakeISdaGroupDelete(param, entity);
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
