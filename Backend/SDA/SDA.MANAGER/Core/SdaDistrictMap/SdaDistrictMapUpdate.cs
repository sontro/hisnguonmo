using SDA.MANAGER.Core.SdaDistrictMap.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDistrictMap
{
    partial class SdaDistrictMapUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaDistrictMapUpdate(CommonParam param, object data)
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
                    ISdaDistrictMapUpdate behavior = SdaDistrictMapUpdateBehaviorFactory.MakeISdaDistrictMapUpdate(param, entity);
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
