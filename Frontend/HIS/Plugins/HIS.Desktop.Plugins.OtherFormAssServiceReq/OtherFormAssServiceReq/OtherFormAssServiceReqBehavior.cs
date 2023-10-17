using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.OtherFormAssServiceReq;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;
using HIS.Desktop.ADO;

namespace HIS.Desktop.Plugins.OtherFormAssServiceReq.OtherFormAssServiceReq
{
    public sealed class OtherFormAssServiceReqBehavior : Tool<IDesktopToolContext>, IOtherFormAssServiceReq
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        OtherFormAssServiceReqADO inputADO = null;
        public OtherFormAssServiceReqBehavior()
            : base()
        {
        }

        public OtherFormAssServiceReqBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IOtherFormAssServiceReq.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    inputADO = new OtherFormAssServiceReqADO();
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is long)
                        {
                            inputADO.ServiceReqId = (long)item;
                        }
                        else if (item is OtherFormAssServiceReqADO)
                        {
                            inputADO = (OtherFormAssServiceReqADO)item;
                        }
                    }

                    result = new frmOtherFormAssServiceReq(currentModule,inputADO);
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
