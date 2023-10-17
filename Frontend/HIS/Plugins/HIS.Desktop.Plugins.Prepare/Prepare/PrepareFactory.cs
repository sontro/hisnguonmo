
using HIS.Desktop.Common;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.Desktop.Plugins.Prepare.Prepare
{
    class PrepareFactory
    {
        internal static IPrepare MakeIPrepare(CommonParam param, object[] data)
        {
            IPrepare result = null;
            HIS_TREATMENT treatment = null;
            HIS_PREPARE prepare = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            DelegateRefreshData refeshData = null;
            try
            {

                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is HIS_TREATMENT)
                            {
                                treatment = ((HIS_TREATMENT)data[i]);
                            }
                            else if (data[i] is HIS_PREPARE)
                            {
                                prepare = ((HIS_PREPARE)data[i]);
                            }
                            else if (data[i] is DelegateRefreshData)
                            {
                                refeshData = ((DelegateRefreshData)data[i]);
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                        }

                        if (moduleData != null && treatment != null)
                        {
                            result = new PrepareBehavior(param, treatment, moduleData);
                        }

                        else if (moduleData != null && prepare != null)
                        {
                            result = new PrepareUpdateBehavior(param, prepare, moduleData, refeshData);
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
