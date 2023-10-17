using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.MANAGER.Core.SdaProvince.Get.ListDynamic
{
    class SdaProvinceGetListDynamicBehaviorByFilterQuery: DynamicBase, ISdaProvinceGetListDynamic
    {
        SdaProvinceViewFilterQuery filterQuery;

        internal SdaProvinceGetListDynamicBehaviorByFilterQuery(CommonParam param, SdaProvinceViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<object> ISdaProvinceGetListDynamic.Run()
        {
            List<object> result = new List<object>();
            try
            {
                result = this.RunBase("V_SDA_PROVINCE", filterQuery);
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
