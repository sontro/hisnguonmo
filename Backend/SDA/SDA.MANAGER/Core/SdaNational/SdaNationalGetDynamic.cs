using Inventec.Core;
using SDA.MANAGER.Core.SdaNational.Get.ListDynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.MANAGER.Core.SdaNational
{
    class SdaNationalGetDynamic: BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaNationalGetDynamic(CommonParam param, object data)
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
                    ISdaNationalGetListDynamic behavior = SdaNationalGetListDynamicBehaviorFactory.MakeISdaNationalGetListDynamic(param, entity);
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
