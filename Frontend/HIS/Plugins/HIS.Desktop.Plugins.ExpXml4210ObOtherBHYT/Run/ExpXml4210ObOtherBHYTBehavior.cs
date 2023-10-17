using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ExpXml4210ObOtherBHYT;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;
using HIS.Desktop.Plugins.ExpXml4210ObOtherBHYT.Run;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.ExpXml4210ObOtherBHYT.Run
{
    public sealed class ExpXml4210ObOtherBHYTBehavior : Tool<IDesktopToolContext>, IExpXml4210ObOtherBHYT
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        public ExpXml4210ObOtherBHYTBehavior()
            : base()
        {
        }

        public ExpXml4210ObOtherBHYTBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IExpXml4210ObOtherBHYT.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                    }
                    if (currentModule != null)
                    {
                        result = new UCExportXml(currentModule);
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
