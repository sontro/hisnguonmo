using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsModule.Get.Ev;
using ACS.MANAGER.Core.AcsModule.Get.ListEv;
using ACS.MANAGER.Core.AcsModule.Get.ListV;
using ACS.MANAGER.Core.AcsModule.Get.V;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsModule
{
    partial class AcsModuleGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal AcsModuleGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<ACS_MODULE>))
                {
                    IAcsModuleGetListEv behavior = AcsModuleGetListEvBehaviorFactory.MakeIAcsModuleGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(ACS_MODULE))
                {
                    IAcsModuleGetEv behavior = AcsModuleGetEvBehaviorFactory.MakeIAcsModuleGetEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(List<V_ACS_MODULE>))
                {
                    IAcsModuleGetListV behavior = AcsModuleGetListVBehaviorFactory.MakeIAcsModuleGetListV(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(V_ACS_MODULE))
                {
                    IAcsModuleGetV behavior = AcsModuleGetVBehaviorFactory.MakeIAcsModuleGetV(param, entity);
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
