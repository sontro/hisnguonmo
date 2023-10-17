using Inventec.Core;
using HIS.Desktop.Common;
using HIS.Desktop.Plugins.HTU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;
using MOS.SDO;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.HTU.HTU
{
    public sealed class HTUBehavior : Tool<IDesktopToolContext>, IHTU
    {
        HIS_HTU hishtu;
        object[] entity;
        public HTUBehavior()
            : base()
        {

        }

        public HTUBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHTU.Run()
        {
            try
            {
                return new frmHTU(hishtu);
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
