using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TYT.Desktop.Plugins.NervesList;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace TYT.Desktop.Plugins.NervesList.TYTNervesList
{
    public sealed class TYTNervesListBehavior : Tool<IDesktopToolContext>, ITYTNervesList
    {
        Inventec.Desktop.Common.Modules.Module moduleData;

        public TYTNervesListBehavior()
            : base()
        {
        }

        public TYTNervesListBehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module moduleData)
            : base()
        {
            this.moduleData = moduleData;
        }

        object ITYTNervesList.Run()
        {
            try
            {
                return new UCTYTNervesList(this.moduleData);
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
