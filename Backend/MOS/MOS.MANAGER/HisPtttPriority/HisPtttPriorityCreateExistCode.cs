using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttPriority
{
	partial class HisPtttPriorityCreate : BusinessBase
	{
		private List<HIS_PTTT_PRIORITY> recentHisPtttPrioritys = new List<HIS_PTTT_PRIORITY>();
		
		internal HisPtttPriorityCreate()
			: base()
		{

		}

		internal HisPtttPriorityCreate(CommonParam paramCreate)
			: base(paramCreate)
		{

		}

		internal bool Create(HIS_PTTT_PRIORITY data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisPtttPriorityCheck checker = new HisPtttPriorityCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				valid = valid && checker.ExistsCode(data.PTTT_PRIORITY_CODE, null);
				if (valid)
				{
					if (!DAOWorker.HisPtttPriorityDAO.Create(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttPriority_ThemMoiThatBai);
						throw new Exception("Them moi thong tin HisPtttPriority that bai." + LogUtil.TraceData("data", data));
					}
					this.recentHisPtttPrioritys.Add(data);
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
			if (IsNotNullOrEmpty(this.recentHisPtttPrioritys))
			{
				if (!DAOWorker.HisPtttPriorityDAO.TruncateList(this.recentHisPtttPrioritys))
				{
					LogSystem.Warn("Rollback du lieu HisPtttPriority that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPtttPrioritys", this.recentHisPtttPrioritys));
				}
				this.recentHisPtttPrioritys = null;
			}
		}
	}
}
