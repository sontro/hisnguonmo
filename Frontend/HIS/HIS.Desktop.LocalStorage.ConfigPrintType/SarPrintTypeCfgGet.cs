using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Common.Adapter;
using Inventec.Core;
using SAR.Filter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.ConfigPrintType
{
    class SarPrintTypeCfgGet
    {
        internal static List<SAR.EFMODEL.DataModels.SAR_PRINT_TYPE_CFG> Get()
        {
            try
            {
                CommonParam param = new CommonParam();
                SarPrintTypeCfgFilter filter = new SarPrintTypeCfgFilter();
                filter.IS_ACTIVE = 1;
                filter.APP_CODE__EXACT = ConfigurationManager.AppSettings["Inventec.Desktop.ApplicationCode"];
                //filter.BRANCH_CODE_OR_EMPTY = BranchDataWorker.Branch.BRANCH_CODE;
                return new BackendAdapter(param).Get<List<SAR.EFMODEL.DataModels.SAR_PRINT_TYPE_CFG>>("api/SarPrintTypeCfg/Get", ApiConsumers.SarConsumer, filter, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return null;
        }
    }
}
