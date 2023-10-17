using HIS.Desktop.ADO;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using LIS.EFMODEL.DataModels;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Desktop.Plugins.LisAntibioticRange.LisAntibioticRange
{
    class LisAntibioticRangeBehavior : Tool<IDesktopToolContext>, ILisAntibioticRange
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        internal LisAntibioticRangeBehavior()
            : base()
        {

        }

        internal LisAntibioticRangeBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ILisAntibioticRange.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;

                    }
                }

                if (currentModule != null)
                {
                    return new LIS.Desktop.Plugins.LisAntibioticRange.Run.frmLisAntibioticRange(currentModule);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
