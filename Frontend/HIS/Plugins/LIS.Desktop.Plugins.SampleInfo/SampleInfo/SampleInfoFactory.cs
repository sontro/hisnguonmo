using Inventec.Core;
using LIS.EFMODEL.DataModels;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SampleInfo.SampleInfo
{
    class SampleInfoFactory
    {
        internal static ISampleInfo MakeISampleInfo(CommonParam param, object[] data)
        {
            ISampleInfo result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            V_LIS_SAMPLE sample = null;
            string serviceReqCode = null;
            List< V_LIS_SAMPLE> sampleList = null;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is V_LIS_SAMPLE)
                            {
                                sample = (V_LIS_SAMPLE)data[i];
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                            else if (data[i] is string)
                            {
                                serviceReqCode = (string)data[i];
                            }else if (data[i] is List<V_LIS_SAMPLE>)
                            {
                                sampleList = (List<V_LIS_SAMPLE>)data[i];
                            }
                        }

                        if (moduleData != null && sample != null)
                        {
                            result = new SampleInfoBehavior(moduleData, param, sample);
                        }
                        else if (moduleData != null && serviceReqCode != null)
                        {
                            result = new SampleInfoBehavior(moduleData, param, serviceReqCode);
                        }else if (moduleData != null && sampleList != null && sampleList.Count > 0)
                        {
                            result = new SampleInfoBehavior(moduleData, param, sampleList);
                        }    
                    }
                }

                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + data.GetType().ToString() + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data), ex);
                result = null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
