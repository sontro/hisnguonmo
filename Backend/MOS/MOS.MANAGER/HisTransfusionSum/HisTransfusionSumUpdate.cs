using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTransfusionSum
{
	partial class HisTransfusionSumUpdate : BusinessBase
	{
		private List<HIS_TRANSFUSION_SUM> beforeUpdateHisTransfusionSums = new List<HIS_TRANSFUSION_SUM>();
		
		internal HisTransfusionSumUpdate()
			: base()
		{

		}

		internal HisTransfusionSumUpdate(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Update(HIS_TRANSFUSION_SUM data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisTransfusionSumCheck checker = new HisTransfusionSumCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				HIS_TRANSFUSION_SUM raw = null;
				valid = valid && checker.VerifyId(data.ID, ref raw);
				valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.VefifyTreatment(data);
				if (valid)
				{                    
					if (!DAOWorker.HisTransfusionSumDAO.Update(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransfusionSum_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisTransfusionSum that bai." + LogUtil.TraceData("data", data));
					}
					this.beforeUpdateHisTransfusionSums.Add(raw);
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

		internal bool UpdateList(List<HIS_TRANSFUSION_SUM> listData)
		{
			bool result = false;
			try
			{
				bool valid = true;
				valid = IsNotNullOrEmpty(listData);
				HisTransfusionSumCheck checker = new HisTransfusionSumCheck(param);
				List<HIS_TRANSFUSION_SUM> listRaw = new List<HIS_TRANSFUSION_SUM>();
				List<long> listId = listData.Select(o => o.ID).ToList();
				valid = valid && checker.VerifyIds(listId, listRaw);
				valid = valid && checker.IsUnLock(listRaw);
				foreach (var data in listData)
				{
					valid = valid && checker.VerifyRequireField(data);
				}
				if (valid)
				{
					if (!DAOWorker.HisTransfusionSumDAO.UpdateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransfusionSum_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisTransfusionSum that bai." + LogUtil.TraceData("listData", listData));
					}
					
					this.beforeUpdateHisTransfusionSums.AddRange(listRaw);
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
			if (IsNotNullOrEmpty(this.beforeUpdateHisTransfusionSums))
			{
				if (!DAOWorker.HisTransfusionSumDAO.UpdateList(this.beforeUpdateHisTransfusionSums))
				{
					LogSystem.Warn("Rollback du lieu HisTransfusionSum that bai, can kiem tra lai." + LogUtil.TraceData("HisTransfusionSums", this.beforeUpdateHisTransfusionSums));
				}
				this.beforeUpdateHisTransfusionSums = null;
			}
		}
	}
}
