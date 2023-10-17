using HIS.Desktop.Controls.Session;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ExpMestSaleCreate.ExpMestSaleCreate
{
    class ExpMestSaleCreateBehavior : Tool<IDesktopToolContext>, IExpMestSaleCreate
    {
        object[] entity;

        internal ExpMestSaleCreateBehavior()
            : base()
        {

        }

        internal ExpMestSaleCreateBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IExpMestSaleCreate.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                long? expMestId = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        else if (entity[i] is long)
                        {
                            expMestId = (long)entity[i];
                        }
                    }
                }
                if (moduleData != null)
                {
                    UserControl uc = new UCExpMestSaleCreate(moduleData, expMestId);
                    HIS.Desktop.ModuleExt.TabControlBaseProcess.TabCreating(SessionManager.GetTabControlMain(), "XUAT_BAN", "Xuất bán", uc, moduleData, ReleaseBeforeClose);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        private void ReleaseBeforeClose(object uc)
        {
            try
            {
                MethodInfo methodInfo = (uc as UserControl).GetType().GetMethod("FromClosingEvent");
                methodInfo.Invoke(uc, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
