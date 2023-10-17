using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisRoom;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.RequestCancel
{
    class HisTransactionRequestCancel : BusinessBase
    {
        internal HisTransactionRequestCancel()
            : base()
        {
            this.Init();
        }

        internal HisTransactionRequestCancel(CommonParam paramDelete)
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
        internal bool Run(HisTransactionRequestCancelSDO data, ref V_HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                V_HIS_TRANSACTION raw = null;
                HisTransactionCheck checker = new HisTransactionCheck(param);
                HisTransactionRequestCancelCheck cancelChecker = new HisTransactionRequestCancelCheck(param);

                valid = valid && checker.VerifyViewId(data.TransactionId, ref raw);
                valid = valid && cancelChecker.VerifyRequireField(data);
                valid = valid && cancelChecker.IsAllow(data, raw);

                if (valid)
                {
                    //List<string> sqls = new List<string>();
                    //this.ProcessTransaction(ref sqls, data);;

                    string CancelReqReason = data.CancelReqReason;
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    string username = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();

                    StringBuilder query = new StringBuilder("UPDATE HIS_TRANSACTION SET ");
                    List<object> listParam = new List<object>();
                    query.Append(" CANCEL_REQ_STT = :param1");
                    listParam.Add(IMSys.DbConfig.HIS_RS.CANCEL_REQ_STT.ID__CANCEL_REQ);

                    query.Append(", CANCEL_REQ_REASON = :param2");
                    listParam.Add(CancelReqReason);

                    query.Append(", CANCEL_REQ_ROOM_ID = :param3");
                    listParam.Add(data.WorkingRoomId);

                    query.Append(", CANCEL_REQ_LOGINNAME = :param4");
                    listParam.Add(loginname);

                    query.Append(", CANCEL_REQ_USERNAME = :param5");
                    listParam.Add(username);

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

                    string eventLog = "";
                    ProcessEventLog(resultData, ref eventLog);
                    new EventLogGenerator(EventLog.Enum.HisTransacton_YeuCauHuyGiaoDich, eventLog)
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

        private void ProcessEventLog(V_HIS_TRANSACTION data, ref string eventLog)
        {
            try
            {
                V_HIS_TRANSACTION transaction = new HisTransactionGet().GetViewById(data.ID);
                V_HIS_ROOM vRoom = new HisRoomGet().GetViewById(data.CANCEL_REQ_ROOM_ID ?? 0);
                List<string> logs = new List<string>();
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.KhoaYeuCau), transaction != null ? transaction.CANCEL_REQ_DEPARTMENT_NAME : ""));
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.PhongYeuCau), vRoom != null ? vRoom.ROOM_NAME : ""));
                eventLog = String.Join(". ", logs);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
