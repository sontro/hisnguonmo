using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.HisConfig;
using HIS.Desktop.LocalStorage.LocalData;
using Inventec.Common.Adapter;
using Inventec.Common.Logging;
using Inventec.Core;
using EMR.EFMODEL.DataModels;
using EMR.Filter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.EmrConfig
{
    public class ConfigLoader
    {
        const string configUri = "/api/EmrConfig/Get";

        public static bool Refresh()
        {
            bool result = false;
            try
            {
                //if (!CheckUserInventecEmr())
                //{
                //    LogSystem.Info("Khong ket noi den he thong EMR noi bo");
                //    return true;
                //}
                CommonParam paramGet = new CommonParam();
                EmrConfigFilter configFilter = new EmrConfigFilter();
                configFilter.IS_ACTIVE = 1;
                var ro = new BackendAdapter(paramGet).Get<List<EMR_CONFIG>>(configUri, ApiConsumers.EmrConsumer, configFilter, paramGet);

                if (ro != null && ro.Count > 0)
                {
                    foreach (var config in ro)
                    {
                        if (!String.IsNullOrWhiteSpace(config.KEY))
                        {
                            if (EmrConfigs.dic.ContainsKey(config.KEY))
                            {
                                object outValue = null;
                                if (!EmrConfigs.dic.TryRemove(config.KEY, out outValue))
                                {
                                    LogSystem.Info("Khong Remove duoc cau hinh trong dictionary Key: " + config.KEY.ToString());
                                    if (!EmrConfigs.dic.TryUpdate(config.KEY, config, EmrConfigs.dic[config.KEY]))
                                        EmrConfigs.dic[config.KEY] = config;
                                }
                                else
                                {
                                    if (!EmrConfigs.dic.TryAdd(config.KEY, config))
                                    {
                                        LogSystem.Info("Khong Add duoc cau hinh vao dictionary Key: " + config.KEY.ToString());
                                    }
                                }
                            }
                            else
                            {
                                if (!EmrConfigs.dic.TryAdd(config.KEY, config))
                                {
                                    LogSystem.Info("Khong Add duoc cau hinh vao dictionary Key: " + config.KEY.ToString());
                                }
                            }
                        }
                        else
                        {
                            LogSystem.Warn("Key null." + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => config), config));
                        }
                    }
                    result = true;
                }
                else if (paramGet.HasException)
                {
                    LogSystem.Error("Query EmrConfig co exception.");
                }
                else
                {
                    LogSystem.Warn("Khong co du lieu EmrConfig & khong co exception.");
                    result = true;
                }
                if (result)
                {
                    LogSystem.Info("Load du lieu cau hinh EmrConfig thanh cong.");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private static bool CheckUserInventecEmr()
        {
            try
            {
                string EmrVersion = HisConfigs.Get<string>("MOS.EMR.INTEGRATION_VERSION");
                string EmrIntegrateOption = HisConfigs.Get<string>("MOS.EMR.INTEGRATE_OPTION");
                string EmrIntegrateType = HisConfigs.Get<string>("MOS.EMR.INTEGRATION_TYPE");

                if (EmrVersion == "1" && EmrIntegrateOption == "1")
                    return true;

                if (EmrVersion == "2" && EmrIntegrateType == "1")
                    return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return false;
        }
    }
}
