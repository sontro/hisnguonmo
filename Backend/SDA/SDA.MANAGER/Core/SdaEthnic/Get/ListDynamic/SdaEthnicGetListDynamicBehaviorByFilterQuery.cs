using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.MANAGER.Core.SdaEthnic.Get.ListDynamic
{
    class SdaEthnicGetListDynamicBehaviorByFilterQuery: DynamicBase, ISdaEthnicGetListDynamic
    {
        SdaEthnicFilterQuery filterQuery;

        internal SdaEthnicGetListDynamicBehaviorByFilterQuery(CommonParam param, SdaEthnicFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<object> ISdaEthnicGetListDynamic.Run()
        {
            List<object> result = new List<object>();
            try
            {
                result = this.RunBase("SDA_ETHNIC", filterQuery);
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
