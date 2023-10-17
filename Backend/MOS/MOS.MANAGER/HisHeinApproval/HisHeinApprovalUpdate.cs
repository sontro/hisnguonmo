using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisHeinApproval
{
    partial class HisHeinApprovalUpdate : BusinessBase
    {
		private HIS_HEIN_APPROVAL beforeUpdateHisHeinApprovalDTO;
		private List<HIS_HEIN_APPROVAL> beforeUpdateHisHeinApprovalDTOs;
		
        internal HisHeinApprovalUpdate()
            : base()
        {

        }

        internal HisHeinApprovalUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        private bool Update(HIS_HEIN_APPROVAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHeinApprovalCheck checker = new HisHeinApprovalCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_HEIN_APPROVAL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
					this.beforeUpdateHisHeinApprovalDTO = raw;
                    if (!DAOWorker.HisHeinApprovalDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHeinApproval_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHeinApproval that bai." + LogUtil.TraceData("data", data));
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

        internal bool Update(HIS_HEIN_APPROVAL data, HIS_HEIN_APPROVAL beforeUpdate)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHeinApprovalCheck checker = new HisHeinApprovalCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(beforeUpdate);
                if (valid)
                {
                    this.beforeUpdateHisHeinApprovalDTO = beforeUpdate;
                    if (!DAOWorker.HisHeinApprovalDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHeinApproval_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHeinApproval that bai." + LogUtil.TraceData("data", data));
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

        private bool UpdateList(List<HIS_HEIN_APPROVAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHeinApprovalCheck checker = new HisHeinApprovalCheck(param);
                List<HIS_HEIN_APPROVAL> listRaw = new List<HIS_HEIN_APPROVAL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisHeinApprovalDTOs = listRaw;
                    if (!DAOWorker.HisHeinApprovalDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHeinApproval_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHeinApproval that bai." + LogUtil.TraceData("listData", listData));
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
            if (this.beforeUpdateHisHeinApprovalDTO != null)
            {
                if (!new HisHeinApprovalUpdate(param).Update(this.beforeUpdateHisHeinApprovalDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisHeinApproval that bai, can kiem tra lai." + LogUtil.TraceData("HisHeinApprovalDTO", this.beforeUpdateHisHeinApprovalDTO));
                }
            }
			
			if (this.beforeUpdateHisHeinApprovalDTOs != null)
            {
                if (!new HisHeinApprovalUpdate(param).UpdateList(this.beforeUpdateHisHeinApprovalDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisHeinApproval that bai, can kiem tra lai." + LogUtil.TraceData("HisHeinApprovalDTOs", this.beforeUpdateHisHeinApprovalDTOs));
                }
            }
        }
    }
}
