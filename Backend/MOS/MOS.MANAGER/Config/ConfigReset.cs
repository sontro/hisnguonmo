using Inventec.Common.Logging;
using Inventec.Common.WebApiClient;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class ResetThreadData
    {
        public string TokenCode { get; set; }
        public string MosAddress { get; set; }
    }

    class ConfigReset : BusinessBase
    {
        internal ConfigReset()
            : base()
        {

        }

        internal ConfigReset(CommonParam param)
            : base(param)
        {

        }

        internal bool ResetConfig()
        {
            bool result = true;
            try
            {
                //Neu la MOS la "master" thi goi den MOS "slave"
                if (!SystemCFG.IS_SLAVE && SystemCFG.SLAVE_ADDRESSES != null && SystemCFG.SLAVE_ADDRESSES.Count > 0)
                {
                    string tokenCode = ResourceTokenManager.GetTokenCode();
                    InitThread(tokenCode);
                }

                result = result && Loader.RefreshConfig();
                if (result)
                {
                    List<Type> cfgs = GetAllCFG();
                    if (cfgs != null && cfgs.Count > 0)
                    {
                        foreach (Type cfg in cfgs)
                        {
                            try
                            {
                                MethodInfo methodInfo = cfg.GetMethod("Reload");
                                if (methodInfo != null)
                                {
                                    methodInfo.Invoke(null, null);
                                }
                            }
                            catch (Exception ex)
                            {
                                LogSystem.Error(ex);
                            }
                        }
                    }
                    LogSystem.Info("Reset config MOS done........");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        private List<Type> GetAllCFG()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && !t.IsInterface
                    && !t.IsGenericTypeDefinition
                    && !t.IsAbstract
                    && t.Namespace == "MOS.MANAGER.Config"
                    && t.FullName.EndsWith("CFG"))
                .ToList();

        }

        private static void InitThread(string tokenCode)
        {
            try
            {
                if (!SystemCFG.IS_SLAVE && SystemCFG.SLAVE_ADDRESSES != null && SystemCFG.SLAVE_ADDRESSES.Count > 0)
                {
                    foreach (string address in SystemCFG.SLAVE_ADDRESSES)
                    {
                        if (!string.IsNullOrWhiteSpace(address))
                        {
                            InitThread(address, tokenCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static void InitThread(string address, string tokenCode)
        {
            try
            {
                ResetThreadData d = new ResetThreadData();
                d.MosAddress = address;
                d.TokenCode = tokenCode;

                Thread thread = new Thread(new ParameterizedThreadStart(Reset));
                thread.Priority = ThreadPriority.AboveNormal;
                thread.Start(d);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static void Reset(object threadData)
        {
            try
            {
                ResetThreadData data = (ResetThreadData)threadData;

                ApiConsumer mosConsumer = new ApiConsumer(data.MosAddress, data.TokenCode, MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE);
                ApiResultObject<bool> result = mosConsumer.Post<ApiResultObject<bool>>("api/HisConfig/ResetAll", new CommonParam(), null);
                if (result == null || !result.Success)
                {
                    LogSystem.Warn("Reset MOS-slave config: " + data.MosAddress + "HisConfig/ResetAll that bai");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
