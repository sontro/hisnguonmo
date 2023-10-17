
using HIS.Desktop.ApiConsumer;
using Inventec.Common.Logging;
using Inventec.Core;
using SDA.EFMODEL.DataModels;
using SDA.Filter;
using System;
using System.Collections.Generic;


namespace HIS.Desktop.Plugins.TransDepartment.Loader
{
    public class Loader 
    {
        public static Dictionary<string, SDA.EFMODEL.DataModels.SDA_CONFIG> dictionaryConfig = new Dictionary<string, SDA.EFMODEL.DataModels.SDA_CONFIG>();

        public static bool RefreshConfig()
        {
            bool result = false;
            try
            {
                CommonParam paramGet = new CommonParam();
                var filter = new SdaConfigFilter();
                //filter.
                var ro = new Inventec.Common.Adapter.BackendAdapter(new Inventec.Core.CommonParam()).GetRO<List<SDA_CONFIG>>(SdaRequestUriStore.SDA_CONFIG_GET, ApiConsumers.MosConsumer, filter, null);
                if (ro.Data != null && ro.Data.Count > 0)
                {
                    foreach (var config in ro.Data)
                    {
                        if (!String.IsNullOrWhiteSpace(config.KEY))
                        {
                            dictionaryConfig[config.KEY] = config; //Ghi de du lieu cu ==> luu y tinh huong neu 2 config trung key thi config sau se de len config truoc. Loi nay thuoc ve constraint du lieu ko thuoc trach nhiem cua Loader.
                        }
                        else
                        {
                            LogSystem.Error("Key null." + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => config), config));
                        }
                    }
                    result = true;
                }
                else if (paramGet.HasException)
                {
                    LogSystem.Error("Query SdaConfig co exception.");
                }
                else
                {
                    LogSystem.Warn("Khong co du lieu SdaConfig & khong co exception.");
                    result = true;
                }
                if (result)
                {
                    LogSystem.Info("Load du lieu cau hinh SdaConfig thanh cong.");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
