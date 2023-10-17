using SDA.MANAGER.Core.SdaNational.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaNational
{
    partial class SdaNationalUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaNationalUpdate(CommonParam param, object data)
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
                    ISdaNationalUpdate behavior = SdaNationalUpdateBehaviorFactory.MakeISdaNationalUpdate(param, entity);
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
