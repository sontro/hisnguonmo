using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.Config.CFG;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSeseTransReq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransReq.CallBack
{
    class HisTransReqBankPaymentCheck : BusinessBase
    {
        internal HisTransReqBankPaymentCheck()
            : base()
        {

        }

        internal HisTransReqBankPaymentCheck(CommonParam param)
            : base(param)
        {

        }


        internal bool VerifyRequireField(SDO.MerchantPaymentSDO data, ref SDO.MerchantPaymentResultSDO resultData)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (String.IsNullOrWhiteSpace(data.checksum)) throw new ArgumentNullException("data.checksum)");
                if (String.IsNullOrWhiteSpace(data.code)) throw new ArgumentNullException("data.code)");
                if (String.IsNullOrWhiteSpace(data.txnId)) throw new ArgumentNullException("data.txnId)");
                if (String.IsNullOrWhiteSpace(data.msgType)) throw new ArgumentNullException("data.msgType)");
                if (String.IsNullOrWhiteSpace(data.amount)) throw new ArgumentNullException("data.amount)");
            }
            catch (ArgumentNullException ex)
            {
                resultData.code = "06";
                resultData.message = "sai thông tin xác thực";
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool VerifyTransReq(SDO.MerchantPaymentSDO data, ref HIS_TRANS_REQ transReq, ref List<HIS_SESE_TRANS_REQ> seseTransReqs, ref SDO.MerchantPaymentResultSDO resultData)
        {
            bool valid = true;
            try
            {
                if (!String.IsNullOrWhiteSpace(data.txnId))
                {
                    HisTransReqFilterQuery filter = new HisTransReqFilterQuery();
                    filter.TRANS_REQ_CODE__EXACT = data.txnId;

                    List<HIS_TRANS_REQ> transReqs = new HisTransReqGet().Get(filter);
                    if (!IsNotNullOrEmpty(transReqs))
                    {
                        resultData.code = "06";
                        resultData.message = "sai thông tin xác thực";
                        Inventec.Common.Logging.LogSystem.Error("TRANS_REQ_CODE invalid: \n" + LogUtil.TraceData("Data", data));
                        return false;
                    }

                    transReq = transReqs.FirstOrDefault();

                    //checkAmount được làm tròn lên 1 đơn vị nên luôn bằng hoặc lớn hơn 1 đơn vị
                    decimal checkAmount = 0;
                    if (!decimal.TryParse(data.amount, out checkAmount) || checkAmount < transReq.AMOUNT || checkAmount - transReq.AMOUNT > 1)
                    {
                        resultData.code = "07";
                        resultData.message = "số tiền không chính xác";
                        dynamic dynamicData = new System.Dynamic.ExpandoObject();
                        dynamicData.amount = transReq.AMOUNT.ToString();
                        resultData.data = dynamicData;
                        Inventec.Common.Logging.LogSystem.Error("AMOUNT invalid: \n" + LogUtil.TraceData("Data", data) + "\n" + LogUtil.TraceData("transReq", transReq));
                        return false;
                    }

                    seseTransReqs = new HisSeseTransReqGet().GetByTransReqId(transReq.ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool VerifySereServ(SDO.MerchantPaymentSDO data, List<HIS_SESE_TRANS_REQ> seseTransReqs, ref List<HIS_SERE_SERV> listSereServ, ref SDO.MerchantPaymentResultSDO resultData)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(seseTransReqs))
                {
                    List<long> sereServIds = seseTransReqs.Select(o => o.SERE_SERV_ID).Distinct().ToList();

                    List<HIS_SERE_SERV_BILL> existsBills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServIds);
                    if (IsNotNullOrEmpty(existsBills))
                    {
                        resultData.code = "03";
                        resultData.message = "Đơn hàng đã được thanh toán";
                        dynamic dynamicData = new System.Dynamic.ExpandoObject();
                        dynamicData.txnId = data.txnId;
                        resultData.data = dynamicData;
                        return false;
                    }

                    listSereServ = new HisSereServGet().GetByIds(sereServIds);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool VerifyCheckSum(SDO.MerchantPaymentSDO data, ref HisTransReqCFG.SecretKeyADO secretKey, ref SDO.MerchantPaymentResultSDO resultData)
        {
            bool result = false;
            try
            {
                if (HisTransReqCFG.SECRET_KEY == null || HisTransReqCFG.SECRET_KEY.Count <= 0)
                {
                    resultData.code = "04";
                    resultData.message = MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTransReq__ChuaCauHinhKeyMaHoa, param.LanguageCode);
                    return false;
                }

                secretKey = HisTransReqCFG.SECRET_KEY.FirstOrDefault(o => o.PROVIDER == HisTransReqCFG.PROVIDER__BIDV);
                if (!IsNotNull(secretKey))
                {
                    resultData.code = "04";
                    resultData.message = MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTransReq__KhongCoCauHinhMaHoaCua, param.LanguageCode);
                    return false;
                }

                List<string> checkData = new List<string>();
                checkData.Add(data.code);
                checkData.Add(data.msgType);
                checkData.Add(data.txnId);
                checkData.Add(data.qrTrace);
                checkData.Add(data.bankCode);
                checkData.Add(data.mobile);
                checkData.Add(data.accountNo);
                checkData.Add(data.amount);
                checkData.Add(data.payDate);
                checkData.Add(data.merchantCode);
                checkData.Add(secretKey.SECRET_KEY);

                string mess = string.Join("|", checkData);

                LogSystem.Info("Data before hash Md5: " + mess);
                string checkSum = Inventec.Common.HashUtil.HashProcessor.HashMD5(mess);
                LogSystem.Info("Data after hash Md5: " + checkSum);

                if (!String.IsNullOrWhiteSpace(data.checksum) && !String.IsNullOrWhiteSpace(checkSum) == data.checksum.Trim().ToLower().Equals(checkSum.Trim().ToLower()))
                {
                    result = true;
                }
                else
                {
                    resultData.code = "06";
                    resultData.message = "sai thông tin xác thực";
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        internal bool IsSttRequest(SDO.MerchantPaymentSDO data, HIS_TRANS_REQ transReq, ref SDO.MerchantPaymentResultSDO resultData)
        {
            bool valid = true;
            try
            {
                if (transReq.TRANS_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__FINISHED)
                {
                    resultData.code = "03";
                    resultData.message = "Đơn hàng đã được thanh toán";
                    dynamic dynamicData = new System.Dynamic.ExpandoObject();
                    dynamicData.txnId = data.txnId;
                    resultData.data = dynamicData;
                    return false;
                }
                else if (transReq.TRANS_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__REQUEST)
                {
                    resultData.code = "04";
                    resultData.message = MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTransReq_TrangThaiCuaYeuCauKhongHopLe, param.LanguageCode);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
