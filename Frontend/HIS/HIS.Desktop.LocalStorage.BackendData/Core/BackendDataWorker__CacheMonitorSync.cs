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
        public static void CacheMonitorSyncExecute<T>() where T : class
        {
            try
            {
                Type type = typeof(T);
                CacheMonitorSync.Create(type.ToString(), false);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public static void CacheMonitorSyncExecute<T>(bool isReload) where T : class
        {
            try
            {
                Type type = typeof(T);
                CacheMonitorSync.Create(type.ToString(), isReload);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public static void CacheMonitorSyncExecute(Type type)
        {
            try
            {
                CacheMonitorSync.Create(type.ToString(), false);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public static void CacheMonitorSyncExecute(Type type, bool isReload)
        {
            try
            {
                CacheMonitorSync.Create(type.ToString(), isReload);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public static void CacheMonitorSyncExecute(string dataKey, bool isReload)
        {
            try
            {
                CacheMonitorSync.Create(dataKey, isReload);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
