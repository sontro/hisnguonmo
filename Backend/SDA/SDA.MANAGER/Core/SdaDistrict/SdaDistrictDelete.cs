using SDA.MANAGER.Core.SdaDistrict.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDistrict
{
    partial class SdaDistrictDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaDistrictDelete(CommonParam param, object data)
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
                    ISdaDistrictDelete behavior = SdaDistrictDeleteBehaviorFactory.MakeISdaDistrictDelete(param, entity);
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
