using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ImpMestPay;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.Plugins.ImpMestPay.FromDesign;

namespace Inventec.Desktop.Plugins.ImpMestPay.ImpMestPay
{
    public sealed class ImpMestPayBehavior : Tool<IDesktopToolContext>, IImpMestPay
    {
        long _ImpMestProposeId;
        Inventec.Desktop.Common.Modules.Module Module;
        public ImpMestPayBehavior()
            : base()
        {
        }

        public ImpMestPayBehavior(CommonParam param, long _impMestProposeId, Inventec.Desktop.Common.Modules.Module module)
            : base()
        {
            this._ImpMestProposeId = _impMestProposeId;
            this.Module = module;
        }

        object IImpMestPay.Run()
        {
            try
            {
                return new frmImpMestPayPlus(this.Module, _ImpMestProposeId);
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
