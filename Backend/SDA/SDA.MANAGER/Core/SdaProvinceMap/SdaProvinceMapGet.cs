using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaProvinceMap.Get.Ev;
using SDA.MANAGER.Core.SdaProvinceMap.Get.ListEv;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaProvinceMap
{
    partial class SdaProvinceMapGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaProvinceMapGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SDA_PROVINCE_MAP>))
                {
                    ISdaProvinceMapGetListEv behavior = SdaProvinceMapGetListEvBehaviorFactory.MakeISdaProvinceMapGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SDA_PROVINCE_MAP))
                {
                    ISdaProvinceMapGetEv behavior = SdaProvinceMapGetEvBehaviorFactory.MakeISdaProvinceMapGetEv(param, entity);
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
