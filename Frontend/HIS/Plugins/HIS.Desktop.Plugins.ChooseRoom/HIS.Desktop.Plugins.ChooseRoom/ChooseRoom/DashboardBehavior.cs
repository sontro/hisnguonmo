using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ChooseRoom.ChooseRoom
{
    class ChooseRoomBehavior : BusinessBase, IChooseRoom
    {
        object[] entity;
        internal ChooseRoomBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IChooseRoom.Run()
        {
            try
            {
				Inventec.Desktop.Common.Modules.Module module = null;
				RefeshReference refeshReference = null;
				foreach (var item in entity)
                {
                    if (item is Inventec.Desktop.Common.Modules.Module)
                    {
                        module = (Inventec.Desktop.Common.Modules.Module)item;
                    }
					if (item is RefeshReference)
                    {
                        refeshReference = (RefeshReference)item;
                    }
                }
                return new frmChooseRoom(module, refeshReference);
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
