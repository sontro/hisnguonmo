using SDA.MANAGER.Core.SdaNational.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaNational
{
    partial class SdaNationalDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaNationalDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaNational.Contains(entity.GetType()))
                {
                    ISdaNationalDelete behavior = SdaNationalDeleteBehaviorFactory.MakeISdaNationalDelete(param, entity);
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
