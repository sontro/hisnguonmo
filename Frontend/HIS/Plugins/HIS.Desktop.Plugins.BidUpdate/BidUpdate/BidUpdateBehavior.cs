using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.BidUpdate;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.BidUpdate.BidUpdate
{
    class BidUpdateBehavior : BusinessBase, IBidUpdate
    {
        object[] entity;
        internal BidUpdateBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IBidUpdate.Run()
        {
            try
            {
                RefeshReference refreshData = null;
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                long bid_id = 0;
                if (entity.GetType() == typeof(object[]))
                {
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
                                bid_id = (long)entity[i];
                            }
                            if (entity[i] is RefeshReference)
                            {
                                refreshData = (RefeshReference)entity[i];
                            }
                        }
                    }
                }

                return new frmBidUpdate(moduleData, bid_id, refreshData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
