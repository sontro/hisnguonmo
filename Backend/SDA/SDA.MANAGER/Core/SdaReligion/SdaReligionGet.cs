using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaReligion.Get.Ev;
using SDA.MANAGER.Core.SdaReligion.Get.ListEv;
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
