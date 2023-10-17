using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.VitaminA;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.VitaminA.VitaminA
{
    public sealed class VitaminABehavior : Tool<IDesktopToolContext>, IVitaminA
    {
        Inventec.Desktop.Common.Modules.Module moduleData;
        public VitaminABehavior()
            : base()
        {
        }

        public VitaminABehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module moduleData)
            : base()
        {
            this.moduleData = moduleData;
        }

        object IVitaminA.Run()
        {
            try
            {
                return new UCVitaminA(this.moduleData.RoomId, this.moduleData.RoomTypeId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                //param.HasException = true;
                return null;
            }
        }
    }
}
