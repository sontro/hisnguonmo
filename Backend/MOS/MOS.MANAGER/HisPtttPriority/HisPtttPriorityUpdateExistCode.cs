using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPtttPriority
{
	partial class HisPtttPriorityUpdate : BusinessBase
	{
		private List<HIS_PTTT_PRIORITY> beforeUpdateHisPtttPrioritys = new List<HIS_PTTT_PRIORITY>();
		
		internal HisPtttPriorityUpdate()
			: base()
		{

		}

		internal HisPtttPriorityUpdate(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Update(HIS_PTTT_PRIORITY data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisPtttPriorityCheck checker = new HisPtttPriorityCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				HIS_PTTT_PRIORITY raw = null;
				valid = valid && checker.VerifyId(data.ID, ref raw);
				valid = valid && checker.IsUnLock(raw);
				valid = valid && checker.ExistsCode(data.PTTT_PRIORITY_CODE, data.ID);
				if (valid)
				{
					if (!DAOWorker.HisPtttPriorityDAO.Update(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttPriority_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisPtttPriority that bai." + LogUtil.TraceData("data", data));
					}
					
					this.beforeUpdateHisPtttPrioritys.Add(raw);
					
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

		internal bool UpdateList(List<HIS_PTTT_PRIORITY> listData)
		{
			bool result = false;
			try
			{
				bool valid = true;
				valid = IsNotNullOrEmpty(listData);
				HisPtttPriorityCheck checker = new HisPtttPriorityCheck(param);
				List<HIS_PTTT_PRIORITY> listRaw = new List<HIS_PTTT_PRIORITY>();
				List<long> listId = listData.Select(o => o.ID).ToList();
				valid = valid && checker.VerifyIds(listId, listRaw);
				valid = valid && checker.IsUnLock(listRaw);
				foreach (var data in listData)
				{
					valid = valid && checker.VerifyRequireField(data);
					valid = valid && checker.ExistsCode(data.PTTT_PRIORITY_CODE, data.ID);
				}
				if (valid)
				{
					if (!DAOWorker.HisPtttPriorityDAO.UpdateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttPriority_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisPtttPriority that bai." + LogUtil.TraceData("listData", listData));
					}
					this.beforeUpdateHisPtttPrioritys.AddRange(listRaw);
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
			if (IsNotNullOrEmpty(this.beforeUpdateHisPtttPrioritys))
			{
				if (!DAOWorker.HisPtttPriorityDAO.UpdateList(this.beforeUpdateHisPtttPrioritys))
				{
					LogSystem.Warn("Rollback du lieu HisPtttPriority that bai, can kiem tra lai." + LogUtil.TraceData("HisPtttPrioritys", this.beforeUpdateHisPtttPrioritys));
				}
				this.beforeUpdateHisPtttPrioritys = null;
			}
		}
	}
}
