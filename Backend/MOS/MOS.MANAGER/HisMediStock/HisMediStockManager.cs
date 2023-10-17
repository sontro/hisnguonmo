using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMediStock.Inventory;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStock
{
    public class HisMediStockManager : BusinessBase
    {
        public HisMediStockManager()
            : base()
        {

        }

        public HisMediStockManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEDI_STOCK>> Get(HisMediStockFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDI_STOCK>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDI_STOCK> resultData = null;
                if (valid)
                {
                    resultData = new HisMediStockGet(param).Get(filter);
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
        public ApiResultObject<List<D_HIS_MEDI_STOCK_1>> GetDHisMediStock1(DHisMediStock1Filter filter)
        {
            ApiResultObject<List<D_HIS_MEDI_STOCK_1>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<D_HIS_MEDI_STOCK_1> resultData = null;
                if (valid)
                {
                    resultData = new HisMediStockGet(param).GetDHisMediStock1(filter);
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

        public ApiResultObject<List<D_HIS_MEDI_STOCK_2>> GetDHisMediStock2(DHisMediStock2Filter filter)
        {
            ApiResultObject<List<D_HIS_MEDI_STOCK_2>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<D_HIS_MEDI_STOCK_2> resultData = null;
                if (valid)
                {
                    resultData = new HisMediStockGet(param).GetDHisMediStock2(filter);
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
        public ApiResultObject<List<V_HIS_MEDI_STOCK>> GetView(HisMediStockViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDI_STOCK>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDI_STOCK> resultData = null;
                if (valid)
                {
                    resultData = new HisMediStockGet(param).GetView(filter);
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
        public ApiResultObject<HisMediStockSDO> Create(HisMediStockSDO data)
        {
            ApiResultObject<HisMediStockSDO> result = new ApiResultObject<HisMediStockSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisMediStockSDO resultData = null;
                if (valid && new HisMediStockCreate(param).Create(data))
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
        public ApiResultObject<HisMediStockSDO> Update(HisMediStockSDO data)
        {
            ApiResultObject<HisMediStockSDO> result = new ApiResultObject<HisMediStockSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisMediStockSDO resultData = null;
                if (valid && new HisMediStockUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEDI_STOCK> ChangeLock(HIS_MEDI_STOCK data)
        {
            ApiResultObject<HIS_MEDI_STOCK> result = new ApiResultObject<HIS_MEDI_STOCK>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_MEDI_STOCK resultData = null;
                if (valid && new HisMediStockLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_MEDI_STOCK data)
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
                    resultData = new HisMediStockTruncate(param).Truncate(data);
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
        public ApiResultObject<List<HisMediStockSDO>> CreateList(List<HisMediStockSDO> listData)
        {
            ApiResultObject<List<HisMediStockSDO>> result = new ApiResultObject<List<HisMediStockSDO>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HisMediStockSDO> resultData = null;
                if (valid && new HisMediStockCreate(param).CreateList(listData))
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
        public ApiResultObject<HisMediStockReplaceSDO> GetReplaceSDO(HisMediStockReplaceSDOFilter filter)
        {
            ApiResultObject<HisMediStockReplaceSDO> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                HisMediStockReplaceSDO resultData = null;
                if (valid)
                {
                    resultData = new HisMediStockGet(param).GetReplaceSDO(filter);
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
        public ApiResultObject<HisMediStockInventoryResultSDO> Inventory(HisMediStockInventorySDO data)
        {
            ApiResultObject<HisMediStockInventoryResultSDO> result = new ApiResultObject<HisMediStockInventoryResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisMediStockInventoryResultSDO resultData = null;
                if (valid)
                {
                    new HisMediStockInventory(param).Run(data, ref resultData);
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
