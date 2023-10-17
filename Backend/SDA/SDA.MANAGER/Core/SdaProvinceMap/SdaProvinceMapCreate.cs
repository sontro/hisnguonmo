using SDA.MANAGER.Core.SdaProvinceMap.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaProvinceMap
{
    partial class SdaProvinceMapCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaProvinceMapCreate(CommonParam param, object data)
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
                    ISdaProvinceMapCreate behavior = SdaProvinceMapCreateBehaviorFactory.MakeISdaProvinceMapCreate(param, entity);
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
