using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.PackingMaterial;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.PackingMaterial.PackingMaterial
{
    public sealed class PackingMaterialBehavior : Tool<IDesktopToolContext>, IPackingMaterial
    {
        Inventec.Desktop.Common.Modules.Module moduleData;

        public PackingMaterialBehavior()
            : base()
        {
        }

        public PackingMaterialBehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module moduleData)
            : base()
        {
            this.moduleData = moduleData;
        }

        object IPackingMaterial.Run()
        {
            try
            {
                return new frmPackingMaterial(this.moduleData);
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
