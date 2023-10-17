using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceRetyCat
{
    public partial class HisServiceRetyCatManager : BusinessBase
    {
        public HisServiceRetyCatManager()
            : base()
        {

        }

        public HisServiceRetyCatManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_SERVICE_RETY_CAT>> Get(HisServiceRetyCatFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_RETY_CAT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_RETY_CAT> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceRetyCatGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_SERVICE_RETY_CAT>> GetView(HisServiceRetyCatViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERVICE_RETY_CAT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERVICE_RETY_CAT> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceRetyCatGet(param).GetView(filter);
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
        public ApiResultObject<HIS_SERVICE_RETY_CAT> Create(HIS_SERVICE_RETY_CAT data)
        {
            ApiResultObject<HIS_SERVICE_RETY_CAT> result = new ApiResultObject<HIS_SERVICE_RETY_CAT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_RETY_CAT resultData = null;
                if (valid && new HisServiceRetyCatCreate(param).Create(data))
                {
                    resultData = data;
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
        public ApiResultObject<List<HIS_SERVICE_RETY_CAT>> CreateList(List<HIS_SERVICE_RETY_CAT> data)
        {
            ApiResultObject<List<HIS_SERVICE_RETY_CAT>> result = new ApiResultObject<List<HIS_SERVICE_RETY_CAT>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_RETY_CAT> resultData = null;
                if (valid && new HisServiceRetyCatCreate(param).CreateList(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_SERVICE_RETY_CAT> Update(HIS_SERVICE_RETY_CAT data)
        {
            ApiResultObject<HIS_SERVICE_RETY_CAT> result = new ApiResultObject<HIS_SERVICE_RETY_CAT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_RETY_CAT resultData = null;
                if (valid && new HisServiceRetyCatUpdate(param).Update(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_SERVICE_RETY_CAT> ChangeLock(HIS_SERVICE_RETY_CAT data)
        {
            ApiResultObject<HIS_SERVICE_RETY_CAT> result = new ApiResultObject<HIS_SERVICE_RETY_CAT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_RETY_CAT resultData = null;
                if (valid && new HisServiceRetyCatLock(param).ChangeLock(data))
                {
                    resultData = data;
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
        public ApiResultObject<bool> Delete(HIS_SERVICE_RETY_CAT data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisServiceRetyCatTruncate(param).Truncate(data);
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
        public ApiResultObject<bool> DeleteList(List<long> ids)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisServiceRetyCatTruncate(param).TruncateList(ids);
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
        public ApiResultObject<List<HIS_SERVICE_RETY_CAT>> CopyByService(HisServiceRetyCatCopyByServiceSDO data)
        {
            ApiResultObject<List<HIS_SERVICE_RETY_CAT>> result = new ApiResultObject<List<HIS_SERVICE_RETY_CAT>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_RETY_CAT> resultData = null;
                if (valid)
                {
                    new HisServiceRetyCatCopyByService(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_SERVICE_RETY_CAT>> CopyByRetyCat(HisServiceRetyCatCopyByRetyCatSDO data)
        {
            ApiResultObject<List<HIS_SERVICE_RETY_CAT>> result = new ApiResultObject<List<HIS_SERVICE_RETY_CAT>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_RETY_CAT> resultData = null;
                if (valid)
                {
                    new HisServiceRetyCatCopyByRetyCat(param).Run(data, ref resultData);
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
    }
}
