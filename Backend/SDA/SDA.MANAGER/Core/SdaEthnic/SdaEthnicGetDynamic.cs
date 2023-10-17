using Inventec.Core;
using SDA.MANAGER.Core.SdaEthnic.Get.ListDynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.MANAGER.Core.SdaEthnic
{
    class SdaEthnicGetDynamic: BeanObjectBase, IDelegacyT
    {
        object entity;

        internal SdaEthnicGetDynamic(CommonParam param, object data)
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
                    ISdaEthnicGetListDynamic behavior = SdaEthnicGetListDynamicBehaviorFactory.MakeISdaEthnicGetListDynamic(param, entity);
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
