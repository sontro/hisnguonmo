using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisServiceMachine
{
    public partial class HisServiceMachineManager : BusinessBase
    {
        public HisServiceMachineManager()
            : base()
        {

        }

        public HisServiceMachineManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_SERVICE_MACHINE>> Get(HisServiceMachineFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_MACHINE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_MACHINE> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceMachineGet(param).Get(filter);
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
        public ApiResultObject<HIS_SERVICE_MACHINE> Create(HIS_SERVICE_MACHINE data)
        {
            ApiResultObject<HIS_SERVICE_MACHINE> result = new ApiResultObject<HIS_SERVICE_MACHINE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_MACHINE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceMachineCreate(param).Create(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_SERVICE_MACHINE>> CreateList(List<HIS_SERVICE_MACHINE> data)
        {
            ApiResultObject<List<HIS_SERVICE_MACHINE>> result = new ApiResultObject<List<HIS_SERVICE_MACHINE>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_MACHINE> resultData = null;
                if (valid && new HisServiceMachineCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_SERVICE_MACHINE> Update(HIS_SERVICE_MACHINE data)
        {
            ApiResultObject<HIS_SERVICE_MACHINE> result = new ApiResultObject<HIS_SERVICE_MACHINE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_MACHINE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceMachineUpdate(param).Update(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_SERVICE_MACHINE> ChangeLock(long id)
        {
            ApiResultObject<HIS_SERVICE_MACHINE> result = new ApiResultObject<HIS_SERVICE_MACHINE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_MACHINE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceMachineLock(param).ChangeLock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_SERVICE_MACHINE> Lock(long id)
        {
            ApiResultObject<HIS_SERVICE_MACHINE> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_MACHINE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceMachineLock(param).Lock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_SERVICE_MACHINE> Unlock(long id)
        {
            ApiResultObject<HIS_SERVICE_MACHINE> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_MACHINE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceMachineLock(param).Unlock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
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
                    resultData = new HisServiceMachineTruncate(param).Truncate(id);
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
                    resultData = new HisServiceMachineTruncate(param).TruncateList(ids);
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
        public ApiResultObject<List<HIS_SERVICE_MACHINE>> CopyByService(HisServiceMachineCopyByServiceSDO data)
        {
            ApiResultObject<List<HIS_SERVICE_MACHINE>> result = new ApiResultObject<List<HIS_SERVICE_MACHINE>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_MACHINE> resultData = null;
                if (valid)
                {
                    new HisServiceMachineCopyByService(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_SERVICE_MACHINE>> CopyByMachine(HisServiceMachineCopyByMachineSDO data)
        {
            ApiResultObject<List<HIS_SERVICE_MACHINE>> result = new ApiResultObject<List<HIS_SERVICE_MACHINE>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_MACHINE> resultData = null;
                if (valid)
                {
                    new HisServiceMachineCopyByMachine(param).Run(data, ref resultData);
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
