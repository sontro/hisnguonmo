using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterial
{
    public partial class HisMaterialManager : BusinessBase
    {
        public HisMaterialManager()
            : base()
        {

        }

        public HisMaterialManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MATERIAL>> Get(HisMaterialFilterQuery filter)
        {
            ApiResultObject<List<HIS_MATERIAL>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MATERIAL> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_MATERIAL>> GetView(HisMaterialViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MATERIAL>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MATERIAL> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialGet(param).GetView(filter);
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
        public ApiResultObject<List<HisMaterialInStockSDO>> GetInStockMaterial(HisMaterialStockViewFilter filter)
        {
            ApiResultObject<List<HisMaterialInStockSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisMaterialInStockSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialGet(param).GetInStockMaterial(filter);
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
        public ApiResultObject<List<HisMaterialIn2StockSDO>> GetIn2StockMaterial(HisMaterial2StockFilter filter)
        {
            ApiResultObject<List<HisMaterialIn2StockSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisMaterialIn2StockSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialGet(param).GetIn2StockMaterial(filter);
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
        public ApiResultObject<List<HisMaterialInStockSDO>> GetInStockMaterialWithTypeTreeOrderByAmount(HisMaterialStockViewFilter filter)
        {
            ApiResultObject<List<HisMaterialInStockSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisMaterialInStockSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialGet(param).GetInStockMaterialWithTypeTreeOrderByAmount(filter);
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
        public ApiResultObject<List<List<HisMaterialInStockSDO>>> GetInStockMaterialWithTypeTreeOrderByExpiredDate(HisMaterialStockViewFilter filter)
        {
            ApiResultObject<List<List<HisMaterialInStockSDO>>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<List<HisMaterialInStockSDO>> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialGet(param).GetInStockMaterialWithTypeTreeOrderByExpiredDate(filter);
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
        public ApiResultObject<HIS_MATERIAL> Create(HIS_MATERIAL data)
        {
            ApiResultObject<HIS_MATERIAL> result = new ApiResultObject<HIS_MATERIAL>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL resultData = null;
                if (valid && new HisMaterialCreate(param).Create(data))
                {
                    resultData = data;
                }
                this.PackSingleResult(data);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_MATERIAL> Update(HIS_MATERIAL data)
        {
            ApiResultObject<HIS_MATERIAL> result = new ApiResultObject<HIS_MATERIAL>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL resultData = null;
                if (valid && new HisMaterialUpdate(param).Update(data))
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
        public ApiResultObject<bool> Lock(HisMaterialChangeLockSDO data)
        {
            ApiResultObject<bool> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisMaterialLock(param).Lock(data);
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
        public ApiResultObject<bool> Unlock(HisMaterialChangeLockSDO data)
        {
            ApiResultObject<bool> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisMaterialLock(param).Unlock(data);
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
        public ApiResultObject<bool> ReturnAvailable(HisMaterialReturnAvailableSDO data)
        {
            ApiResultObject<bool> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisMaterialReturnAvailable(param).ReturnAvailable(data);
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
