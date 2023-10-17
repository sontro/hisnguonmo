using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaDistrictMap.Get.Ev;
using SDA.MANAGER.Core.SdaDistrictMap.Get.ListEv;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaDistrictMap
{
    partial class SdaDistrictMapGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaDistrictMapGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SDA_DISTRICT_MAP>))
                {
                    ISdaDistrictMapGetListEv behavior = SdaDistrictMapGetListEvBehaviorFactory.MakeISdaDistrictMapGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SDA_DISTRICT_MAP))
                {
                    ISdaDistrictMapGetEv behavior = SdaDistrictMapGetEvBehaviorFactory.MakeISdaDistrictMapGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = default(T);
            }
            return result;
        }
    }
}
