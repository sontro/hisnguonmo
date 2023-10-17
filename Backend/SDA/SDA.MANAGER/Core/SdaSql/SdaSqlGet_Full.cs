using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaSql.Get.Ev;
using SDA.MANAGER.Core.SdaSql.Get.ListEv;
using SDA.MANAGER.Core.SdaSql.Get.ListV;
using SDA.MANAGER.Core.SdaSql.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaSql
{
    partial class SdaSqlGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaSqlGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SDA_SQL>))
                {
                    ISdaSqlGetListEv behavior = SdaSqlGetListEvBehaviorFactory.MakeISdaSqlGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SDA_SQL))
                {
                    ISdaSqlGetEv behavior = SdaSqlGetEvBehaviorFactory.MakeISdaSqlGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_SDA_SQL>))
                {
                    ISdaSqlGetListV behavior = SdaSqlGetListVBehaviorFactory.MakeISdaSqlGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_SDA_SQL))
                {
                    ISdaSqlGetV behavior = SdaSqlGetVBehaviorFactory.MakeISdaSqlGetV(param, entity);
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
