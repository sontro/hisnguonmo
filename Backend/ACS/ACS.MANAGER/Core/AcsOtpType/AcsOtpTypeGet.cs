using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsOtpType.Get.Ev;
using ACS.MANAGER.Core.AcsOtpType.Get.ListEv;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsOtpType
{
    partial class AcsOtpTypeGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal AcsOtpTypeGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<ACS_OTP_TYPE>))
                {
                    IAcsOtpTypeGetListEv behavior = AcsOtpTypeGetListEvBehaviorFactory.MakeIAcsOtpTypeGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(ACS_OTP_TYPE))
                {
                    IAcsOtpTypeGetEv behavior = AcsOtpTypeGetEvBehaviorFactory.MakeIAcsOtpTypeGetEv(param, entity);
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
