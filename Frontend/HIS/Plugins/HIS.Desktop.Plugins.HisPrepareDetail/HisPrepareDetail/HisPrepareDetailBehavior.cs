using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using TYT.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.HisPrepareDetail.HisPrepareDetail
{
    class HisPrepareDetailBehavior : Tool<IDesktopToolContext>, IHisPrepareDetail
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal HisPrepareDetailBehavior()
            : base()
        {

        }

        internal HisPrepareDetailBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IHisPrepareDetail.Run()
        {
            object result = null;
            try
            {
                long prepareId = 0;
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is long)
                        {
                            prepareId = (long)item;
                        }
                        else if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                    }

                    if (currentModule != null && prepareId > 0)
                    {
                        result = new frmHisPrepareDetail(currentModule, prepareId);
                    }
                    else
                    {
                        throw new ArgumentNullException("PREPARE_ID: " + prepareId + " CurrentModule " + currentModule);
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
