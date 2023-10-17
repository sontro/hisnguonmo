using SDA.MANAGER.Core.SdaGroupType.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaGroupType
{
    partial class SdaGroupTypeUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaGroupTypeUpdate(CommonParam param, object data)
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
                    ISdaGroupTypeUpdate behavior = SdaGroupTypeUpdateBehaviorFactory.MakeISdaGroupTypeUpdate(param, entity);
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
