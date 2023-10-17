using SDA.MANAGER.Core.SdaDistrictMap.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDistrictMap
{
    partial class SdaDistrictMapCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaDistrictMapCreate(CommonParam param, object data)
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
                    ISdaDistrictMapCreate behavior = SdaDistrictMapCreateBehaviorFactory.MakeISdaDistrictMapCreate(param, entity);
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
