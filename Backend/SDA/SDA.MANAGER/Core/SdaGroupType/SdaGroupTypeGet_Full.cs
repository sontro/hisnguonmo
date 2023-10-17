using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaGroupType.Get.Ev;
using SDA.MANAGER.Core.SdaGroupType.Get.ListEv;
using SDA.MANAGER.Core.SdaGroupType.Get.ListV;
using SDA.MANAGER.Core.SdaGroupType.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaGroupType
{
    partial class SdaGroupTypeGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaGroupTypeGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SDA_GROUP_TYPE>))
                {
                    ISdaGroupTypeGetListEv behavior = SdaGroupTypeGetListEvBehaviorFactory.MakeISdaGroupTypeGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SDA_GROUP_TYPE))
                {
                    ISdaGroupTypeGetEv behavior = SdaGroupTypeGetEvBehaviorFactory.MakeISdaGroupTypeGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_SDA_GROUP_TYPE>))
                {
                    ISdaGroupTypeGetListV behavior = SdaGroupTypeGetListVBehaviorFactory.MakeISdaGroupTypeGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_SDA_GROUP_TYPE))
                {
                    ISdaGroupTypeGetV behavior = SdaGroupTypeGetVBehaviorFactory.MakeISdaGroupTypeGetV(param, entity);
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
