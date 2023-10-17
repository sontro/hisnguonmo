using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.HisSuggestedPaymentList;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;
using HIS.Desktop.Plugins.HisSuggestedPaymentList.Run;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.HisSuggestedPaymentList.Run
{
    public sealed class HisSuggestedPaymentListBehavior : Tool<IDesktopToolContext>, IHisSuggestedPaymentList
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        long treatmentId = 0;
        HIS_DHST currentDhst;
        public HisSuggestedPaymentListBehavior()
            : base()
        {
        }

        public HisSuggestedPaymentListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisSuggestedPaymentList.Run()
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
                    }
                    if (currentModule != null )
                    {
                        result = new frmHisSuggestedPaymentList(currentModule);
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
