using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.MANAGER.Core.SdaCommune.Get.ListDynamic
{
    class SdaCommuneGetListDynamicBehaviorByFilterQuery : DynamicBase, ISdaCommuneGetListDynamic
    {
        SdaCommuneViewFilterQuery filterQuery;

        internal SdaCommuneGetListDynamicBehaviorByFilterQuery(CommonParam param, SdaCommuneViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<object> ISdaCommuneGetListDynamic.Run()
        {
            List<object> result = new List<object>();
            try
            {
                result = this.RunBase("V_SDA_COMMUNE", filterQuery);
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
            if (filterQuery.DISTRICT_ID.HasValue)
            {
                strFilterCondition += string.Format(" AND DISTRICT_ID = {0}", filterQuery.DISTRICT_ID.Value);
            }
            return strFilterCondition;
        }
    }
}
