using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicine
{
    public partial class HisMedicineManager : BusinessBase
    {
        public HisMedicineManager()
            : base()
        {

        }

        public HisMedicineManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEDICINE>> Get(HisMedicineFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDICINE>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDICINE> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_MEDICINE>> GetView(HisMedicineViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDICINE>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDICINE> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineGet(param).GetView(filter);
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
        public ApiResultObject<List<HisMedicineInStockSDO>> GetInStockMedicine(HisMedicineStockViewFilter filter)
        {
            ApiResultObject<List<HisMedicineInStockSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisMedicineInStockSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineGet(param).GetInStockMedicine(filter);
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
        public ApiResultObject<List<HisMedicineIn2StockSDO>> GetIn2StockMedicine(HisMedicine2StockFilter filter)
        {
            ApiResultObject<List<HisMedicineIn2StockSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisMedicineIn2StockSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineGet(param).GetIn2StockMedicine(filter);
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
        public ApiResultObject<List<HisMedicineInStockSDO>> GetInStockMedicineWithTypeTreeOrderByAmount(HisMedicineStockViewFilter filter)
        {
            ApiResultObject<List<HisMedicineInStockSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisMedicineInStockSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineGet(param).GetInStockMedicineWithTypeTreeOrderByAmount(filter);
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
        public ApiResultObject<List<List<HisMedicineInStockSDO>>> GetInStockMedicineWithTypeTreeOrderByExpiredDate(HisMedicineStockViewFilter filter)
        {
            ApiResultObject<List<List<HisMedicineInStockSDO>>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<List<HisMedicineInStockSDO>> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineGet(param).GetInStockMedicineWithTypeTreeOrderByExpiredDate(filter);
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
        public ApiResultObject<HIS_MEDICINE> Create(HIS_MEDICINE data)
        {
            ApiResultObject<HIS_MEDICINE> result = new ApiResultObject<HIS_MEDICINE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE resultData = null;
                if (valid && new HisMedicineCreate(param).Create(data))
                {
                    resultData = data;
                }
                this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_MEDICINE> Update(HIS_MEDICINE data)
        {
            ApiResultObject<HIS_MEDICINE> result = new ApiResultObject<HIS_MEDICINE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE resultData = null;
                if (valid && new HisMedicineUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEDICINE> UpdateSdo(HisMedicineSDO data)
        {
            ApiResultObject<HIS_MEDICINE> result = new ApiResultObject<HIS_MEDICINE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE resultData = null;
                if (valid)
                {
                    new HisMedicineUpdateSdo(param).Run(data, ref resultData);
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
        public ApiResultObject<bool> Lock(HisMedicineChangeLockSDO data)
        {
            ApiResultObject<bool> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisMedicineLock(param).Lock(data);
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
        public ApiResultObject<bool> Unlock(HisMedicineChangeLockSDO data)
        {
            ApiResultObject<bool> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisMedicineLock(param).Unlock(data);
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
        public ApiResultObject<bool> ReturnAvailable(HisMedicineReturnAvailableSDO data)
        {
            ApiResultObject<bool> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisMedicineReturnAvailable(param).ReturnAvailable(data);
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
