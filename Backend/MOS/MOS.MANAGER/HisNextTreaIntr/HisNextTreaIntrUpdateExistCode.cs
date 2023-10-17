using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisNextTreaIntr
{
    partial class HisNextTreaIntrUpdate : BusinessBase
    {
		private List<HIS_NEXT_TREA_INTR> beforeUpdateHisNextTreaIntrs = new List<HIS_NEXT_TREA_INTR>();
		
        internal HisNextTreaIntrUpdate()
            : base()
        {

        }

        internal HisNextTreaIntrUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_NEXT_TREA_INTR data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisNextTreaIntrCheck checker = new HisNextTreaIntrCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_NEXT_TREA_INTR raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.NEXT_TREA_INTR_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisNextTreaIntrDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisNextTreaIntr_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisNextTreaIntr that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisNextTreaIntrs.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_NEXT_TREA_INTR> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisNextTreaIntrCheck checker = new HisNextTreaIntrCheck(param);
                List<HIS_NEXT_TREA_INTR> listRaw = new List<HIS_NEXT_TREA_INTR>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.NEXT_TREA_INTR_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisNextTreaIntrDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisNextTreaIntr_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisNextTreaIntr that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisNextTreaIntrs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisNextTreaIntrs))
            {
                if (!DAOWorker.HisNextTreaIntrDAO.UpdateList(this.beforeUpdateHisNextTreaIntrs))
                {
                    LogSystem.Warn("Rollback du lieu HisNextTreaIntr that bai, can kiem tra lai." + LogUtil.TraceData("HisNextTreaIntrs", this.beforeUpdateHisNextTreaIntrs));
                }
				this.beforeUpdateHisNextTreaIntrs = null;
            }
        }
    }
}
