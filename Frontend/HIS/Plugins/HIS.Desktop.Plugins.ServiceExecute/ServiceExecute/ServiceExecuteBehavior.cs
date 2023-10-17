using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using HIS.Desktop.Plugins.ServiceExecute;
using Inventec.Common.Logging;

namespace HIS.Desktop.Plugins.ServiceExecute.ServiceExecute
{
    class ServiceExecuteBehavior : Tool<IDesktopToolContext>, IServiceExecute
    {
        object[] entity;
        internal ServiceExecuteBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IServiceExecute.Run()
        {
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            MOS.EFMODEL.DataModels.V_HIS_SERVICE_REQ serviceReq = null;
            bool? isExecuter = null, isReadResult = null;
            Common.DelegateRefresh RefreshData = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                        if (item is MOS.EFMODEL.DataModels.V_HIS_SERVICE_REQ)
                        {
                            serviceReq = (MOS.EFMODEL.DataModels.V_HIS_SERVICE_REQ)item;
                        }
                        if (item is HIS.Desktop.ADO.ServiceExecuteADO)
                        {
                            serviceReq = ((HIS.Desktop.ADO.ServiceExecuteADO)item).ServiceReq;
                            RefreshData = ((HIS.Desktop.ADO.ServiceExecuteADO)item).RefreshData;
                            isExecuter = ((HIS.Desktop.ADO.ServiceExecuteADO)item).IsExecuter;
                            isReadResult = ((HIS.Desktop.ADO.ServiceExecuteADO)item).IsReadResult;
                        }
                    }
                }
                if (moduleData != null && serviceReq != null)
                {
                    return new UCServiceExecute(moduleData, serviceReq, RefreshData, isExecuter, isReadResult);
                }
                else
                {
                    LogSystem.Info(LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => serviceReq), serviceReq));
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
