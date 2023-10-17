using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.AntibioticRequestList.AntibioticRequestList
{
    class AntibioticRequestListBehavior : Tool<IDesktopToolContext>, IAntibioticRequestList
    {
        object[] entity;
        string treatmentcode;
        internal AntibioticRequestListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IAntibioticRequestList.Run()
        {
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
                        if (entity[i] is string)
                        {
                            treatmentcode = entity[i].ToString();
                        }
                    }
                }
                if (moduleData != null && !string.IsNullOrEmpty(treatmentcode))
                {
                    return new UCAntibioticRequestList(moduleData,treatmentcode);
                }
                else if (moduleData != null)
                {
                    return new UCAntibioticRequestList(moduleData);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
