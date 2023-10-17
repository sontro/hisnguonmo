using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceMaty
{
    public partial class HisServiceMatyManager : BusinessBase
    {
        public HisServiceMatyManager()
            : base()
        {

        }

        public HisServiceMatyManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_SERVICE_MATY>> Get(HisServiceMatyFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceMatyGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_SERVICE_MATY>> GetView(HisServiceMatyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERVICE_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERVICE_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceMatyGet(param).GetView(filter);
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
        public ApiResultObject<HIS_SERVICE_MATY> Create(HIS_SERVICE_MATY data)
        {
            ApiResultObject<HIS_SERVICE_MATY> result = new ApiResultObject<HIS_SERVICE_MATY>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_MATY resultData = null;
                if (valid && new HisServiceMatyCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_SERVICE_MATY>> CreateList(List<HIS_SERVICE_MATY> data)
        {
            ApiResultObject<List<HIS_SERVICE_MATY>> result = new ApiResultObject<List<HIS_SERVICE_MATY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_MATY> resultData = null;
                if (valid && new HisServiceMatyCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_SERVICE_MATY> Update(HIS_SERVICE_MATY data)
        {
            ApiResultObject<HIS_SERVICE_MATY> result = new ApiResultObject<HIS_SERVICE_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_MATY resultData = null;
                if (valid && new HisServiceMatyUpdate(param).Update(data))
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
        public ApiResultObject<List<HIS_SERVICE_MATY>> UpdateList(List<HIS_SERVICE_MATY> data)
        {
            ApiResultObject<List<HIS_SERVICE_MATY>> result = new ApiResultObject<List<HIS_SERVICE_MATY>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_MATY> resultData = null;
                if (valid && new HisServiceMatyUpdate(param).UpdateList(data))
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
        public ApiResultObject<HIS_SERVICE_MATY> ChangeLock(HIS_SERVICE_MATY data)
        {
            ApiResultObject<HIS_SERVICE_MATY> result = new ApiResultObject<HIS_SERVICE_MATY>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_MATY resultData = null;
                if (valid && new HisServiceMatyLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_SERVICE_MATY data)
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
                    resultData = new HisServiceMatyTruncate(param).Truncate(data);
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
                    resultData = new HisServiceMatyTruncate(param).TruncateList(ids);
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
        public ApiResultObject<List<HIS_SERVICE_MATY>> CopyByService(HisServiceMatyCopyByServiceSDO data)
        {
            ApiResultObject<List<HIS_SERVICE_MATY>> result = new ApiResultObject<List<HIS_SERVICE_MATY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_MATY> resultData = null;
                if (valid)
                {
                    new HisServiceMatyCopyByService(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_SERVICE_MATY>> CopyByMaty(HisServiceMatyCopyByMatySDO data)
        {
            ApiResultObject<List<HIS_SERVICE_MATY>> result = new ApiResultObject<List<HIS_SERVICE_MATY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_MATY> resultData = null;
                if (valid)
                {
                    new HisServiceMatyCopyByMaty(param).Run(data, ref resultData);
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
