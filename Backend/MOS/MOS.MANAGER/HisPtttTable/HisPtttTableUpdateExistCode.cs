using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPtttTable
{
	partial class HisPtttTableUpdate : BusinessBase
	{
		private List<HIS_PTTT_TABLE> beforeUpdateHisPtttTables = new List<HIS_PTTT_TABLE>();
		
		internal HisPtttTableUpdate()
			: base()
		{

		}

		internal HisPtttTableUpdate(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Update(HIS_PTTT_TABLE data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisPtttTableCheck checker = new HisPtttTableCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				HIS_PTTT_TABLE raw = null;
				valid = valid && checker.VerifyId(data.ID, ref raw);
				valid = valid && checker.IsUnLock(raw);
				valid = valid && checker.ExistsCode(data.PTTT_TABLE_CODE, data.ID);
				if (valid)
				{
					if (!DAOWorker.HisPtttTableDAO.Update(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttTable_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisPtttTable that bai." + LogUtil.TraceData("data", data));
					}
					
					this.beforeUpdateHisPtttTables.Add(raw);
					
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

		internal bool UpdateList(List<HIS_PTTT_TABLE> listData)
		{
			bool result = false;
			try
			{
				bool valid = true;
				valid = IsNotNullOrEmpty(listData);
				HisPtttTableCheck checker = new HisPtttTableCheck(param);
				List<HIS_PTTT_TABLE> listRaw = new List<HIS_PTTT_TABLE>();
				List<long> listId = listData.Select(o => o.ID).ToList();
				valid = valid && checker.VerifyIds(listId, listRaw);
				valid = valid && checker.IsUnLock(listRaw);
				foreach (var data in listData)
				{
					valid = valid && checker.VerifyRequireField(data);
					valid = valid && checker.ExistsCode(data.PTTT_TABLE_CODE, data.ID);
				}
				if (valid)
				{
					if (!DAOWorker.HisPtttTableDAO.UpdateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttTable_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisPtttTable that bai." + LogUtil.TraceData("listData", listData));
					}
					this.beforeUpdateHisPtttTables.AddRange(listRaw);
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
			if (IsNotNullOrEmpty(this.beforeUpdateHisPtttTables))
			{
				if (!DAOWorker.HisPtttTableDAO.UpdateList(this.beforeUpdateHisPtttTables))
				{
					LogSystem.Warn("Rollback du lieu HisPtttTable that bai, can kiem tra lai." + LogUtil.TraceData("HisPtttTables", this.beforeUpdateHisPtttTables));
				}
				this.beforeUpdateHisPtttTables = null;
			}
		}
	}
}
