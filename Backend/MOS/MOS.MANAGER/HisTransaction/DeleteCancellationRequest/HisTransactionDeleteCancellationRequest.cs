using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.DeleteCancellationRequest
{
    class HisTransactionDeleteCancellationRequest: BusinessBase
    {
        internal HisTransactionDeleteCancellationRequest()
            : base()
        {
            this.Init();
        }

        internal HisTransactionDeleteCancellationRequest(CommonParam paramDelete)
            : base(paramDelete)
        {
            this.Init();
        }

        private void Init()
        {
        }

        /// <summary>
        /// yêu cầu hủy giao dịch
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(HisTransactionDeleteCancellationRequestSDO data, ref V_HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                V_HIS_TRANSACTION raw = null;
                HisTransactionCheck checker = new HisTransactionCheck(param);
                HisTransactionDeleteCancellationRequestCheck cancelChecker = new HisTransactionDeleteCancellationRequestCheck(param);

                valid = valid && checker.VerifyViewId(data.TransactionId, ref raw);
                valid = valid && cancelChecker.VerifyRequireField(data);
                valid = valid && cancelChecker.IsAllow(data, raw);

                if (valid)
                {
                    StringBuilder query = new StringBuilder("UPDATE HIS_TRANSACTION SET ");
                    List<object> listParam = new List<object>();
                    query.Append(" CANCEL_REQ_STT = NULL");
                    query.Append(", CANCEL_REQ_REASON = NULL");
                    query.Append(", CANCEL_REQ_ROOM_ID = NULL");
                    query.Append(", CANCEL_REQ_LOGINNAME = NULL");
                    query.Append(", CANCEL_REQ_USERNAME = NULL");
                    query.Append(", CANCEL_REQ_REJECT_LOGINNAME = NULL");
                    query.Append(", CANCEL_REQ_REJECT_USERNAME = NULL");
                    query.Append(", CANCEL_REQ_REJECT_TIME = NULL");
                    query.Append(" WHERE ID = :param6");
                    listParam.Add(data.TransactionId);

                    if (!DAOWorker.SqlDAO.Execute(query.ToString(), listParam.ToArray()))
                    {
                        throw new Exception("Update HIS_TRANSACTION that bai. Ket thuc nghiep vu");
                    }
                    resultData = new HisTransactionGet().GetViewById(data.TransactionId);
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisTransacton_XoaYeuCauHuyGiaoDich)
                               .TransactionCode(raw.TRANSACTION_CODE)
                               .Run();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
        
    }
}
