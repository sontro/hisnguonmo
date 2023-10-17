using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ImpMestAggregate;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ImpMestAggregate.ImpMestAggregate
{
    public sealed class ImpMestAggregateBehavior : Tool<IDesktopToolContext>, IImpMestAggregate
    {
        object[] entity;
        public ImpMestAggregateBehavior()
            : base()
        {
        }

        public ImpMestAggregateBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IImpMestAggregate.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        if (moduleData != null)
                        {
                            return new UCImpMestAggregate(moduleData);
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
