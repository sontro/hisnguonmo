using SDA.MANAGER.Core.SdaProvinceMap.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaProvinceMap
{
    partial class SdaProvinceMapUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaProvinceMapUpdate(CommonParam param, object data)
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
                    ISdaProvinceMapUpdate behavior = SdaProvinceMapUpdateBehaviorFactory.MakeISdaProvinceMapUpdate(param, entity);
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
