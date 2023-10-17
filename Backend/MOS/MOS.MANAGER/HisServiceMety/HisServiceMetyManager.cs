using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceMety
{
    public partial class HisServiceMetyManager : BusinessBase
    {
        public HisServiceMetyManager()
            : base()
        {

        }

        public HisServiceMetyManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_SERVICE_METY>> Get(HisServiceMetyFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_METY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_METY> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceMetyGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_SERVICE_METY>> GetView(HisServiceMetyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERVICE_METY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERVICE_METY> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceMetyGet(param).GetView(filter);
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
        public ApiResultObject<HIS_SERVICE_METY> Create(HIS_SERVICE_METY data)
        {
            ApiResultObject<HIS_SERVICE_METY> result = new ApiResultObject<HIS_SERVICE_METY>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_METY resultData = null;
                if (valid && new HisServiceMetyCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_SERVICE_METY>> CreateList(List<HIS_SERVICE_METY> data)
        {
            ApiResultObject<List<HIS_SERVICE_METY>> result = new ApiResultObject<List<HIS_SERVICE_METY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_METY> resultData = null;
                if (valid && new HisServiceMetyCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_SERVICE_METY> Update(HIS_SERVICE_METY data)
        {
            ApiResultObject<HIS_SERVICE_METY> result = new ApiResultObject<HIS_SERVICE_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_METY resultData = null;
                if (valid && new HisServiceMetyUpdate(param).Update(data))
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
        public ApiResultObject<List<HIS_SERVICE_METY>> UpdateList(List<HIS_SERVICE_METY> data)
        {
            ApiResultObject<List<HIS_SERVICE_METY>> result = new ApiResultObject<List<HIS_SERVICE_METY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_METY> resultData = null;
                if (valid && new HisServiceMetyUpdate(param).UpdateList(data))
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
        public ApiResultObject<HIS_SERVICE_METY> ChangeLock(HIS_SERVICE_METY data)
        {
            ApiResultObject<HIS_SERVICE_METY> result = new ApiResultObject<HIS_SERVICE_METY>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_METY resultData = null;
                if (valid && new HisServiceMetyLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_SERVICE_METY data)
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
                    resultData = new HisServiceMetyTruncate(param).Truncate(data);
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
                valid = valid && IsNotNullOrEmpty(ids);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisServiceMetyTruncate(param).TruncateList(ids);
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
        public ApiResultObject<List<HIS_SERVICE_METY>> CopyByService(HisServiceMetyCopyByServiceSDO data)
        {
            ApiResultObject<List<HIS_SERVICE_METY>> result = new ApiResultObject<List<HIS_SERVICE_METY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_METY> resultData = null;
                if (valid)
                {
                    new HisServiceMetyCopyByService(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_SERVICE_METY>> CopyByMety(HisServiceMetyCopyByMetySDO data)
        {
            ApiResultObject<List<HIS_SERVICE_METY>> result = new ApiResultObject<List<HIS_SERVICE_METY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_METY> resultData = null;
                if (valid)
                {
                    new HisServiceMetyCopyByMety(param).Run(data, ref resultData);
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
