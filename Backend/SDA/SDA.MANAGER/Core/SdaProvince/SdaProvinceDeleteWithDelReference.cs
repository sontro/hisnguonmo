using SDA.MANAGER.Core.SdaProvince.DeleteWithDelReference;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaProvince
{
    partial class SdaProvinceDeleteWithDelReference : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaProvinceDeleteWithDelReference(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaProvince.Contains(entity.GetType()))
                {
                    ISdaProvinceDeleteWithDelReference behavior = SdaProvinceDeleteWithDelReferenceBehaviorFactory.MakeISdaProvinceDeleteWithDelReference(param, entity);
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
