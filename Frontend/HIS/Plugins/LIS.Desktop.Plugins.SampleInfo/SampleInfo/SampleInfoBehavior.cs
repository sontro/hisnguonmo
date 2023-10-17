using HIS.Desktop.ADO;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using LIS.Desktop.Plugins.SampleInfo;
using LIS.EFMODEL.DataModels;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SampleInfo.SampleInfo
{
    class SampleInfoBehavior : Tool<IDesktopToolContext>, ISampleInfo
    {
        V_LIS_SAMPLE sample = null;
        List<V_LIS_SAMPLE> sampleList = null;
        Inventec.Desktop.Common.Modules.Module Module;
        string serviceReqCode = null;
        internal SampleInfoBehavior()
            : base()
        {

        }
        internal SampleInfoBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, List<V_LIS_SAMPLE> data)
           : base()
        {
            this.Module = module;
            this.sampleList = data;
        }
        internal SampleInfoBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, V_LIS_SAMPLE data)
            : base()
        {
            this.Module = module;
            this.sample = data;
        }
        internal SampleInfoBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, string data)
            : base()
        {
            this.Module = module;
            this.serviceReqCode = data;
        }
        internal SampleInfoBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param)
            : base()
        {
            this.Module = module;
        }

        object ISampleInfo.Run()
        {
            object result = null;
            try
            {
                if (sample != null)
                {
                    result = new frmSampleInfo(Module, sample);
                    if (result == null) throw new NullReferenceException(LogUtil.TraceData("sample", sample));
                }
                else if (sampleList != null)
                {
                    result = new frmSampleInfo(Module, sampleList);
                    if (result == null) throw new NullReferenceException(LogUtil.TraceData("sampleList", sampleList));
                }
                else if (!String.IsNullOrWhiteSpace(serviceReqCode))
                {
                    result = new frmSampleInfo(Module, serviceReqCode);
                    if (result == null) throw new NullReferenceException(LogUtil.TraceData("serviceReqCode", serviceReqCode));
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
