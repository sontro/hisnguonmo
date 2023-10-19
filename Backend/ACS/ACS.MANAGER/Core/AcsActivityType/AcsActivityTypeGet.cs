using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsActivityType.Get.Ev;
using ACS.MANAGER.Core.AcsActivityType.Get.ListEv;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsActivityType
{
    partial class AcsActivityTypeGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal AcsActivityTypeGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<ACS_ACTIVITY_TYPE>))
                {
                    IAcsActivityTypeGetListEv behavior = AcsActivityTypeGetListEvBehaviorFactory.MakeIAcsActivityTypeGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(ACS_ACTIVITY_TYPE))
                {
                    IAcsActivityTypeGetEv behavior = AcsActivityTypeGetEvBehaviorFactory.MakeIAcsActivityTypeGetEv(param, entity);
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
