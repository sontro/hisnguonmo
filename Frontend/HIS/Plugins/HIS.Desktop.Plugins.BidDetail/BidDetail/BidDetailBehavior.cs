using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.BidDetail.BidDetail
{
    class BidDetailBehavior : Tool<IDesktopToolContext>, IBidDetail
    {
        object[] entity;

        internal BidDetailBehavior()
            : base()
        {

        }

        internal BidDetailBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IBidDetail.Run()
        {
            MOS.EFMODEL.DataModels.HIS_BID result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is MOS.EFMODEL.DataModels.HIS_BID) 
                            result = (MOS.EFMODEL.DataModels.HIS_BID)item;
                        if (item is Inventec.Desktop.Common.Modules.Module) 
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                    }
                }
                if (moduleData != null)
                {
                    return new FormBidDetail(result, moduleData);
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
