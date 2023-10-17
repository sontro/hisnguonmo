using SDA.MANAGER.Core.SdaGroup.CreateWithUpdatePath;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaGroup
{
    partial class SdaGroupCreateWithUpdatePath : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaGroupCreateWithUpdatePath(CommonParam param, object data)
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
                    ISdaGroupCreateWithUpdatePath behavior = SdaGroupCreateWithUpdatePathBehaviorFactory.MakeISdaGroupCreateWithUpdatePath(param, entity);
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
