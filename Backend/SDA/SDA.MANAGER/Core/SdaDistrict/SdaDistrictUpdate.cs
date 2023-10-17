using SDA.MANAGER.Core.SdaDistrict.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDistrict
{
    partial class SdaDistrictUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaDistrictUpdate(CommonParam param, object data)
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
                    ISdaDistrictUpdate behavior = SdaDistrictUpdateBehaviorFactory.MakeISdaDistrictUpdate(param, entity);
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
