using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaDeleteData.Get.Ev;
using SDA.MANAGER.Core.SdaDeleteData.Get.ListEv;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaDeleteData
{
    partial class SdaDeleteDataGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaDeleteDataGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SDA_DELETE_DATA>))
                {
                    ISdaDeleteDataGetListEv behavior = SdaDeleteDataGetListEvBehaviorFactory.MakeISdaDeleteDataGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SDA_DELETE_DATA))
                {
                    ISdaDeleteDataGetEv behavior = SdaDeleteDataGetEvBehaviorFactory.MakeISdaDeleteDataGetEv(param, entity);
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
