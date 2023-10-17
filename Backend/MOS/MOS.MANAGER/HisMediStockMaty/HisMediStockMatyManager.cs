using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStockMaty
{
    public partial class HisMediStockMatyManager : BusinessBase
    {
        public HisMediStockMatyManager()
            : base()
        {

        }

        public HisMediStockMatyManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEDI_STOCK_MATY>> Get(HisMediStockMatyFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDI_STOCK_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDI_STOCK_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisMediStockMatyGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_MEDI_STOCK_MATY>> GetView(HisMediStockMatyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDI_STOCK_MATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDI_STOCK_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisMediStockMatyGet(param).GetView(filter);
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
        public ApiResultObject<List<V_HIS_MEDI_STOCK_MATY_1>> GetView1(HisMediStockMatyView1FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDI_STOCK_MATY_1>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDI_STOCK_MATY_1> resultData = null;
                if (valid)
                {
                    resultData = new HisMediStockMatyGet(param).GetView1(filter);
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
        public ApiResultObject<HIS_MEDI_STOCK_MATY> Create(HIS_MEDI_STOCK_MATY data)
        {
            ApiResultObject<HIS_MEDI_STOCK_MATY> result = new ApiResultObject<HIS_MEDI_STOCK_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_STOCK_MATY resultData = null;
                if (valid && new HisMediStockMatyCreate(param).Create(data))
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
        public ApiResultObject<List<HIS_MEDI_STOCK_MATY>> CreateList(List<HIS_MEDI_STOCK_MATY> data)
        {
            ApiResultObject<List<HIS_MEDI_STOCK_MATY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_MATY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEDI_STOCK_MATY> resultData = null;
                if (valid && new HisMediStockMatyCreate(param).CreateList(data))
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
        public ApiResultObject<List<HIS_MEDI_STOCK_MATY>> UpdateList(List<HIS_MEDI_STOCK_MATY> data)
        {
            ApiResultObject<List<HIS_MEDI_STOCK_MATY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_MATY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEDI_STOCK_MATY> resultData = null;
                if (valid && new HisMediStockMatyUpdate(param).UpdateList(data))
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
        public ApiResultObject<HIS_MEDI_STOCK_MATY> Update(HIS_MEDI_STOCK_MATY data)
        {
            ApiResultObject<HIS_MEDI_STOCK_MATY> result = new ApiResultObject<HIS_MEDI_STOCK_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_STOCK_MATY resultData = null;
                if (valid && new HisMediStockMatyUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEDI_STOCK_MATY> ChangeLock(HIS_MEDI_STOCK_MATY data)
        {
            ApiResultObject<HIS_MEDI_STOCK_MATY> result = new ApiResultObject<HIS_MEDI_STOCK_MATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDI_STOCK_MATY resultData = null;
                if (valid && new HisMediStockMatyLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_MEDI_STOCK_MATY data)
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
                    resultData = new HisMediStockMatyTruncate(param).Truncate(data);
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
                    resultData = new HisMediStockMatyTruncate(param).TruncateList(ids);
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
        public ApiResultObject<List<HIS_MEDI_STOCK_MATY>> CopyByMediStock(HisMestMatyCopyByMediStockSDO data)
        {
            ApiResultObject<List<HIS_MEDI_STOCK_MATY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_MATY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEDI_STOCK_MATY> resultData = null;
                if (valid)
                {
                    new HisMediStockMatyCopyByMediStock(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_MEDI_STOCK_MATY>> CopyByMaty(HisMestMatyCopyByMatySDO data)
        {
            ApiResultObject<List<HIS_MEDI_STOCK_MATY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_MATY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MEDI_STOCK_MATY> resultData = null;
                if (valid)
                {
                    new HisMediStockMatyCopyByMaty(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_MEDI_STOCK_MATY>> Import(List<HIS_MEDI_STOCK_MATY> listData)
        {
            ApiResultObject<List<HIS_MEDI_STOCK_MATY>> result = new ApiResultObject<List<HIS_MEDI_STOCK_MATY>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(listData);
                List<HIS_MEDI_STOCK_MATY> resultData = null;
                if (valid)
                {
                    new HisMediStockMatyImport(param).Run(listData, ref resultData);
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
