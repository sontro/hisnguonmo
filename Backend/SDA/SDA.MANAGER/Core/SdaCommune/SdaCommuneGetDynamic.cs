using Inventec.Core;
using SDA.MANAGER.Core.SdaCommune.Get.ListDynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.MANAGER.Core.SdaCommune
{
    class SdaCommuneGetDynamic: BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaCommuneGetDynamic(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        T IDelegacyT.Execute<T>()
        {
            T result = default(T);
            try
            {
                if (typeof(T) == typeof(List<object>))
                {
                    ISdaCommuneGetListDynamic behavior = SdaCommuneGetListDynamicBehaviorFactory.MakeISdaCommuneGetListDynamic(param, entity);
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
