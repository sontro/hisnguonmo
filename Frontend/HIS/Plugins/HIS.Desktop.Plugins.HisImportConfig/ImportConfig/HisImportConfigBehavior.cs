using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using HIS.Desktop.Plugins.HisImportConfig;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.Common;


namespace HIS.Desktop.Plugins.HisImportConfig.ImportConfig.Run
{
   public sealed class HisImportConfigBehavior : Tool<IDesktopToolContext>, IHisImportConfig

    {
       object[] entity;
       Inventec.Desktop.Common.Modules.Module currentModule;
       RefeshReference delegateRefresh;
       public HisImportConfigBehavior()
           : base()
       { }
       public HisImportConfigBehavior(CommonParam param, object[] filter)
           : base()
       { this.entity = filter; }
       object IHisImportConfig.Run()
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
                       else if (item is RefeshReference)
                       {
                           delegateRefresh = (RefeshReference)item;
                       }
                   }
                   if (currentModule != null && delegateRefresh != null)
                   {
                       result = new frmImportConfig(currentModule, delegateRefresh);
                   }
                   else
                   {
                       result = new frmImportConfig(currentModule);
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
