using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.DispenseDetail.DispenseDetail
{
    class DispenseDetailBehavior : Tool<IDesktopToolContext>, IDispenseDetail
    {
        object[] entity;

        internal DispenseDetailBehavior()
            : base()
        {

        }

        internal DispenseDetailBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IDispenseDetail.Run()
        {
            MOS.EFMODEL.DataModels.HIS_DISPENSE result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is MOS.EFMODEL.DataModels.HIS_DISPENSE)
                            result = (MOS.EFMODEL.DataModels.HIS_DISPENSE)item;
                        if (item is Inventec.Desktop.Common.Modules.Module) 
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                    }
                }
                if (moduleData != null)
                {
                    return new frmDispenseDetail(result, moduleData);
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
