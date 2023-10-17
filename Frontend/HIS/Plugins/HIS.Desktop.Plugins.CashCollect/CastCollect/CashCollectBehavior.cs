using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.CashCollect.CashCollect
{
    class CashCollectBehavior : Tool<IDesktopToolContext>, ICashCollect
    {
        object[] entity;
        long treatmentId;
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal CashCollectBehavior()
            : base()
        {

        }

        internal CashCollectBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object ICashCollect.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    //foreach (var item in entity)
                    //{
                    //    if (item is long)
                    //    {
                    //        treatmentId = (long)item;
                    //    }
                    //    else if (item is Inventec.Desktop.Common.Modules.Module)
                    //    {
                    //        currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                    //    }
                    //    if (currentModule != null && treatmentId > 0)
                    //    {
                    //        result = new UCCashCollect(currentModule, treatmentId);
                    //        break;
                    //    }
                    //}

                    foreach (var item in entity)
                    {
                       if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                    }

                    result = new UCCashCollect(currentModule);
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
