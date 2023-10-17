using SDA.MANAGER.Core.SdaGroup.UpdateWithUpdatePath;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaGroup
{
    partial class SdaGroupUpdateWithUpdatePath : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaGroupUpdateWithUpdatePath(CommonParam param, object data)
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
                    ISdaGroupUpdateWithUpdatePath behavior = SdaGroupUpdateWithUpdatePathBehaviorFactory.MakeISdaGroupUpdateWithUpdatePath(param, entity);
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
