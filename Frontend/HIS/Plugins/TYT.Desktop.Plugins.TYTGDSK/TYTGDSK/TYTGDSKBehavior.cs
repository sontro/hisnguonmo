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

namespace TYT.Desktop.Plugins.TYTGDSK.TYTGDSK
{
    class TYTGDSKBehavior : Tool<IDesktopToolContext>, ITYTGDSK
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal TYTGDSKBehavior()
            : base()
        {

        }

        internal TYTGDSKBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object ITYTGDSK.Run()
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

                    if (currentModule != null)
                    {
                        result = new frm(currentModule);
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
