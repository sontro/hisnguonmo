using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Common;
using Inventec.Desktop.Core.Tools;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.Hospitalize.Hospitalize
{
    class HospitalizeBehavior : Tool<IDesktopToolContext>, IHospitalize
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module Module;
        V_HIS_SERVICE_REQ ServiceReq;
        long treatmentID;

        internal HospitalizeBehavior()
            : base()
        {
        }

        public HospitalizeBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHospitalize.Run()
        {
            object result = null;
            DelegateReturnSuccess returnSuccess = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            Module = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is long)
                        {
                            treatmentID = (long)item;
                        }
                        else if (item is V_HIS_SERVICE_REQ)
                        {
                            ServiceReq = (V_HIS_SERVICE_REQ)item;
                        }
                        else if (item is DelegateReturnSuccess)
                        {
                            returnSuccess = (DelegateReturnSuccess)item;
                        }
                        if (Module != null && treatmentID > 0)
                        {
                            result = new FormHospitalize(Module, treatmentID, ServiceReq, returnSuccess);
                            break;
                        }
                    }
                }
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
