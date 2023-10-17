using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisHoreHandover
{
    partial class HisHoreHandoverUpdate : BusinessBase
    {
		private List<HIS_HORE_HANDOVER> beforeUpdateHisHoreHandovers = new List<HIS_HORE_HANDOVER>();
		
        internal HisHoreHandoverUpdate()
            : base()
        {

        }

        internal HisHoreHandoverUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_HORE_HANDOVER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHoreHandoverCheck checker = new HisHoreHandoverCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_HORE_HANDOVER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisHoreHandoverDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoreHandover_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHoreHandover that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisHoreHandovers.Add(raw);
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

        internal bool Update(HIS_HORE_HANDOVER data, HIS_HORE_HANDOVER before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHoreHandoverCheck checker = new HisHoreHandoverCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(before);
                if (valid)
                {
                    if (!DAOWorker.HisHoreHandoverDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoreHandover_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHoreHandover that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisHoreHandovers.Add(before);
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

        internal bool UpdateList(List<HIS_HORE_HANDOVER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHoreHandoverCheck checker = new HisHoreHandoverCheck(param);
                List<HIS_HORE_HANDOVER> listRaw = new List<HIS_HORE_HANDOVER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisHoreHandoverDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoreHandover_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHoreHandover that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisHoreHandovers.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisHoreHandovers))
            {
                if (!DAOWorker.HisHoreHandoverDAO.UpdateList(this.beforeUpdateHisHoreHandovers))
                {
                    LogSystem.Warn("Rollback du lieu HisHoreHandover that bai, can kiem tra lai." + LogUtil.TraceData("HisHoreHandovers", this.beforeUpdateHisHoreHandovers));
                }
				this.beforeUpdateHisHoreHandovers = null;
            }
        }
    }
}
