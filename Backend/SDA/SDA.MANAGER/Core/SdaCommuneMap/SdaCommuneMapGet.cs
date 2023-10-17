using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaCommuneMap.Get.Ev;
using SDA.MANAGER.Core.SdaCommuneMap.Get.ListEv;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaCommuneMap
{
    partial class SdaCommuneMapGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaCommuneMapGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SDA_COMMUNE_MAP>))
                {
                    ISdaCommuneMapGetListEv behavior = SdaCommuneMapGetListEvBehaviorFactory.MakeISdaCommuneMapGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SDA_COMMUNE_MAP))
                {
                    ISdaCommuneMapGetEv behavior = SdaCommuneMapGetEvBehaviorFactory.MakeISdaCommuneMapGetEv(param, entity);
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
