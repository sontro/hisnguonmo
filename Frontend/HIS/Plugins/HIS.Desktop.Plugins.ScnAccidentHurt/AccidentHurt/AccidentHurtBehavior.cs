using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.ScnAccidentHurt.AccidentHurt
{
    class AccidentHurtBehavior : Tool<IDesktopToolContext>, IAccidentHurt
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal AccidentHurtBehavior()
            : base()
        {

        }

        internal AccidentHurtBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IAccidentHurt.Run()
        {
            object result = null;
            try
            {
                string _PersonCode = "";
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is string)
                        {
                            _PersonCode = (string)item;
                        }
                        else if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        if (currentModule != null && !string.IsNullOrEmpty(_PersonCode))
                        {
                            result = new frmAccidentHurt(currentModule, _PersonCode);
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
