using SDA.MANAGER.Core.SdaDistrictMap.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDistrictMap
{
    partial class SdaDistrictMapDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaDistrictMapDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaDistrictMap.Contains(entity.GetType()))
                {
                    ISdaDistrictMapDelete behavior = SdaDistrictMapDeleteBehaviorFactory.MakeISdaDistrictMapDelete(param, entity);
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
