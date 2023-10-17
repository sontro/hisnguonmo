using HIS.Desktop.ADO;
using HIS.Desktop.Common;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TYT.Desktop.Plugins.FetusAbortion.TYTFetusAbortion
{
    class TYTFetusAbortionFactory
    {
        internal static ITYTFetusAbortion MakeITYTFetusAbortion(CommonParam param, object[] data)
        {
            ITYTFetusAbortion result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            V_HIS_PATIENT patient = null;
            long? fetusAbortionId = null;
            DelegateSelectData refeshData = null;

            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                            else if (data[i] is V_HIS_PATIENT)
                            {
                                patient = data[i] as V_HIS_PATIENT;
                            }
                            else if (data[i] is long)
                            {
                                fetusAbortionId = (long)data[i];
                            }
                            else if (data[i] is DelegateSelectData)
                            {
                                refeshData = (DelegateSelectData)data[i];
                            }
                        }
                        if (fetusAbortionId > 0)
                        {
                            result = new TYTFetusAbortionUpdateBehavior(param, fetusAbortionId ?? 0, refeshData, moduleData);
                        }
                        else
                        {
                            result = new TYTFetusAbortionBehavior(param, patient, moduleData);
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
