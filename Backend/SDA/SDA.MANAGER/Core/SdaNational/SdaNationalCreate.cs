using SDA.MANAGER.Core.SdaNational.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaNational
{
    partial class SdaNationalCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaNationalCreate(CommonParam param, object data)
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
                    ISdaNationalCreate behavior = SdaNationalCreateBehaviorFactory.MakeISdaNationalCreate(param, entity);
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
