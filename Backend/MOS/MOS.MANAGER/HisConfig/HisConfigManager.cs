using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.MANAGER.Config;

namespace MOS.MANAGER.HisConfig
{
    public partial class HisConfigManager : BusinessBase
    {
        public HisConfigManager()
            : base()
        {

        }

        public HisConfigManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<bool> ResetAll()
        {
            ApiResultObject<bool> result = null;
            try
            {
                var rsData = new ConfigReset(param).ResetConfig();
                if (rsData)
                {
                    LogSystem.Info("Tai lai cau hinh MOS thanh cong");
                }
                result = this.PackSuccess(rsData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_CONFIG>> Get(HisConfigFilterQuery filter)
        {
            ApiResultObject<List<HIS_CONFIG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CONFIG> resultData = null;
                if (valid)
                {
                    resultData = new HisConfigGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_CONFIG> Create(HIS_CONFIG data)
        {
            ApiResultObject<HIS_CONFIG> result = new ApiResultObject<HIS_CONFIG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CONFIG resultData = null;
                if (valid && new HisConfigCreate(param).Create(data))
                {
                    resultData = data;
                    new ConfigReset(param).ResetConfig();
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_CONFIG> Update(HIS_CONFIG data)
        {
            ApiResultObject<HIS_CONFIG> result = new ApiResultObject<HIS_CONFIG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CONFIG resultData = null;
                if (valid && new HisConfigUpdate(param).Update(data))
                {
                    resultData = data;
                    new ConfigReset(param).ResetConfig();
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_CONFIG> ChangeLock(long id)
        {
            ApiResultObject<HIS_CONFIG> result = new ApiResultObject<HIS_CONFIG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CONFIG resultData = null;
                if (valid)
                {
                    if (new HisConfigLock(param).ChangeLock(id, ref resultData))
                        Config.Loader.RefreshConfig();
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_CONFIG> Lock(long id)
        {
            ApiResultObject<HIS_CONFIG> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CONFIG resultData = null;
                if (valid)
                {
                    if (new HisConfigLock(param).Lock(id, ref resultData))
                        Config.Loader.RefreshConfig();
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_CONFIG> Unlock(long id)
        {
            ApiResultObject<HIS_CONFIG> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CONFIG resultData = null;
                if (valid)
                {
                    if (new HisConfigLock(param).Unlock(id, ref resultData))
                        Config.Loader.RefreshConfig();
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Delete(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisConfigTruncate(param).Truncate(id);
                    if (resultData) Config.Loader.RefreshConfig();
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_CONFIG>> CreateList(List<HIS_CONFIG> listData)
        {
            ApiResultObject<List<HIS_CONFIG>> result = new ApiResultObject<List<HIS_CONFIG>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_CONFIG> resultData = null;
                if (valid && new HisConfigCreate(param).CreateList(listData))
                {
                    resultData = listData;
                    new ConfigReset(param).ResetConfig();
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_CONFIG>> UpdateList(List<HIS_CONFIG> listData)
        {
            ApiResultObject<List<HIS_CONFIG>> result = new ApiResultObject<List<HIS_CONFIG>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_CONFIG> resultData = null;
                if (valid && new HisConfigUpdate(param).UpdateList(listData))
                {
                    resultData = listData;
                    new ConfigReset(param).ResetConfig();
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }


    }
}
