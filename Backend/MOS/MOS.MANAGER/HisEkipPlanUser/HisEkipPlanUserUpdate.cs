using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEkipPlanUser
{
	partial class HisEkipPlanUserUpdate : BusinessBase
	{
		private List<HIS_EKIP_PLAN_USER> beforeUpdateHisEkipPlanUsers = new List<HIS_EKIP_PLAN_USER>();
		
		internal HisEkipPlanUserUpdate()
			: base()
		{

		}

		internal HisEkipPlanUserUpdate(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Update(HIS_EKIP_PLAN_USER data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisEkipPlanUserCheck checker = new HisEkipPlanUserCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				HIS_EKIP_PLAN_USER raw = null;
				valid = valid && checker.VerifyId(data.ID, ref raw);
				valid = valid && checker.IsUnLock(raw);
				if (valid)
				{                    
					if (!DAOWorker.HisEkipPlanUserDAO.Update(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkipPlanUser_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisEkipPlanUser that bai." + LogUtil.TraceData("data", data));
					}
					this.beforeUpdateHisEkipPlanUsers.Add(raw);
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

		internal bool UpdateList(List<HIS_EKIP_PLAN_USER> listData)
		{
			bool result = false;
			try
			{
				bool valid = true;
				valid = IsNotNullOrEmpty(listData);
				HisEkipPlanUserCheck checker = new HisEkipPlanUserCheck(param);
				List<HIS_EKIP_PLAN_USER> listRaw = new List<HIS_EKIP_PLAN_USER>();
				List<long> listId = listData.Select(o => o.ID).ToList();
				valid = valid && checker.VerifyIds(listId, listRaw);
				valid = valid && checker.IsUnLock(listRaw);
				foreach (var data in listData)
				{
					valid = valid && checker.VerifyRequireField(data);
				}
				if (valid)
				{
					if (!DAOWorker.HisEkipPlanUserDAO.UpdateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkipPlanUser_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisEkipPlanUser that bai." + LogUtil.TraceData("listData", listData));
					}
					
					this.beforeUpdateHisEkipPlanUsers.AddRange(listRaw);
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
			if (IsNotNullOrEmpty(this.beforeUpdateHisEkipPlanUsers))
			{
				if (!DAOWorker.HisEkipPlanUserDAO.UpdateList(this.beforeUpdateHisEkipPlanUsers))
				{
					LogSystem.Warn("Rollback du lieu HisEkipPlanUser that bai, can kiem tra lai." + LogUtil.TraceData("HisEkipPlanUsers", this.beforeUpdateHisEkipPlanUsers));
				}
				this.beforeUpdateHisEkipPlanUsers = null;
			}
		}
	}
}
