using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.MANAGER.Core.SdaDistrict.Get.ListDynamic
{
    class SdaDistrictGetListDynamicBehaviorByFilterQuery: DynamicBase, ISdaDistrictGetListDynamic
    {
        SdaDistrictViewFilterQuery filterQuery;

        internal SdaDistrictGetListDynamicBehaviorByFilterQuery(CommonParam param, SdaDistrictViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<object> ISdaDistrictGetListDynamic.Run()
        {
            List<object> result = new List<object>();
            try
            {
                result = this.RunBase("V_SDA_DISTRICT", filterQuery);
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
            if (filterQuery.PROVINCE_ID.HasValue)
            {
                strFilterCondition += string.Format(" AND PROVINCE_ID = {0}", filterQuery.PROVINCE_ID.Value);
            }
            return strFilterCondition;
        }
    }
}
