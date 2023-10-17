using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DiscountSereServ.DiscountSereServ
{
    class DiscountSereServBehavior : Tool<IDesktopToolContext>, IDiscountSereServ
    {
        Inventec.Desktop.Common.Modules.Module moduleData = null;
        HIS_SERE_SERV sereServ = null;

        internal DiscountSereServBehavior()
            : base()
        {

        }

        internal DiscountSereServBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, HIS_SERE_SERV data)
            : base()
        {
            sereServ = data;
            moduleData = module;
        }

        object IDiscountSereServ.Run()
        {
            object result = null;
            try
            {
                result = new frmDiscountSereServ(moduleData, sereServ);
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
