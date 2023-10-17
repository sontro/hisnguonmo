using SDA.MANAGER.Core.SdaProvince.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaProvince
{
    partial class SdaProvinceDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaProvinceDelete(CommonParam param, object data)
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
                    ISdaProvinceDelete behavior = SdaProvinceDeleteBehaviorFactory.MakeISdaProvinceDelete(param, entity);
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
