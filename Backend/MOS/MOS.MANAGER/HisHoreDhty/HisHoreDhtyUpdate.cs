using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisHoreDhty
{
    partial class HisHoreDhtyUpdate : BusinessBase
    {
		private List<HIS_HORE_DHTY> beforeUpdateHisHoreDhtys = new List<HIS_HORE_DHTY>();
		
        internal HisHoreDhtyUpdate()
            : base()
        {

        }

        internal HisHoreDhtyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_HORE_DHTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHoreDhtyCheck checker = new HisHoreDhtyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_HORE_DHTY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisHoreDhtyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoreDhty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHoreDhty that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisHoreDhtys.Add(raw);
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

        internal bool UpdateList(List<HIS_HORE_DHTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHoreDhtyCheck checker = new HisHoreDhtyCheck(param);
                List<HIS_HORE_DHTY> listRaw = new List<HIS_HORE_DHTY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisHoreDhtyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoreDhty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHoreDhty that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisHoreDhtys.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisHoreDhtys))
            {
                if (!DAOWorker.HisHoreDhtyDAO.UpdateList(this.beforeUpdateHisHoreDhtys))
                {
                    LogSystem.Warn("Rollback du lieu HisHoreDhty that bai, can kiem tra lai." + LogUtil.TraceData("HisHoreDhtys", this.beforeUpdateHisHoreDhtys));
                }
				this.beforeUpdateHisHoreDhtys = null;
            }
        }
    }
}
