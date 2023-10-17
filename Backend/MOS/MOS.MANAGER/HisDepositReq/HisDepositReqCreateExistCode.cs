using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisTransReq.CreateByDepositReq;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDepositReq
{
    partial class HisDepositReqCreate : BusinessBase
    {
		private List<HIS_DEPOSIT_REQ> recentHisDepositReqs = new List<HIS_DEPOSIT_REQ>();
		
        internal HisDepositReqCreate()
            : base()
        {
           
        }

        internal HisDepositReqCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool Create(HIS_DEPOSIT_REQ data)
        {
            bool result = false;
            try
            {
                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.REQUEST_ROOM_ID);
                if (workPlace == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                    throw new Exception("Khong co thong tin workplace.");
                }

                data.REQUEST_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                data.REQUEST_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                data.REQUEST_ROOM_ID = workPlace.RoomId;
                data.REQUEST_DEPARTMENT_ID = workPlace.DepartmentId;
                
                bool valid = true;
                bool isWriteEventLogHisTransReqCreate = false;
                HIS_TRANS_REQ transReq = null;
                HisDepositReqCheck checker = new HisDepositReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.DEPOSIT_REQ_CODE, null);
                if (valid)
                {
                    if (HisTransReqCFG.AUTO_CREATE_OPTION)
                    {
                        if (new HisTransReqCreateByDepositReqProccesor(param).Run(data.TREATMENT_ID, data, workPlace, ref transReq))
                        {
                            isWriteEventLogHisTransReqCreate = true;
                        }else
                        {
                            Inventec.Common.Logging.LogSystem.Error("Tao HisTransReq that bai");
                            Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData("tranReqParam", param));
                        }
                        if (transReq != null)
                        {
                            data.TRANS_REQ_ID = transReq.ID;
                        }
                    }
					if (!DAOWorker.HisDepositReqDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDepositReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDepositReq that bai." + LogUtil.TraceData("data", data));
                    }
                    if (isWriteEventLogHisTransReqCreate) this.WriteEventLog(data, transReq);
                    this.recentHisDepositReqs.Add(data);
                    result = true;
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

        private void WriteEventLog(HIS_DEPOSIT_REQ data, HIS_TRANS_REQ transReq)
        {
            var treatment = new HisTreatmentGet().GetById(data.TREATMENT_ID);
            new EventLogGenerator(EventLog.Enum.HisTransReq_TaoYeuCauThanhToan,
                                    transReq.TRANS_REQ_CODE,
                                    MessageUtil.GetMessage(LibraryMessage.Message.Enum.TransReqType_YeuCauThanhToanTheoSoTienConThieuCuaHoSo, param.LanguageCode),
                                    transReq.AMOUNT,
                                    String.Format("Mã yêu cầu tạm ứng: {0}.", data.DEPOSIT_REQ_CODE)).TreatmentCode(treatment.TREATMENT_CODE).Run();
        }
		
		internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentHisDepositReqs))
            {
                if (!new HisDepositReqTruncate(param).TruncateList(this.recentHisDepositReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisDepositReq that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDepositReqs", this.recentHisDepositReqs));
                }
            }
        }
    }
}
