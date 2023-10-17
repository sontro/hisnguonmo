using SDA.MANAGER.Core.SdaProvince.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaProvince
{
    partial class SdaProvinceCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaProvinceCreate(CommonParam param, object data)
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
                    ISdaProvinceCreate behavior = SdaProvinceCreateBehaviorFactory.MakeISdaProvinceCreate(param, entity);
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
