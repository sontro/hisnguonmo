using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.ConfigApplication;
using HIS.Desktop.Plugins.Library.PrintOtherForm.Base;
using HIS.Desktop.Plugins.Library.PrintOtherForm.RunPrintTemplate;
using Inventec.Common.Adapter;
using Inventec.Common.Logging;
using Inventec.Common.ThreadCustom;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintOtherForm.MpsBehavior.Mps000408
{
    public partial class Mps000408Behavior : MpsDataBase, ILoad
    {
        private void LoadData()
        {
            try
            {
                if (this.inputAdo != null)
                {
                    List<Action> methods = new List<Action>();
                    methods.Add(LoadServiceReq);
                    methods.Add(LoadSereServPttt);
                    ThreadCustomManager.MultipleThreadWithJoin(methods);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadServiceReq()
        {
            try
            {
                if (this.inputAdo.ServiceReqId > 0)
                {
                    CommonParam param = new CommonParam();
                    HisServiceReqViewFilter filter = new HisServiceReqViewFilter();
                    filter.ID = this.inputAdo.ServiceReqId;
                    var listServiceReq = new BackendAdapter(param).Get<List<V_HIS_SERVICE_REQ>>("api/HisServiceReq/GetView", ApiConsumers.MosConsumer, filter, param);
                    if (listServiceReq != null && listServiceReq.Count > 0)
                    {
                        this.serviceReq = listServiceReq.FirstOrDefault();
                    }
                    LogSystem.Debug(LogUtil.TraceData("serviceReq", serviceReq));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadSereServPttt()
        {
            try
            {
                if (this.inputAdo.SereServId.HasValue)
                {
                    CommonParam param = new CommonParam();
                    HisSereServPtttViewFilter filter = new HisSereServPtttViewFilter();
                    filter.SERE_SERV_ID = this.inputAdo.SereServId.Value;
                    var listSsPttt = new BackendAdapter(param).Get<List<V_HIS_SERE_SERV_PTTT>>("api/HisSereServPttt/GetView", ApiConsumers.MosConsumer, filter, param);
                    if (listSsPttt != null && listSsPttt.Count > 0)
                    {
                        this.sereServPTTT = listSsPttt.FirstOrDefault();
                    }
                    LogSystem.Debug(LogUtil.TraceData("sereServPTTT", sereServPTTT));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
