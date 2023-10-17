using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisCaroDepartment
{
    public partial class HisCaroDepartmentManager : BusinessBase
    {
        public HisCaroDepartmentManager()
            : base()
        {

        }

        public HisCaroDepartmentManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_CARO_DEPARTMENT>> Get(HisCaroDepartmentFilterQuery filter)
        {
            ApiResultObject<List<HIS_CARO_DEPARTMENT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CARO_DEPARTMENT> resultData = null;
                if (valid)
                {
                    resultData = new HisCaroDepartmentGet(param).Get(filter);
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
        public ApiResultObject<HIS_CARO_DEPARTMENT> Create(HIS_CARO_DEPARTMENT data)
        {
            ApiResultObject<HIS_CARO_DEPARTMENT> result = new ApiResultObject<HIS_CARO_DEPARTMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARO_DEPARTMENT resultData = null;
                if (valid && new HisCaroDepartmentCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_CARO_DEPARTMENT>> CreateList(List<HIS_CARO_DEPARTMENT> listData)
        {
            ApiResultObject<List<HIS_CARO_DEPARTMENT>> result = new ApiResultObject<List<HIS_CARO_DEPARTMENT>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(listData);
                List<HIS_CARO_DEPARTMENT> resultData = null;
                if (valid && new HisCaroDepartmentCreate(param).CreateList(listData))
                {
                    resultData = listData;
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
        public ApiResultObject<HIS_CARO_DEPARTMENT> Update(HIS_CARO_DEPARTMENT data)
        {
            ApiResultObject<HIS_CARO_DEPARTMENT> result = new ApiResultObject<HIS_CARO_DEPARTMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARO_DEPARTMENT resultData = null;
                if (valid && new HisCaroDepartmentUpdate(param).Update(data))
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
        public ApiResultObject<HIS_CARO_DEPARTMENT> ChangeLock(HIS_CARO_DEPARTMENT data)
        {
            ApiResultObject<HIS_CARO_DEPARTMENT> result = new ApiResultObject<HIS_CARO_DEPARTMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARO_DEPARTMENT resultData = null;
                if (valid && new HisCaroDepartmentLock(param).ChangeLock(data))
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
        public ApiResultObject<HIS_CARO_DEPARTMENT> Lock(long id)
        {
            ApiResultObject<HIS_CARO_DEPARTMENT> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CARO_DEPARTMENT resultData = null;
                if (valid)
                {
                    new HisCaroDepartmentLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_CARO_DEPARTMENT> Unlock(long id)
        {
            ApiResultObject<HIS_CARO_DEPARTMENT> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CARO_DEPARTMENT resultData = null;
                if (valid)
                {
                    new HisCaroDepartmentLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisCaroDepartmentTruncate(param).Truncate(id);
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
                    resultData = new HisCaroDepartmentTruncate(param).TruncateList(ids);
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
        public ApiResultObject<List<HIS_CARO_DEPARTMENT>> CopyByCashierRoom(HisCaroDepaCopyByCashierRoomSDO data)
        {
            ApiResultObject<List<HIS_CARO_DEPARTMENT>> result = new ApiResultObject<List<HIS_CARO_DEPARTMENT>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_CARO_DEPARTMENT> resultData = null;
                if (valid)
                {
                    new HisCaroDepartmentCopyByCashierRoom(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_CARO_DEPARTMENT>> CopyByDepartment(HisCaroDepaCopyByDepartmentSDO data)
        {
            ApiResultObject<List<HIS_CARO_DEPARTMENT>> result = new ApiResultObject<List<HIS_CARO_DEPARTMENT>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_CARO_DEPARTMENT> resultData = null;
                if (valid)
                {
                    new HisCaroDepartmentCopyByDepartment(param).Run(data, ref resultData);
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
