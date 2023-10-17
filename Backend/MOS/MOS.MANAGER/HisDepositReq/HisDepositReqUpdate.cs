using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
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
            bool result = false;
            try
            {
                bool valid = true;
                HisDepositReqCheck checker = new HisDepositReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DEPOSIT_REQ raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisDepositReqs.Add(raw);
					if (!DAOWorker.HisDepositReqDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDepositReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDepositReq that bai." + LogUtil.TraceData("data", data));
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
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
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
