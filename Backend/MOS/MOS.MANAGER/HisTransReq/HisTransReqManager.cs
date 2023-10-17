using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.TDO;
//using MOS.MANAGER.HisTransReq.TwoBook;
//using MOS.MANAGER.HisTransReq.CallBack;
//using MOS.MANAGER.HisTransReq.Bill;

namespace MOS.MANAGER.HisTransReq
{
    public partial class HisTransReqManager : BusinessBase
    {
        public HisTransReqManager()
            : base()
        {

        }

        public HisTransReqManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_TRANS_REQ>> Get(HisTransReqFilterQuery filter)
        {
            ApiResultObject<List<HIS_TRANS_REQ>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TRANS_REQ> resultData = null;
                if (valid)
                {
                    resultData = new HisTransReqGet(param).Get(filter);
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
        public ApiResultObject<HIS_TRANS_REQ> Create(HIS_TRANS_REQ data)
        {
            ApiResultObject<HIS_TRANS_REQ> result = new ApiResultObject<HIS_TRANS_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANS_REQ resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTransReqCreate(param).Create(data);
                    resultData = isSuccess ? data : null;
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
        public ApiResultObject<HIS_TRANS_REQ> Update(HIS_TRANS_REQ data)
        {
            ApiResultObject<HIS_TRANS_REQ> result = new ApiResultObject<HIS_TRANS_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANS_REQ resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTransReqUpdate(param).Update(data);
                    resultData = isSuccess ? data : null;
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
        public ApiResultObject<HIS_TRANS_REQ> ChangeLock(long id)
        {
            ApiResultObject<HIS_TRANS_REQ> result = new ApiResultObject<HIS_TRANS_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TRANS_REQ resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTransReqLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_TRANS_REQ> Lock(long id)
        {
            ApiResultObject<HIS_TRANS_REQ> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TRANS_REQ resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTransReqLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_TRANS_REQ> Unlock(long id)
        {
            ApiResultObject<HIS_TRANS_REQ> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TRANS_REQ resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTransReqLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisTransReqTruncate(param).Truncate(id);
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
        public ApiResultObject<List<HIS_TRANS_REQ>> CreateBillTwoBook(HisTransReqBillTwoBookSDO data)
        {
            ApiResultObject<List<HIS_TRANS_REQ>> result = new ApiResultObject<List<HIS_TRANS_REQ>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_TRANS_REQ> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    //isSuccess = new HisTransReqBillTwoBookCreate(param).Run(data, ref resultData);
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
        public ApiResultObject<bool> CallbackPaylater(HisTransReqCallbackSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool isSuccess = false;
                if (valid)
                {
                    //isSuccess = new HisTransReqCallback(param).Run(data);
                }
                result = this.PackSingleResult(isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TRANS_REQ> CreateBill(HisTransReqBillSDO data)
        {
            ApiResultObject<HIS_TRANS_REQ> result = new ApiResultObject<HIS_TRANS_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANS_REQ resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    //isSuccess = new HisTransReqBillCreate(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_TRANS_REQ> UpdateQrInfo(HisTransReqBankInfoSDO data)
        {
            ApiResultObject<HIS_TRANS_REQ> result = new ApiResultObject<HIS_TRANS_REQ>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANS_REQ resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    //isSuccess = new HisTransReqUpdateQrInfo(param).Run(data, ref resultData);
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
        public ApiResultObject<MerchantPaymentResultSDO> BankPayment(MerchantPaymentSDO data)
        {
            ApiResultObject<MerchantPaymentResultSDO> result = new ApiResultObject<MerchantPaymentResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                MerchantPaymentResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    //isSuccess = new HisTransReqBankPayment(param).Run(data, ref resultData);
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
        public ApiResultObject<PaymentVietinbankResultTDO> QrPaymentVietinbank(PaymentVietinbankTDO data)
        {
            ApiResultObject<PaymentVietinbankResultTDO> result = new ApiResultObject<PaymentVietinbankResultTDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                PaymentVietinbankResultTDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new Vietinbank.QrPayment(param).Run(data, ref resultData);
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
        public ApiResultObject<PaymentBidvResultTDO> QrPaymentBIDV(PaymentBidvTDO data)
        {
            ApiResultObject<PaymentBidvResultTDO> result = new ApiResultObject<PaymentBidvResultTDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                PaymentBidvResultTDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new BIDV.QrPayment(param).Run(data, ref resultData);
                    if (!isSuccess) Inventec.Common.Logging.LogSystem.Error("Xác nhận thanh toán BIDV thất bại"+Inventec.Common.Logging.LogUtil.TraceData("ResultData", resultData));
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
