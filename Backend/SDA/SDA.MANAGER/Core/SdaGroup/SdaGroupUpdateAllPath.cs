using SDA.MANAGER.Core.SdaGroup.UpdateAllPath;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaGroup
{
    partial class SdaGroupUpdateAllPath : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaGroupUpdateAllPath(CommonParam param, object data)
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
                    ISdaGroupUpdateAllPath behavior = SdaGroupUpdateAllPathBehaviorFactory.MakeISdaGroupUpdateAllPath(param, entity);
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
