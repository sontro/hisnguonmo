using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.HisMestInveUser;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.Plugins.HisMestInveUser.Run;
using MOS.EFMODEL.DataModels;
using MOS.SDO;

namespace Inventec.Desktop.Plugins.HisMestInveUser.Run
{
    public sealed class HisMestInveUserBehavior : Tool<IDesktopToolContext>, IHisMestInveUser
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        V_HIS_MEDI_STOCK_PERIOD HisMediStockPeriod = new V_HIS_MEDI_STOCK_PERIOD();
        HisExpMestResultSDO hisLostExpMest = new HisExpMestResultSDO();
        public HisMestInveUserBehavior()
            : base()
        {
        }

        public HisMestInveUserBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisMestInveUser.Run()
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
                        else if (item is V_HIS_MEDI_STOCK_PERIOD)
                        {
                            HisMediStockPeriod = (V_HIS_MEDI_STOCK_PERIOD)item;
                        }
                        else if (item is HisExpMestResultSDO)
                        {
                            hisLostExpMest = (HisExpMestResultSDO)item;
                        }
                    }
                    if (currentModule != null && HisMediStockPeriod != null && HisMediStockPeriod.ID > 0)
                    {
                        result = new frmHisMestInveUser(currentModule,
                                                    HisMediStockPeriod);
                    }
                    else
                        result = new frmHisMestInveUser(hisLostExpMest);
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
