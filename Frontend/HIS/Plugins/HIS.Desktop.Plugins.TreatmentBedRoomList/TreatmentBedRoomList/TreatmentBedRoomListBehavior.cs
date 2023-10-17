using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentBedRoomList
{
    class TreatmentBedRoomListBehavior : Tool<DesktopToolContext>, ITreatmentBedRoomList
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        string _TreatmentCode = "";

        internal TreatmentBedRoomListBehavior()
            : base()
        {

        }

        public TreatmentBedRoomListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ITreatmentBedRoomList.Run()
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
                        else if (item is string)
                        {
                            _TreatmentCode = (string)item;
                        }
                    }
                    if (currentModule != null)
                    {
                        result = new frmTreatmentBedRoomList(currentModule, _TreatmentCode);
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
