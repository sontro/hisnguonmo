using SDA.MANAGER.Core.SdaDistrict.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDistrict
{
    partial class SdaDistrictCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaDistrictCreate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaDistrict.Contains(entity.GetType()))
                {
                    ISdaDistrictCreate behavior = SdaDistrictCreateBehaviorFactory.MakeISdaDistrictCreate(param, entity);
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
