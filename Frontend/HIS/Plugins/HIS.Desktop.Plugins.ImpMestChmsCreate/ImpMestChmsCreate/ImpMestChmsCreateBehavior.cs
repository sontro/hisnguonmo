using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestChmsCreate.ImpMestChmsCreate
{
    class ImpMestChmsCreateBehavior : Tool<IDesktopToolContext>, IImpMestChmsCreate
    {
       long expMestId;
       long expMestTypeId;
        Inventec.Desktop.Common.Modules.Module Module;
        HIS.Desktop.Common.DelegateRefreshData refreshData;
        internal ImpMestChmsCreateBehavior()
            : base()
        {

        }

        internal ImpMestChmsCreateBehavior(Inventec.Desktop.Common.Modules.Module moduleData, CommonParam param, long data, long expMestTypeId, HIS.Desktop.Common.DelegateRefreshData refresh)
            : base()
        {
            this.Module = moduleData;
            this.expMestId = data;
            this.expMestTypeId = expMestTypeId;
            this.refreshData = refresh;
        }

        object IImpMestChmsCreate.Run()
        {
            object result = null;
            try
            {
                result = new frmImpMestChmsCreate(this.Module, this.expMestId, this.expMestTypeId, this.refreshData);
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
