using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBranch;
using MOS.MANAGER.HisCancelReason;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTT.SDO;

namespace MOS.MANAGER.HisTransaction.Cancel.Epayment
{
    class HisTransactionCancelEpaymentProccessor: BusinessBase
    {
        public const string SUCCESS = "00";
        internal HisTransactionCancelEpaymentProccessor()
            : base()
        {

        }

        internal HisTransactionCancelEpaymentProccessor(CommonParam param)
            : base(param)
        {

        }

        /// <summary>
        /// Xu ly nghiep vu lien quan den hủy giao dịch qua hệ thống thẻ mà không cần cài đặt phần mềm The.desktop
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(HisTransactionCancelSDO data, HIS_TRANSACTION raw, V_HIS_CASHIER_ROOM cashierRoom)
        {
            try
            {
                HIS_BRANCH branch = new HisBranchGet().GetById(cashierRoom.BRANCH_ID);
                string hashKey = ConfigurationManager.AppSettings["Inventec.The.HashKey"];

                YttVoidHisCancelSDO input = new YttVoidHisCancelSDO();
                input.Amount = long.Parse(Math.Round(raw.AMOUNT, 0).ToString());
                input.VoidTransCode = raw.TIG_TRANSACTION_CODE;
                input.BranchCode = branch != null ? branch.THE_BRANCH_CODE : "";
                input.ClientTraceCode = Guid.NewGuid().ToString();
                if (data.CancelReasonId != null)
                {
                    HIS_CANCEL_REASON cancelReason = new HisCancelReasonGet().GetById(data.CancelReasonId.Value);
                    input.Reason = cancelReason != null ? cancelReason.CANCEL_REASON_NAME : "";
                }
                else
                {
                    input.Reason = data.CancelReason;
                }

                string dataHash = String.Join("|", new List<string>() { input.Amount.ToString(), input.BranchCode, input.Reason, input.VoidTransCode, input.ClientTraceCode });

                input.CheckSum = Inventec.Common.HashUtil.HashProcessor.HashSHA256(dataHash, hashKey);
                YttVoidHisCancelResultSDO result = MOS.ApiConsumerManager.ApiConsumerStore.YttConsumer.Post<YttVoidHisCancelResultSDO>(true, "api/YttVoid/HisCancel", param, input);
                if (result != null && result.ResultCode == SUCCESS)
                {
                    raw.TIG_VOID_CODE = result.TransCode;
                    raw.TIG_VOID_TIME = result.TransTime;
                }
                else if (result == null || result.ResultCode != SUCCESS)
                {
                    string yttResultCode = result != null ? result.ResultCode : "";
                    string yttResultDesc = result != null ? result.ResultDesc : "";

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_GiaoDichHeThongTheThatBai, yttResultCode, yttResultDesc);
                    return false;
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }

    }
}
