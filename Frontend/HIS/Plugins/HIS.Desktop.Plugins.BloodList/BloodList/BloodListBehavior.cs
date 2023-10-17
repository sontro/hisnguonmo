using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.BloodList.BloodList
{
    class BloodListBehavior : Tool<IDesktopToolContext>, IBloodList
    {
        object[] entity;
        internal BloodListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IBloodList.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                long bloodTypeId = 0;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        if (entity[i] is long)
                        {
                            bloodTypeId = (long)entity[i];
                        }
                    }
                }
                if (moduleData != null && bloodTypeId == 0)
                {
                    return new UCBloodList(moduleData);
                }
                else if (moduleData != null && bloodTypeId > 0)
                {
                    return new frmBloodList(moduleData, bloodTypeId);
                }
                else
                {
                    return null;
                }
                //return new UCBloodList();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
