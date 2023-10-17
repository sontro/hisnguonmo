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

namespace MOS.MANAGER.HisTransaction.RejectCancellationRequest
{
    class HisTransactionRejectCancellationRequest : BusinessBase
    {
        internal HisTransactionRejectCancellationRequest()
            : base()
        {
            this.Init();
        }

        internal HisTransactionRejectCancellationRequest(CommonParam paramDelete)
            : base(paramDelete)
        {
            this.Init();
        }

        private void Init()
        {
        }

        /// <summary>
        /// Từ chối yêu cầu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(HisTransactionRejectCancellationRequestSDO data, ref V_HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                V_HIS_TRANSACTION raw = null;
                HisTransactionCheck checker = new HisTransactionCheck(param);
                HisTransactionRejectCancellationRequestCheck cancelChecker = new HisTransactionRejectCancellationRequestCheck(param);
                WorkPlaceSDO workPlace = null;

                valid = valid && checker.VerifyViewId(data.TransactionId, ref raw);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                //Neu ko phai duoc uy quyen thi tai khoan phai lam viec tai phong thu ngan
                valid = valid && checker.IsCashierRoom(workPlace);
                valid = valid && cancelChecker.VerifyRequireField(data);
                valid = valid && cancelChecker.IsAllow(data, raw);
                
                if (valid)
                {
                    string RejectCancelReqReason = data.RejectCancelReqReason;
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    string username = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    long time = (Inventec.Common.DateTime.Get.Now() ?? 0);

                    StringBuilder query = new StringBuilder("UPDATE HIS_TRANSACTION SET ");
                    List<object> listParam = new List<object>();
                    query.Append(" CANCEL_REQ_STT = :param1");
                    listParam.Add(IMSys.DbConfig.HIS_RS.CANCEL_REQ_STT.ID__REJECT_CANCEL_REQ);

                    query.Append(", CANCEL_REQ_REJECT_LOGINNAME = :param2");
                    listParam.Add(loginname);

                    query.Append(", CANCEL_REQ_REJECT_USERNAME = :param3");
                    listParam.Add(username);

                    query.Append(", CANCEL_REQ_REJECT_REASON = :param4");
                    listParam.Add(RejectCancelReqReason);

                    query.Append(", CANCEL_REQ_REJECT_TIME = :param5");
                    listParam.Add(time);

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
                    new EventLogGenerator(EventLog.Enum.HisTransaction_TuChoiYeuCauHuyGiaoDich, eventLog)
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
                List<string> logs = new List<string>();
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LyDoYeuCauHuy), data.CANCEL_REQ_REASON));
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LyDoTuChoiYeuCauHuy), data.CANCEL_REQ_REJECT_REASON));
                eventLog = String.Join(". ", logs);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
      
    }
}
