using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaReligion.Get.Ev;
using SDA.MANAGER.Core.SdaReligion.Get.ListEv;
using SDA.MANAGER.Core.SdaReligion.Get.ListV;
using SDA.MANAGER.Core.SdaReligion.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaReligion
{
    partial class SdaReligionGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaReligionGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SDA_RELIGION>))
                {
                    ISdaReligionGetListEv behavior = SdaReligionGetListEvBehaviorFactory.MakeISdaReligionGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SDA_RELIGION))
                {
                    ISdaReligionGetEv behavior = SdaReligionGetEvBehaviorFactory.MakeISdaReligionGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_SDA_RELIGION>))
                {
                    ISdaReligionGetListV behavior = SdaReligionGetListVBehaviorFactory.MakeISdaReligionGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_SDA_RELIGION))
                {
                    ISdaReligionGetV behavior = SdaReligionGetVBehaviorFactory.MakeISdaReligionGetV(param, entity);
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
