using SDA.MANAGER.Core.SdaGroupType.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaGroupType
{
    partial class SdaGroupTypeDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaGroupTypeDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaGroupType.Contains(entity.GetType()))
                {
                    ISdaGroupTypeDelete behavior = SdaGroupTypeDeleteBehaviorFactory.MakeISdaGroupTypeDelete(param, entity);
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
