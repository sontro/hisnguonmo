using HIS.Desktop.ApiConsumer;
using Inventec.Common.Adapter;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignService
{
    class SereServGet
    {
        static List<MOS.EFMODEL.DataModels.HIS_SERE_SERV> sereservs;
        internal static List<MOS.EFMODEL.DataModels.HIS_SERE_SERV> GetByServiceReqId(long serviceReqId)
        {
            try
            {
                //if (sereservs == null || sereservs.Count == 0)
                //{
                    CommonParam param = new CommonParam();
                    MOS.Filter.HisSereServFilter sereServFilter = new MOS.Filter.HisSereServFilter();
                    sereServFilter.SERVICE_REQ_ID = serviceReqId;
                    sereservs = new BackendAdapter(param).Get<List<MOS.EFMODEL.DataModels.HIS_SERE_SERV>>(HisRequestUriStore.HIS_SERE_SERV_GET, ApiConsumers.MosConsumer, sereServFilter, HIS.Desktop.Controls.Session.SessionManager.ActionLostToken, param);
                //}
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return sereservs;
        }

        public void DisposeSereServ()
        {
            try
            {
                sereservs = null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
