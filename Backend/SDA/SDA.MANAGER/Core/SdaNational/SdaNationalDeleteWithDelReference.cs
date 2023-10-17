using Inventec.Core;
using System;
using SDA.MANAGER.Core.SdaNational.DeleteWithDelReference;

namespace SDA.MANAGER.Core.SdaNational
{
    partial class SdaNationalDeleteWithDelReference : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaNationalDeleteWithDelReference(CommonParam param, object data)
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
                    ISdaNationalDeleteWithDelReference behavior = SdaNationalDeleteWithDelReferenceBehaviorFactory.MakeISdaNationalDeleteWithDelReference(param, entity);
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
