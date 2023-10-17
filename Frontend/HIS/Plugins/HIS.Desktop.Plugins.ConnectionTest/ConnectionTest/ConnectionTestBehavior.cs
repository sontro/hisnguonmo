using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ConnectionTest.ConnectionTest
{
    class ConnectionTestBehavior : Tool<IDesktopToolContext>, IConnectionTest
    {
        object[] entity;        
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal ConnectionTestBehavior()
            : base()
        {

        }

        internal ConnectionTestBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IConnectionTest.Run()
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
                        result = new UC_ConnectionTest(currentModule);
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
