using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPatientType
{
    public class HisMestPatientTypeManager : BusinessBase
    {
        public HisMestPatientTypeManager()
            : base()
        {

        }

        public HisMestPatientTypeManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEST_PATIENT_TYPE>> Get(HisMestPatientTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEST_PATIENT_TYPE>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEST_PATIENT_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisMestPatientTypeGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<V_HIS_MEST_PATIENT_TYPE>> GetView(HisMestPatientTypeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEST_PATIENT_TYPE>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEST_PATIENT_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisMestPatientTypeGet(param).GetView(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_MEST_PATIENT_TYPE> GetById(long id)
        {
            ApiResultObject<HIS_MEST_PATIENT_TYPE> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_MEST_PATIENT_TYPE resultData = null;
                if (valid)
                {
                    HisMestPatientTypeFilterQuery filter = new HisMestPatientTypeFilterQuery();
                    resultData = new HisMestPatientTypeGet(param).GetById(id, filter);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<V_HIS_MEST_PATIENT_TYPE> GetViewById(long id)
        {
            ApiResultObject<V_HIS_MEST_PATIENT_TYPE> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                V_HIS_MEST_PATIENT_TYPE resultData = null;
                if (valid)
                {
                    HisMestPatientTypeViewFilterQuery filter = new HisMestPatientTypeViewFilterQuery();
                    resultData = new HisMestPatientTypeGet(param).GetViewById(id, filter);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_MEST_PATIENT_TYPE> Create(HIS_MEST_PATIENT_TYPE data)
        {
            ApiResultObject<HIS_MEST_PATIENT_TYPE> result = new ApiResultObject<HIS_MEST_PATIENT_TYPE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PATIENT_TYPE resultData = null;
                if (valid && new HisMestPatientTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_MEST_PATIENT_TYPE> Update(HIS_MEST_PATIENT_TYPE data)
        {
            ApiResultObject<HIS_MEST_PATIENT_TYPE> result = new ApiResultObject<HIS_MEST_PATIENT_TYPE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PATIENT_TYPE resultData = null;
                if (valid && new HisMestPatientTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEST_PATIENT_TYPE> ChangeLock(HIS_MEST_PATIENT_TYPE data)
        {
            ApiResultObject<HIS_MEST_PATIENT_TYPE> result = new ApiResultObject<HIS_MEST_PATIENT_TYPE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_MEST_PATIENT_TYPE resultData = null;
                if (valid && new HisMestPatientTypeLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_MEST_PATIENT_TYPE data)
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
                    resultData = new HisMestPatientTypeTruncate(param).Truncate(data);
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
        public ApiResultObject<List<HIS_MEST_PATIENT_TYPE>> CreateList(List<HIS_MEST_PATIENT_TYPE> listData)
        {
            ApiResultObject<List<HIS_MEST_PATIENT_TYPE>> result = new ApiResultObject<List<HIS_MEST_PATIENT_TYPE>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_MEST_PATIENT_TYPE> resultData = null;
                if (valid && new HisMestPatientTypeCreate(param).CreateList(listData))
                {
                    resultData = listData;
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
                valid = valid && IsNotNullOrEmpty(ids);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisMestPatientTypeTruncate(param).TruncateList(ids);
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
        public ApiResultObject<List<HIS_MEST_PATIENT_TYPE>> CopyByMediStock(HisMestPatientTypeCopyByMediStockSDO data)
        {
            ApiResultObject<List<HIS_MEST_PATIENT_TYPE>> result = new ApiResultObject<List<HIS_MEST_PATIENT_TYPE>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEST_PATIENT_TYPE> resultData = null;
                if (valid)
                {
                    new HisMestPatientTypeCopyByMediStock(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_MEST_PATIENT_TYPE>> CopyByPatientType(HisMestPatientTypeCopyByPatientTypeSDO data)
        {
            ApiResultObject<List<HIS_MEST_PATIENT_TYPE>> result = new ApiResultObject<List<HIS_MEST_PATIENT_TYPE>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEST_PATIENT_TYPE> resultData = null;
                if (valid)
                {
                    new HisMestPatientTypeCopyByPatientType(param).Run(data, ref resultData);
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
