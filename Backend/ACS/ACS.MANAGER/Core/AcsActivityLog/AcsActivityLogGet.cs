using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsActivityLog.Get.Ev;
using ACS.MANAGER.Core.AcsActivityLog.Get.ListEv;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsActivityLog
{
    partial class AcsActivityLogGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal AcsActivityLogGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<ACS_ACTIVITY_LOG>))
                {
                    IAcsActivityLogGetListEv behavior = AcsActivityLogGetListEvBehaviorFactory.MakeIAcsActivityLogGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(ACS_ACTIVITY_LOG))
                {
                    IAcsActivityLogGetEv behavior = AcsActivityLogGetEvBehaviorFactory.MakeIAcsActivityLogGetEv(param, entity);
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
