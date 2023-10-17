using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineBean
{
    public partial class HisMedicineBeanManager : BusinessBase
    {
        public HisMedicineBeanManager()
            : base()
        {

        }

        public HisMedicineBeanManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEDICINE_BEAN>> Get(HisMedicineBeanFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDICINE_BEAN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDICINE_BEAN> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineBeanGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_MEDICINE_BEAN>> GetView(HisMedicineBeanViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDICINE_BEAN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDICINE_BEAN> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineBeanGet(param).GetView(filter);
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
        public ApiResultObject<List<HIS_MEDICINE_BEAN>> Take(TakeBeanSDO data)
        {
            ApiResultObject<List<HIS_MEDICINE_BEAN>> result = new ApiResultObject<List<HIS_MEDICINE_BEAN>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEDICINE_BEAN> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineBeanTake(param).Take(data);
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
        public ApiResultObject<List<TakeMedicineBeanListResultSDO>> Take(List<TakeBeanSDO> data)
        {
            ApiResultObject<List<TakeMedicineBeanListResultSDO>> result = new ApiResultObject<List<TakeMedicineBeanListResultSDO>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<TakeMedicineBeanListResultSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineBeanTake(param).Take(data);
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
        public ApiResultObject<List<HIS_MEDICINE_BEAN>> TakeByMedicine(TakeBeanByMameSDO data)
        {
            ApiResultObject<List<HIS_MEDICINE_BEAN>> result = new ApiResultObject<List<HIS_MEDICINE_BEAN>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEDICINE_BEAN> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineBeanTakeByMedicine(param).Take(data);
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
        public ApiResultObject<List<TakeMedicineBeanByMedicineListResultSDO>> TakeByMedicine(List<TakeBeanByMameSDO> data)
        {
            ApiResultObject<List<TakeMedicineBeanByMedicineListResultSDO>> result = new ApiResultObject<List<TakeMedicineBeanByMedicineListResultSDO>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<TakeMedicineBeanByMedicineListResultSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineBeanTakeByMedicine(param).Take(data);
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
        public ApiResultObject<bool> Release(ReleaseBeanSDO data)
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
                    resultData = new HisMedicineBeanRelease(param).Release(data);
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
        public ApiResultObject<bool> Release(string data)
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
                    resultData = new HisMedicineBeanRelease(param).Release(data);
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
