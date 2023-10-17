using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisDispense.Handler.Create;
using MOS.MANAGER.HisDispense.Handler.Update;
using MOS.MANAGER.HisDispense.Handler.Confirm;
using MOS.MANAGER.HisDispense.Handler.UnConfirm;
using MOS.MANAGER.HisDispense.Handler.Delete;
using MOS.MANAGER.HisDispense.Packing.Create;
using MOS.MANAGER.HisDispense.Packing.Update;
using MOS.MANAGER.HisDispense.Packing.Delete;
using MOS.MANAGER.HisDispense.Packing.Confirm;
using MOS.MANAGER.HisDispense.Packing.Unconfirm;

namespace MOS.MANAGER.HisDispense
{
    public partial class HisDispenseManager : BusinessBase
    {
        public HisDispenseManager()
            : base()
        {

        }

        public HisDispenseManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_DISPENSE>> Get(HisDispenseFilterQuery filter)
        {
            ApiResultObject<List<HIS_DISPENSE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DISPENSE> resultData = null;
                if (valid)
                {
                    resultData = new HisDispenseGet(param).Get(filter);
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
        public ApiResultObject<HisDispenseHandlerResultSDO> Create(HisDispenseSDO data)
        {
            ApiResultObject<HisDispenseHandlerResultSDO> result = new ApiResultObject<HisDispenseHandlerResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisDispenseHandlerResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDispenseHandlerCreate(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HisDispenseHandlerResultSDO> Update(HisDispenseUpdateSDO data)
        {
            ApiResultObject<HisDispenseHandlerResultSDO> result = new ApiResultObject<HisDispenseHandlerResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisDispenseHandlerResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDispenseHandlerUpdate(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_DISPENSE> ChangeLock(long id)
        {
            ApiResultObject<HIS_DISPENSE> result = new ApiResultObject<HIS_DISPENSE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DISPENSE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDispenseLock(param).ChangeLock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_DISPENSE> Lock(long id)
        {
            ApiResultObject<HIS_DISPENSE> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DISPENSE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDispenseLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_DISPENSE> Unlock(long id)
        {
            ApiResultObject<HIS_DISPENSE> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DISPENSE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDispenseLock(param).Unlock(id, ref resultData);
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
        public ApiResultObject<bool> Delete(HisDispenseDeleteSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisDispenseHandlerTruncate(param).Run(data);
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
        public ApiResultObject<HisDispenseResultSDO> Confirm(HisDispenseConfirmSDO data)
        {
            ApiResultObject<HisDispenseResultSDO> result = new ApiResultObject<HisDispenseResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisDispenseResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDispenseHandlerConfirm(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HisDispenseResultSDO> UnConfirm(HisDispenseConfirmSDO data)
        {
            ApiResultObject<HisDispenseResultSDO> result = new ApiResultObject<HisDispenseResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisDispenseResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDispenseHandlerUnConfirm(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HisPackingResultSDO> PackingCreate(HisPackingCreateSDO data)
        {
            ApiResultObject<HisPackingResultSDO> result = new ApiResultObject<HisPackingResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisPackingResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDispensePackingCreate(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HisPackingResultSDO> PackingUpdate(HisPackingUpdateSDO data)
        {
            ApiResultObject<HisPackingResultSDO> result = new ApiResultObject<HisPackingResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisPackingResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDispensePackingUpdate(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> PackingDelete(HisPackingSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisDispensePackingTruncate(param).Run(data);
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
        public ApiResultObject<HisPackingResultSDO> PackingConfirm(HisPackingSDO data)
        {
            ApiResultObject<HisPackingResultSDO> result = new ApiResultObject<HisPackingResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisPackingResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDispensePackingConfirm(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HisPackingResultSDO> PackingUnconfirm(HisPackingSDO data)
        {
            ApiResultObject<HisPackingResultSDO> result = new ApiResultObject<HisPackingResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisPackingResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDispensePackingUnconfirm(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
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
