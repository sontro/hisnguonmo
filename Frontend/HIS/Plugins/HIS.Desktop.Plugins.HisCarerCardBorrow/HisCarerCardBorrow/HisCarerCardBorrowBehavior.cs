using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisCarerCardBorrow.HisCarerCardBorrow
{
    class HisCarerCardBorrowBehavior : Tool<IDesktopToolContext>, IHisCarerCardBorrow
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module moduleData = null;     
        internal HisCarerCardBorrowBehavior()
            : base()
        {

        }

        internal HisCarerCardBorrowBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IHisCarerCardBorrow.Run()
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;                
                      
                    }
                }

                if (moduleData != null)
                {
                    return new HIS.Desktop.Plugins.HisCarerCardBorrow.Run.UCCarerCardBorrow(moduleData);
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
