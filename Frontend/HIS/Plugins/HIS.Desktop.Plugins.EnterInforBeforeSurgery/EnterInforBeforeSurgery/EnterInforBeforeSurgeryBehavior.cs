using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.EnterInforBeforeSurgery;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.EFMODEL.DataModels;

namespace Inventec.Desktop.Plugins.EnterInforBeforeSurgery.EnterInforBeforeSurgery
{
    public sealed class EnterInforBeforeSurgeryBehavior : Tool<IDesktopToolContext>, IEnterInforBeforeSurgery
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        public EnterInforBeforeSurgeryBehavior()
            : base()
        {
        }

        public EnterInforBeforeSurgeryBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IEnterInforBeforeSurgery.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    HIS_SERVICE_REQ data = null;
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is HIS_SERVICE_REQ)
                        {
                            data = (HIS_SERVICE_REQ)item;
                        }
                    }
                    if (currentModule != null && data != null)
                    {
                        result = new frmEnterInforBeforeSurgery(currentModule, data);
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
