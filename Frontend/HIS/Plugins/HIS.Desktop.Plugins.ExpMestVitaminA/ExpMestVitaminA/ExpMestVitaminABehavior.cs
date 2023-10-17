using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestVitaminA.ExpMestVitaminA
{
    class ExpMestVitaminABehavior : Tool<IDesktopToolContext>, IExpMestVitaminA
    {
        List<V_HIS_VITAMIN_A> vitaminA;
        Inventec.Desktop.Common.Modules.Module Module;
        HIS.Desktop.Common.DelegateSelectData dlg;

        internal ExpMestVitaminABehavior()
            : base()
        {

        }

        internal ExpMestVitaminABehavior(Inventec.Desktop.Common.Modules.Module moduleData, CommonParam param, List<V_HIS_VITAMIN_A> data, HIS.Desktop.Common.DelegateSelectData dlg)
            : base()
        {
            Module = moduleData;
            vitaminA = data;
            this.dlg = dlg;
        }

        object IExpMestVitaminA.Run()
        {
            object result = null;
            try
            {
                result = new frmExpMestVitaminA(Module, vitaminA, this.dlg);
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
