using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.NotTaken;
using MOS.MANAGER.HisExpMest.Sale.Create;
using MOS.MANAGER.HisExpMest.Sale.Get;
using MOS.MANAGER.HisExpMest.Sale.Update;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMest
{
    public partial class HisExpMestManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<HisExpMestResultSDO> SaleCreate(HisExpMestSaleSDO data)
        {
            ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisExpMestResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestSaleCreate(param).Create(data, ref resultData);
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
        public ApiResultObject<HisExpMestResultSDO> SaleUpdate(HisExpMestSaleSDO data)
        {
            ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisExpMestResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestSaleUpdate(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_EXP_MEST> NotTaken(HIS_EXP_MEST data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestNotTaken(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HisExpMestSaleResultSDO>> SaleCreateList(List<HisExpMestSaleSDO> data)
        {
            ApiResultObject<List<HisExpMestSaleResultSDO>> result = new ApiResultObject<List<HisExpMestSaleResultSDO>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HisExpMestSaleResultSDO> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestSaleCreateList(param).Run(data, ref resultData);
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
        public ApiResultObject<HisExpMestSaleListResultSDO> SaleCreateListSdo(HisExpMestSaleListSDO data)
        {
            ApiResultObject<HisExpMestSaleListResultSDO> result = new ApiResultObject<HisExpMestSaleListResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisExpMestSaleListResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestSaleCreateListSdo(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HisExpMestSaleListResultSDO>> SaleCreateBillList(List<HisExpMestSaleListSDO> data)
        {
            ApiResultObject<List<HisExpMestSaleListResultSDO>> result = new ApiResultObject<List<HisExpMestSaleListResultSDO>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HisExpMestSaleListResultSDO> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestSaleCreateBillList(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HisExpMestSaleResultSDO>> SaleUpdateList(List<HisExpMestSaleSDO> data)
        {
            ApiResultObject<List<HisExpMestSaleResultSDO>> result = new ApiResultObject<List<HisExpMestSaleResultSDO>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HisExpMestSaleResultSDO> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestSaleUpdateList(param).Run(data, ref resultData);
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
        public ApiResultObject<HisExpMestSaleListResultSDO> SaleUpdateListSdo(HisExpMestSaleListSDO data)
        {
            ApiResultObject<HisExpMestSaleListResultSDO> result = new ApiResultObject<HisExpMestSaleListResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisExpMestSaleListResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestSaleUpdateListSdo(param).Run(data, ref resultData);
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
        public ApiResultObject<HisExpMestForSaleSDO> GetForSale(HisExpMestForSaleFilter filter)
        {
            ApiResultObject<HisExpMestForSaleSDO> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                HisExpMestForSaleSDO resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestGetForSale(param).GetForSale(filter);
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

    }
}
