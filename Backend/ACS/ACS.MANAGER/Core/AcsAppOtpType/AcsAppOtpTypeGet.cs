using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsAppOtpType.Get.Ev;
using ACS.MANAGER.Core.AcsAppOtpType.Get.ListEv;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsAppOtpType
{
    partial class AcsAppOtpTypeGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal AcsAppOtpTypeGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<ACS_APP_OTP_TYPE>))
                {
                    IAcsAppOtpTypeGetListEv behavior = AcsAppOtpTypeGetListEvBehaviorFactory.MakeIAcsAppOtpTypeGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(ACS_APP_OTP_TYPE))
                {
                    IAcsAppOtpTypeGetEv behavior = AcsAppOtpTypeGetEvBehaviorFactory.MakeIAcsAppOtpTypeGetEv(param, entity);
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
