using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.HisBedRoomList;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using HIS.Desktop.Plugins.HisBedRoomList;

namespace HIS.Desktop.Plugins.HisBedRoomList.HisBedRoomList
{
    public sealed class HisBedRoomListBehavior : Tool<IDesktopToolContext>, IHisBedRoomList
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        public HisBedRoomListBehavior()
            : base()
        {
        }

        public HisBedRoomListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisBedRoomList.Run()
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
                        result = new frmHisBedRoomList(currentModule);

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
