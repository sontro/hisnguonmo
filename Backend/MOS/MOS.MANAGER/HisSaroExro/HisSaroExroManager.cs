using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSaroExro
{
    public partial class HisSaroExroManager : BusinessBase
    {
        public HisSaroExroManager()
            : base()
        {

        }

        public HisSaroExroManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_SARO_EXRO>> Get(HisSaroExroFilterQuery filter)
        {
            ApiResultObject<List<HIS_SARO_EXRO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SARO_EXRO> resultData = null;
                if (valid)
                {
                    resultData = new HisSaroExroGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_SARO_EXRO>> GetView(HisSaroExroViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SARO_EXRO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SARO_EXRO> resultData = null;
                if (valid)
                {
                    resultData = new HisSaroExroGet(param).GetView(filter);
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
        public ApiResultObject<HIS_SARO_EXRO> Create(HIS_SARO_EXRO data)
        {
            ApiResultObject<HIS_SARO_EXRO> result = new ApiResultObject<HIS_SARO_EXRO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SARO_EXRO resultData = null;
                if (valid && new HisSaroExroCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_SARO_EXRO>> CreateList(List<HIS_SARO_EXRO> data)
        {
            ApiResultObject<List<HIS_SARO_EXRO>> result = new ApiResultObject<List<HIS_SARO_EXRO>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SARO_EXRO> resultData = null;
                if (valid && new HisSaroExroCreate(param).CreateList(data))
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
                    resultData = new HisSaroExroTruncate(param).TruncateList(ids);
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
        public ApiResultObject<HIS_SARO_EXRO> Update(HIS_SARO_EXRO data)
        {
            ApiResultObject<HIS_SARO_EXRO> result = new ApiResultObject<HIS_SARO_EXRO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SARO_EXRO resultData = null;
                if (valid && new HisSaroExroUpdate(param).Update(data))
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
        public ApiResultObject<HIS_SARO_EXRO> ChangeLock(HIS_SARO_EXRO data)
        {
            ApiResultObject<HIS_SARO_EXRO> result = new ApiResultObject<HIS_SARO_EXRO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SARO_EXRO resultData = null;
                if (valid && new HisSaroExroLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_SARO_EXRO data)
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
                    resultData = new HisSaroExroTruncate(param).Truncate(data);
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
        public ApiResultObject<List<HIS_SARO_EXRO>> CopyBySampleRoom(HisSaroExroCopyBySampleRoomSDO data)
        {
            ApiResultObject<List<HIS_SARO_EXRO>> result = new ApiResultObject<List<HIS_SARO_EXRO>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SARO_EXRO> resultData = null;
                if (valid)
                {
                    new HisSaroExroCopyBySampleRoom(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_SARO_EXRO>> CopyByExecuteRoom(HisSaroExroCopyByExecuteRoomSDO data)
        {
            ApiResultObject<List<HIS_SARO_EXRO>> result = new ApiResultObject<List<HIS_SARO_EXRO>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SARO_EXRO> resultData = null;
                if (valid)
                {
                    new HisSaroExroCopyByExecuteRoom(param).Run(data, ref resultData);
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
