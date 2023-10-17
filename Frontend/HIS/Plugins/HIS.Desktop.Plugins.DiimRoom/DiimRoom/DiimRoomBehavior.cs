using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.DiimRoom.DiimRoom
{
    class DiimRoomBehavior : BusinessBase, IDiimRoom
    {
        object[] entity;
        internal DiimRoomBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IDiimRoom.Run()
        {
            try
            {
				Inventec.Desktop.Common.Modules.Module module = null;
                V_HIS_SERE_SERV_6 sereServ6 = null;
				RefeshReference refeshReference = null;
				foreach (var item in entity)
                {
                    if (item is Inventec.Desktop.Common.Modules.Module)
                    {
                        module = (Inventec.Desktop.Common.Modules.Module)item;
                    }
					else if (item is RefeshReference)
                    {
                        refeshReference = (RefeshReference)item;
                    }
                    else if (item is V_HIS_SERE_SERV_6)
                    {
                        sereServ6 = (V_HIS_SERE_SERV_6)item;
                    }
                }
                return new frmPacsView(module, sereServ6, refeshReference);
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
