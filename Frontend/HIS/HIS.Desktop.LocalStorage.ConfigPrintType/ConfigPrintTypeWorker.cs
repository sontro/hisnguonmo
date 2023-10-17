using HIS.Desktop.ApiConsumer;
using Inventec.Common.Adapter;
using Inventec.Common.Logging;
using Inventec.Common.XmlConfig;
using Inventec.Core;
using Inventec.Desktop.Common.LanguageManager;
using SAR.Filter;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.LocalStorage.BackendData;

namespace HIS.Desktop.LocalStorage.ConfigPrintType
{
    public class ConfigPrintTypeWorker
    {
        static readonly string APPLICATION_CODE = ConfigurationManager.AppSettings["Inventec.Desktop.ApplicationCode"];//Ma ung dung khai bao tren ACS
        static List<SAR_PRINT_TYPE_CFG> ConfigPrintTypes;
        static Object thisLock = new Object();

        public static void Init()
        {
            try
            {
                CommonParam param = new CommonParam();
                ConfigPrintTypes = SarPrintTypeCfgGet.Get();
                if (ConfigPrintTypes == null || ConfigPrintTypes.Count == 0) throw new ArgumentNullException("ConfigPrintTypes");

                ConfigPrintTypes = ConfigPrintTypes.Where(o => o.BRANCH_CODE == BranchDataWorker.Branch.BRANCH_CODE || String.IsNullOrEmpty(o.BRANCH_CODE)).ToList();

                Inventec.Common.Logging.LogSystem.Debug("ConfigPrintTypeWorker.Init. ConfigPrintTypes.count =" + (ConfigPrintTypes != null && ConfigPrintTypes.Count > 0 ? ConfigPrintTypes.Count : 0));
            }
            catch (ArgumentNullException ex)
            {
                ConfigPrintTypes = null;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public static List<SAR_PRINT_TYPE_CFG> GetByModule(string moduleLink)
        {
            List<SAR_PRINT_TYPE_CFG> data = default(List<SAR_PRINT_TYPE_CFG>);
            try
            {
                if (ConfigPrintTypes != null && ConfigPrintTypes.Count > 0)
                {
                    var cfgPrtype = ConfigPrintTypes.AsQueryable();
                    cfgPrtype = cfgPrtype.Where(o => o.MODULE_LINK == moduleLink);
                    return data = cfgPrtype.ToList();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return default(List<SAR_PRINT_TYPE_CFG>);
        }

        public static bool ReloadAll()
        {
            bool result = false;
            try
            {
                ConfigPrintTypes.Clear();
                Init();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return result;
        }
    }
}
