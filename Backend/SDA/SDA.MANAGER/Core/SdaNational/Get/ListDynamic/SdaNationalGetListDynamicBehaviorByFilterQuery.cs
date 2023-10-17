using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.MANAGER.Core.SdaNational.Get.ListDynamic
{
    class SdaNationalGetListDynamicBehaviorByFilterQuery: DynamicBase, ISdaNationalGetListDynamic
    {
        SdaNationalFilterQuery filterQuery;

        internal SdaNationalGetListDynamicBehaviorByFilterQuery(CommonParam param, SdaNationalFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<object> ISdaNationalGetListDynamic.Run()
        {
            List<object> result = new List<object>();
            try
            {
                result = this.RunBase("SDA_NATIONAL", filterQuery);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
            return result;
        }

        protected override string ProcessFilterQuery()
        {
            string strFilterCondition = "";
            return strFilterCondition;
        }
    }
}
