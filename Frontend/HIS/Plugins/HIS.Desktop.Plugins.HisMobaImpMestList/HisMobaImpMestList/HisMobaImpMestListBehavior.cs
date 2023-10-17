using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.HisMobaImpMestList.HisMobaImpMestList
{
    class HisMobaImpMestListBehavior : Tool<IDesktopToolContext>, IHisMobaImpMestList
    {
        object[] entity;
        internal HisMobaImpMestListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisMobaImpMestList.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                ADO.MobaImpMestListADO ado = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        else if (entity[i] is ADO.MobaImpMestListADO)
                        {
                            ado = (ADO.MobaImpMestListADO)entity[i];
                        }
                    }
                }
                if (moduleData != null)
                {
                    if (ado != null && ado.MODULE_TYPE == Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__FORM)
                    {
                        return new FormHisMobaImpMestList(moduleData, ado);
                    }
                    else
                        return new UCHisMobaImpMestList(moduleData.RoomId, moduleData.RoomTypeId);
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
