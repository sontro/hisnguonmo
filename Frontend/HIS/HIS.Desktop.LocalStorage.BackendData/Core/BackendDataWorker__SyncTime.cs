using HIS.Desktop.Library.CacheClient;
using HIS.Desktop.Library.CacheClient.Redis;
using HIS.Desktop.LocalStorage.ConfigApplication;
using Inventec.Common.Logging;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace HIS.Desktop.LocalStorage.BackendData
{
    public partial class BackendDataWorker
    {
        public static string SetSyncTime<T>(string timeSync)
        {
            string result = "";
            try
            {
                Type type = typeof(T);
                result = (!String.IsNullOrEmpty(timeSync) ? timeSync : DateTime.Now.ToString("yyyyMMddHHmmss"));
                if (dicTimeSync.ContainsKey(type))
                {
                    string outValue = null;
                    if (!dicTimeSync.TryRemove(type, out outValue))
                    {
                        LogSystem.Info("Khong Remove duoc cau hinh trong dictionary dicTimeSync Key: " + type.ToString());
                        dicTimeSync.TryUpdate(type, timeSync, null);
                    }
                    else
                    {
                        if (!dicTimeSync.TryAdd(type, timeSync))
                        {
                            LogSystem.Info("Khong Add duoc cau hinh vao dictionary dicTimeSync Key: " + type.ToString());
                        }
                    }
                }
                else
                {
                    if (!dicTimeSync.TryAdd(type, result))
                    {
                        LogSystem.Info("Khong Add duoc cau hinh vao dictionary dicTimeSync Key: " + type.ToString());
                    }
                }

                //Type type = typeof(T);
                //var ts = dicTimeSync.FirstOrDefault(o => o.DataType == type);
                //if (ts == null || ts.Data == null)
                //{
                //    if (!String.IsNullOrEmpty(timeSync))
                //        result = timeSync;
                //    else
                //        result = DateTime.Now.ToString("yyyyMMddHHmmss");
                //    dicTimeSync.Add(new TimeSyncADO(type, result));
                //}
            }
            catch (Exception ex)
            {
                result = "";
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return "";
        }

        public static string SetSyncTime(Type type, string timeSync)
        {
            string result = "";
            try
            {
                result = (!String.IsNullOrEmpty(timeSync) ? timeSync : DateTime.Now.ToString("yyyyMMddHHmmss"));
                if (dicTimeSync.ContainsKey(type))
                {
                    string outValue = null;
                    if (!dicTimeSync.TryRemove(type, out outValue))
                    {
                        LogSystem.Info("Khong Remove duoc cau hinh trong dictionary Key: " + type.ToString());
                        dicTimeSync.TryUpdate(type, timeSync, null);
                    }
                    else
                    {
                        if (!dicTimeSync.TryAdd(type, timeSync))
                        {
                            LogSystem.Info("Khong Add duoc cau hinh vao dictionary Key: " + type.ToString());
                        }
                    }
                }
                else
                {
                    if (!dicTimeSync.TryAdd(type, result))
                    {
                        LogSystem.Info("Khong Add duoc cau hinh vao dictionary Key: " + type.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                result = "";
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return result;
        }

        public static string GetSyncTime<T>() where T : class
        {
            string result = "";
            try
            {
                Type type = typeof(T);
                dicTimeSync.TryGetValue(type, out result);
            }
            catch (Exception ex)
            {
                result = "";
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return "";
        }

        public static string GetSyncTime(Type type)
        {
            string result = "";
            try
            {
                dicTimeSync.TryGetValue(type, out result);
                if (String.IsNullOrEmpty(result))
                {
                    Inventec.Common.Logging.LogSystem.Warn("type = " + type.ToString() + " GetSyncTime not found");
                }
            }
            catch (Exception ex)
            {
                result = "";
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return result;
        }

    }
}
