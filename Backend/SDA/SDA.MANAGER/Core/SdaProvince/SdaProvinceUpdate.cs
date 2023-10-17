using SDA.MANAGER.Core.SdaProvince.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaProvince
{
    partial class SdaProvinceUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaProvinceUpdate(CommonParam param, object data)
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
                    ISdaProvinceUpdate behavior = SdaProvinceUpdateBehaviorFactory.MakeISdaProvinceUpdate(param, entity);
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
