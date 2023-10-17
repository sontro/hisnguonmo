using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMediStockPeriod.Approve;
using MOS.MANAGER.HisMediStockPeriod.Unapprove;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockPeriod
{
    public partial class HisMediStockPeriodManager : BusinessBase
    {
        public HisMediStockPeriodManager()
            : base()
        {

        }

        public HisMediStockPeriodManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEDI_STOCK_PERIOD>> Get(HisMediStockPeriodFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDI_STOCK_PERIOD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDI_STOCK_PERIOD> resultData = null;
                if (valid)
                {
                    resultData = new HisMediStockPeriodGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_MEDI_STOCK_PERIOD>> GetView(HisMediStockPeriodViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDI_STOCK_PERIOD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDI_STOCK_PERIOD> resultData = null;
                if (valid)
                {
                    resultData = new HisMediStockPeriodGet(param).GetView(filter);
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
        public ApiResultObject<HIS_MEDI_STOCK_PERIOD> Create(HIS_MEDI_STOCK_PERIOD data)
        {
            ApiResultObject<HIS_MEDI_STOCK_PERIOD> result = new ApiResultObject<HIS_MEDI_STOCK_PERIOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDI_STOCK_PERIOD resultData = null;
                if (valid && new HisMediStockPeriodCreate(param).Create(data, ref resultData))
                {

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
        public ApiResultObject<HIS_MEDI_STOCK_PERIOD> Update(HIS_MEDI_STOCK_PERIOD data)
        {
            ApiResultObject<HIS_MEDI_STOCK_PERIOD> result = new ApiResultObject<HIS_MEDI_STOCK_PERIOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_STOCK_PERIOD resultData = null;
                if (valid && new HisMediStockPeriodUpdate(param).Update(data))
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
        public ApiResultObject<bool> UpdateInventory(HisMediStockPeriodInventorySDO data)
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
                    resultData = new HisMediStockPeriodUpdateInventory(param).Run(data);
                }
                result = this.PackResult(resultData, resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HisMestPeriodApproveResultSDO> Approve(HisMestPeriodApproveSDO data)
        {
            ApiResultObject<HisMestPeriodApproveResultSDO> result = new ApiResultObject<HisMestPeriodApproveResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisMestPeriodApproveResultSDO resultData = null;
                if (valid)
                {
                    new HisMediStockPeriodApprove(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_MEDI_STOCK_PERIOD> Unapprove(HisMestPeriodApproveSDO data)
        {
            ApiResultObject<HIS_MEDI_STOCK_PERIOD> result = new ApiResultObject<HIS_MEDI_STOCK_PERIOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_STOCK_PERIOD resultData = null;
                if (valid)
                {
                    new HisMediStockPeriodUnapprove(param).Run(data, ref resultData);
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
                    resultData = new HisMediStockPeriodTruncate(param).Truncate(id);
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
