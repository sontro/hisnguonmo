using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsControl.Get.Ev;
using ACS.MANAGER.Core.AcsControl.Get.ListEv;
//using ACS.MANAGER.Core.AcsControl.Get.ListV;
//using ACS.MANAGER.Core.AcsControl.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsControl
{
    partial class AcsControlGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal AcsControlGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<ACS_CONTROL>))
                {
                    IAcsControlGetListEv behavior = AcsControlGetListEvBehaviorFactory.MakeIAcsControlGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(ACS_CONTROL))
                {
                    IAcsControlGetEv behavior = AcsControlGetEvBehaviorFactory.MakeIAcsControlGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                //else if (typeof(T) == typeof(List<V_ACS_CONTROL>))
                //{
                //    IAcsControlGetListV behavior = AcsControlGetListVBehaviorFactory.MakeIAcsControlGetListV(param, entity);
                //    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                //}
                //else if (typeof(T) == typeof(V_ACS_CONTROL))
                //{
                //    IAcsControlGetV behavior = AcsControlGetVBehaviorFactory.MakeIAcsControlGetV(param, entity);
                //    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                //}
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
