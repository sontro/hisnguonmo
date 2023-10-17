using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipPlan
{
	partial class HisEkipPlanCreate : BusinessBase
	{
		private List<HIS_EKIP_PLAN> recentHisEkipPlans = new List<HIS_EKIP_PLAN>();
		
		internal HisEkipPlanCreate()
			: base()
		{

		}

		internal HisEkipPlanCreate(CommonParam paramCreate)
			: base(paramCreate)
		{

		}

		internal bool Create(HIS_EKIP_PLAN data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisEkipPlanCheck checker = new HisEkipPlanCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				if (valid)
				{
					if (!DAOWorker.HisEkipPlanDAO.Create(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkipPlan_ThemMoiThatBai);
						throw new Exception("Them moi thong tin HisEkipPlan that bai." + LogUtil.TraceData("data", data));
					}
					this.recentHisEkipPlans.Add(data);
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
		
		internal bool CreateList(List<HIS_EKIP_PLAN> listData)
		{
			bool result = false;
			try
			{
				bool valid = true;
				valid = IsNotNullOrEmpty(listData);
				HisEkipPlanCheck checker = new HisEkipPlanCheck(param);
				foreach (var data in listData)
				{
					valid = valid && checker.VerifyRequireField(data);
				}
				if (valid)
				{
					if (!DAOWorker.HisEkipPlanDAO.CreateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkipPlan_ThemMoiThatBai);
						throw new Exception("Them moi thong tin HisEkipPlan that bai." + LogUtil.TraceData("listData", listData));
					}
					this.recentHisEkipPlans.AddRange(listData);
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
			if (IsNotNullOrEmpty(this.recentHisEkipPlans))
			{
				if (!DAOWorker.HisEkipPlanDAO.TruncateList(this.recentHisEkipPlans))
				{
					LogSystem.Warn("Rollback du lieu HisEkipPlan that bai, can kiem tra lai." + LogUtil.TraceData("recentHisEkipPlans", this.recentHisEkipPlans));
				}
				this.recentHisEkipPlans = null;
			}
		}
	}
}
