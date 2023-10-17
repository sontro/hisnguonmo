using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaSqlParam.Get.Ev;
using SDA.MANAGER.Core.SdaSqlParam.Get.ListEv;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaSqlParam
{
    partial class SdaSqlParamGet : BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaSqlParamGet(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<SDA_SQL_PARAM>))
                {
                    ISdaSqlParamGetListEv behavior = SdaSqlParamGetListEvBehaviorFactory.MakeISdaSqlParamGetListEv(param, entity);
                    result = behavior != null ? (T)System.Convert.ChangeType(behavior.Run(), typeof(T)) : default(T);
                }
                else if (typeof(T) == typeof(SDA_SQL_PARAM))
                {
                    ISdaSqlParamGetEv behavior = SdaSqlParamGetEvBehaviorFactory.MakeISdaSqlParamGetEv(param, entity);
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
