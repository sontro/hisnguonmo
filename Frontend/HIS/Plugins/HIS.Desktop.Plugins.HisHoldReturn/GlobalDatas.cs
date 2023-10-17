using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisHoldReturn
{
    internal class GlobalDatas
    {
        static Dictionary<long, HIS_EXP_MEST_TEMPLATE> expMestTemplates;
        internal static Dictionary<long, HIS_EXP_MEST_TEMPLATE> ExpMestTemplates
        {
            get
            {
                if (expMestTemplates == null)
                {
                    expMestTemplates = BackendDataWorker.Get<HIS_EXP_MEST_TEMPLATE>().ToDictionary(o => o.ID, o => o);
                }

                return expMestTemplates;
            }
            set { expMestTemplates = value; }
        }

        //internal static List<HIS_TREATMENT> Treatments;
        //internal async Task GetTreatments()
        //{
        //    if (Treatments == null)
        //    {
        //        Inventec.Common.Logging.LogSystem.Debug("GetTreatments. 1");
        //        CommonParam paramCommon = new CommonParam();
        //        dynamic filter = new System.Dynamic.ExpandoObject();
        //        filter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
        //        Treatments = await new Inventec.Common.Adapter.BackendAdapter(paramCommon).GetAsync<List<MOS.EFMODEL.DataModels.HIS_TREATMENT>>("api/HisTreatment/Get", ApiConsumers.MosConsumer, filter, paramCommon);
        //        Inventec.Common.Logging.LogSystem.Debug("GetTreatments. 2");
        //    }
        //}
    }
}
