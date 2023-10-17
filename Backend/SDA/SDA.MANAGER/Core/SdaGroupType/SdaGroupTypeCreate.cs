using SDA.MANAGER.Core.SdaGroupType.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaGroupType
{
    partial class SdaGroupTypeCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaGroupTypeCreate(CommonParam param, object data)
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
                    ISdaGroupTypeCreate behavior = SdaGroupTypeCreateBehaviorFactory.MakeISdaGroupTypeCreate(param, entity);
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
