using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestChmsCreate.ImpMestChmsCreate
{
    class ImpMestChmsCreateFactory
    {
        internal static IImpMestChmsCreate MakeIImpMestChmsCreate(CommonParam param, object[] data)
        {
            IImpMestChmsCreate result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            long expMestTypeId = 0;
            long? expMestId = null;
            HIS.Desktop.Common.DelegateRefreshData refreshData = null;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    if (data != null && data.Count() > 0)
                    {
                        for (int i = 0; i < data.Count(); i++)
                        {
                            if (data[i] is V_HIS_EXP_MEST)
                            {
                                expMestId = ((V_HIS_EXP_MEST)data[i]).ID;
                            }
                            else if (data[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)data[i];
                            }
                            else if (data[i] is long)
                            {
                                expMestTypeId = (long)data[i];
                            }
                            else if (data[i] is HIS.Desktop.Common.DelegateRefreshData)
                            {
                                refreshData = (HIS.Desktop.Common.DelegateRefreshData)data[i];
                            }

                        }

                        if (moduleData != null && expMestId.HasValue && expMestTypeId > 0)
                        {
                            result = new ImpMestChmsCreateBehavior(moduleData, param, expMestId.Value, expMestTypeId, refreshData);
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
