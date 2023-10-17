using SDA.MANAGER.Core.SdaProvinceMap.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaProvinceMap
{
    partial class SdaProvinceMapDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaProvinceMapDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaProvinceMap.Contains(entity.GetType()))
                {
                    ISdaProvinceMapDelete behavior = SdaProvinceMapDeleteBehaviorFactory.MakeISdaProvinceMapDelete(param, entity);
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
