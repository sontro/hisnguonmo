using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockMety
{
    public partial class HisMediStockMetyManager : BusinessBase
    {
        public HisMediStockMetyManager()
            : base()
        {

        }

        public HisMediStockMetyManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEDI_STOCK_METY>> Get(HisMediStockMetyFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDI_STOCK_METY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDI_STOCK_METY> resultData = null;
                if (valid)
                {
                    resultData = new HisMediStockMetyGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_MEDI_STOCK_METY>> GetView(HisMediStockMetyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDI_STOCK_METY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDI_STOCK_METY> resultData = null;
                if (valid)
                {
                    resultData = new HisMediStockMetyGet(param).GetView(filter);
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
        public ApiResultObject<List<V_HIS_MEDI_STOCK_METY_1>> GetView1(HisMediStockMetyView1FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDI_STOCK_METY_1>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDI_STOCK_METY_1> resultData = null;
                if (valid)
                {
                    resultData = new HisMediStockMetyGet(param).GetView1(filter);
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
        public ApiResultObject<HIS_MEDI_STOCK_METY> Create(HIS_MEDI_STOCK_METY data)
        {
            ApiResultObject<HIS_MEDI_STOCK_METY> result = new ApiResultObject<HIS_MEDI_STOCK_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_STOCK_METY resultData = null;
                if (valid && new HisMediStockMetyCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_MEDI_STOCK_METY>> CreateList(List<HIS_MEDI_STOCK_METY> data)
        {
            ApiResultObject<List<HIS_MEDI_STOCK_METY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_METY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEDI_STOCK_METY> resultData = null;
                if (valid && new HisMediStockMetyCreate(param).CreateList(data))
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
        public ApiResultObject<List<HIS_MEDI_STOCK_METY>> UpdateList(List<HIS_MEDI_STOCK_METY> data)
        {
            ApiResultObject<List<HIS_MEDI_STOCK_METY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_METY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEDI_STOCK_METY> resultData = null;
                if (valid && new HisMediStockMetyUpdate(param).UpdateList(data))
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
        public ApiResultObject<HIS_MEDI_STOCK_METY> Update(HIS_MEDI_STOCK_METY data)
        {
            ApiResultObject<HIS_MEDI_STOCK_METY> result = new ApiResultObject<HIS_MEDI_STOCK_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_STOCK_METY resultData = null;
                if (valid && new HisMediStockMetyUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEDI_STOCK_METY> ChangeLock(HIS_MEDI_STOCK_METY data)
        {
            ApiResultObject<HIS_MEDI_STOCK_METY> result = new ApiResultObject<HIS_MEDI_STOCK_METY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_STOCK_METY resultData = null;
                if (valid && new HisMediStockMetyLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_MEDI_STOCK_METY data)
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
                    resultData = new HisMediStockMetyTruncate(param).Truncate(data);
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
                    resultData = new HisMediStockMetyTruncate(param).TruncateList(ids);
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
        public ApiResultObject<List<HIS_MEDI_STOCK_METY>> CopyByMediStock(HisMestMetyCopyByMediStockSDO data)
        {
            ApiResultObject<List<HIS_MEDI_STOCK_METY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_METY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEDI_STOCK_METY> resultData = null;
                if (valid)
                {
                    new HisMediStockMetyCopyByMediStock(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_MEDI_STOCK_METY>> CopyByMety(HisMestMetyCopyByMetySDO data)
        {
            ApiResultObject<List<HIS_MEDI_STOCK_METY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_METY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEDI_STOCK_METY> resultData = null;
                if (valid)
                {
                    new HisMediStockMetyCopyByMety(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_MEDI_STOCK_METY>> Import(List<HIS_MEDI_STOCK_METY> listData)
        {
            ApiResultObject<List<HIS_MEDI_STOCK_METY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_METY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(listData);
                List<HIS_MEDI_STOCK_METY> resultData = null;
                if (valid)
                {
                    new HisMediStockMetyImport(param).Run(listData, ref resultData);
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
