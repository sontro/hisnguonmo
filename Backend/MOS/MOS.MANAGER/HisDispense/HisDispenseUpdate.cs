using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDispense
{
    partial class HisDispenseUpdate : BusinessBase
    {
		private List<HIS_DISPENSE> beforeUpdateHisDispenses = new List<HIS_DISPENSE>();
		
        internal HisDispenseUpdate()
            : base()
        {

        }

        internal HisDispenseUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DISPENSE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDispenseCheck checker = new HisDispenseCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DISPENSE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisDispenseDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDispense_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDispense that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisDispenses.Add(raw);
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

        internal bool UpdateList(List<HIS_DISPENSE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDispenseCheck checker = new HisDispenseCheck(param);
                List<HIS_DISPENSE> listRaw = new List<HIS_DISPENSE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisDispenseDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDispense_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDispense that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisDispenses.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisDispenses))
            {
                if (!DAOWorker.HisDispenseDAO.UpdateList(this.beforeUpdateHisDispenses))
                {
                    LogSystem.Warn("Rollback du lieu HisDispense that bai, can kiem tra lai." + LogUtil.TraceData("HisDispenses", this.beforeUpdateHisDispenses));
                }
				this.beforeUpdateHisDispenses = null;
            }
        }
    }
}
