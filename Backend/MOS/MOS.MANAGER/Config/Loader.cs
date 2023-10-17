using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisConfig;
using SDA.Filter;
using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    public class Loader : BusinessBase
    {
        private static Object thisLock = new Object();

        public static List<HIS_CONFIG> CONFIGs = new List<HIS_CONFIG>();

        public static bool RefreshConfig()
        {
            bool result = false;
            try
            {
                LogSystem.Info("Bat dau get HisConfig.");
                List<HIS_CONFIG> data = new HisConfigGet().Get(new HisConfigFilterQuery());
                LogSystem.Info("Ket thuc get HisConfig.");
                lock (thisLock)
                {
                    CONFIGs = data;
                    result = true;
                    LogSystem.Info("Load du lieu cau hinh HisConfig thanh cong.");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public static List<HIS_CONFIG> GetConfig(string code, long branchId)
        {
            List<HIS_CONFIG> result = null;
            try
            {
                result = CONFIGs
                    .Where(o => o.KEY == code && (!o.BRANCH_ID.HasValue || o.BRANCH_ID.Value == branchId))
                    .ToList();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        public static HIS_CONFIG GetConfig(string code)
        {
            HIS_CONFIG result = null;
            try
            {
                result = CONFIGs.Where(o => o.KEY == code).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }
    }
}
