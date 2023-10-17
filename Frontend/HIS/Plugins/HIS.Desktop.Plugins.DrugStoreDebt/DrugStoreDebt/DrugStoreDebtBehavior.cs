using HIS.Desktop.ADO;
using HIS.Desktop.Common;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DrugStoreDebt.DrugStoreDebt
{
    class DrugStoreDebtBehavior : Tool<IDesktopToolContext>, IDrugStoreDebt
    {
        private List<long> expMestIds = null;
        private Inventec.Desktop.Common.Modules.Module Module;
        private string ExpMestCode;
        private DelegateSelectData _RefreshData;

        internal DrugStoreDebtBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, List<long> data, DelegateSelectData refreshData)
            : base()
        {
            this.Module = module;
            this.expMestIds = data;
            this._RefreshData = refreshData;
        }
        internal DrugStoreDebtBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, string expCode, DelegateSelectData refreshData)
            : base()
        {
            this.Module = module;
            this.ExpMestCode = expCode;
            this._RefreshData = refreshData;
        }
        internal DrugStoreDebtBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param)
            : base()
        {
            this.Module = module;
        }

        object IDrugStoreDebt.Run()
        {
            object result = null;
            try
            {
                if (expMestIds != null && expMestIds.Count > 0)
                {
                    result = new frmDrugStoreDebt(Module, expMestIds, this._RefreshData);
                }
                else if (!String.IsNullOrWhiteSpace(ExpMestCode))
                {
                    result = new frmDrugStoreDebt(Module, ExpMestCode, this._RefreshData);
                }
                else
                {
                    result = new frmDrugStoreDebt(Module);
                }
                if (result == null) throw new NullReferenceException(LogUtil.TraceData("ExpMestIds", expMestIds)
                    + "\n" + LogUtil.TraceData("ExpMestCode", ExpMestCode));
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
