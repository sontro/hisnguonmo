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

namespace MOS.MANAGER.HisTransaction.UnrejectCancellationRequest
{
    class HisTransactionUnrejectCancellationRequest: BusinessBase
    {
        private List<HIS_TRANSACTION> beforeUpdateHisTransactions = new List<HIS_TRANSACTION>();

        internal HisTransactionUnrejectCancellationRequest()
            : base()
        {
            this.Init();
        }

        internal HisTransactionUnrejectCancellationRequest(CommonParam paramDelete)
            : base(paramDelete)
        {
            this.Init();
        }

        private void Init()
        {
        }

        /// <summary>
        /// Huy giao dich
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(HisTransactionUnrejectCancellationRequestSDO data, ref V_HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                V_HIS_TRANSACTION raw = null;

                HisTransactionCheck checker = new HisTransactionCheck(param);
                HisTransactionUnrejectCancellationRequestCheck unCancelChecker = new HisTransactionUnrejectCancellationRequestCheck(param);

                valid = valid && checker.VerifyViewId(data.TransactionId, ref raw);
                valid = valid && unCancelChecker.VerifyRequireField(data);
                valid = valid && unCancelChecker.IsAllow(data, raw);
                

                if (valid)
                {
                    StringBuilder query = new StringBuilder("UPDATE HIS_TRANSACTION SET ");
                    List<object> listParam = new List<object>();

                    query.Append(" CANCEL_REQ_STT = :param1");
                    listParam.Add(IMSys.DbConfig.HIS_RS.CANCEL_REQ_STT.ID__CANCEL_REQ);

                    query.Append(", CANCEL_REQ_REJECT_LOGINNAME = NULL");
                    query.Append(", CANCEL_REQ_REJECT_USERNAME = NULL");
                    query.Append(", CANCEL_REQ_REJECT_REASON = NULL");
                    query.Append(", CANCEL_REQ_REJECT_TIME = NULL");
                    query.Append(" WHERE ID = :param2");
                    listParam.Add(data.TransactionId);

                    if (!DAOWorker.SqlDAO.Execute(query.ToString(), listParam.ToArray()))
                    {
                        throw new Exception("Update HIS_TRANSACTION that bai. Ket thuc nghiep vu");
                    }
                    resultData = new HisTransactionGet().GetViewById(data.TransactionId);
                    result = true;

                    new EventLogGenerator(EventLog.Enum.HisTransaction_KhoiPhucYeuCauHuyGiaoDich)
                        .TransactionCode(raw.TRANSACTION_CODE).Run();
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
