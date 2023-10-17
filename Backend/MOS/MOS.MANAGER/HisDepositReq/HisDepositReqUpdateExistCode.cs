using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisTransReq;
using MOS.MANAGER.HisTransReq.CreateByDepositReq;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDepositReq
{
	partial class HisDepositReqUpdate : BusinessBase
	{
		private List<HIS_DEPOSIT_REQ> beforeUpdateHisDepositReqs = new List<HIS_DEPOSIT_REQ>();
		
		internal HisDepositReqUpdate()
			: base()
		{

		}

		internal HisDepositReqUpdate(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Update(HIS_DEPOSIT_REQ data)
		{
            return this.Update(data, true);
		}

        internal bool Update(HIS_DEPOSIT_REQ data, bool isUpdateRequestInfo)
        {
            bool result = false;
            try
            {
                WorkPlaceSDO workPlace = null;
                if (isUpdateRequestInfo)
                {
                    workPlace = TokenManager.GetWorkPlace(data.REQUEST_ROOM_ID);
                    if (workPlace == null)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                        throw new Exception("Khong co thong tin workplace.");
                    }
                    data.REQUEST_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    data.REQUEST_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    data.REQUEST_ROOM_ID = workPlace.RoomId;
                    data.REQUEST_DEPARTMENT_ID = workPlace.DepartmentId;
                }
                else
                {
                    V_HIS_ROOM vRoom = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == data.REQUEST_ROOM_ID);
                    workPlace = new WorkPlaceSDO() { RoomId = vRoom.ID, BranchId = vRoom.BRANCH_ID, DepartmentId = vRoom.DEPARTMENT_ID };
                }
                
                bool valid = true;
                bool isWriteEventLogHisTransReqCreate = false;
                HisDepositReqCheck checker = new HisDepositReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DEPOSIT_REQ raw = null;
                HIS_TRANS_REQ transReq = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.HasNoDeposit(raw);
                valid = valid && checker.ExistsCode(data.DEPOSIT_REQ_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisDepositReqs.Add(raw);
                    if (HisTransReqCFG.AUTO_CREATE_OPTION)
                    {
                        if (raw.AMOUNT != data.AMOUNT)
                        {
                            if (new HisTransReqCreateByDepositReqProccesor(param).Run(data.TREATMENT_ID, data, workPlace, ref transReq))
                            {
                                isWriteEventLogHisTransReqCreate = true;
                            }
                            else
                            {
                                Inventec.Common.Logging.LogSystem.Error("Tao HisTransReq that bai");
                                Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData("tranReqParam", param));
                            }
                            if (transReq != null)
                            {
                                data.TRANS_REQ_ID = transReq.ID;
                            }
                        }
                        else
                        {
                            data.TRANS_REQ_ID = raw.TRANS_REQ_ID;
                        }
                        
                    }
                    if (!DAOWorker.HisDepositReqDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDepositReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDepositReq that bai." + LogUtil.TraceData("data", data));
                    }
                    if (isWriteEventLogHisTransReqCreate) this.WriteEventLog(data, transReq);

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

		internal bool UpdateList(List<HIS_DEPOSIT_REQ> listData)
		{
			bool result = false;
			try
			{
				bool valid = true;
				valid = IsNotNullOrEmpty(listData);
				HisDepositReqCheck checker = new HisDepositReqCheck(param);
				List<HIS_DEPOSIT_REQ> listRaw = new List<HIS_DEPOSIT_REQ>();
				List<long> listId = listData.Select(o => o.ID).ToList();
				valid = valid && checker.VerifyIds(listId, listRaw);
				valid = valid && checker.IsUnLock(listRaw);
                valid = valid && checker.HasNoDeposit(listRaw);
				foreach (var data in listData)
				{
					valid = valid && checker.VerifyRequireField(data);
					valid = valid && checker.ExistsCode(data.DEPOSIT_REQ_CODE, data.ID);
				}
				if (valid)
				{
					this.beforeUpdateHisDepositReqs.AddRange(listRaw);
					if (!DAOWorker.HisDepositReqDAO.UpdateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDepositReq_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisDepositReq that bai." + LogUtil.TraceData("listData", listData));
					}
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
		
		internal void RollbackData()
		{
			if (IsNotNullOrEmpty(this.beforeUpdateHisDepositReqs))
			{
				if (!new HisDepositReqUpdate(param).UpdateList(this.beforeUpdateHisDepositReqs))
				{
					LogSystem.Warn("Rollback du lieu HisDepositReq that bai, can kiem tra lai." + LogUtil.TraceData("HisDepositReqs", this.beforeUpdateHisDepositReqs));
				}
			}
		}
	}
}
