using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsModule.Get.Ev;
using ACS.MANAGER.Core.AcsModule.Get.ListEv;
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
