using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestBlood;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransfusionSum
{
	partial class HisTransfusionSumCreate : BusinessBase
	{
		private List<HIS_TRANSFUSION_SUM> recentHisTransfusionSums = new List<HIS_TRANSFUSION_SUM>();
		
		internal HisTransfusionSumCreate()
			: base()
		{

		}

		internal HisTransfusionSumCreate(CommonParam paramCreate)
			: base(paramCreate)
		{

		}

		internal bool Create(HIS_TRANSFUSION_SUM data)
		{
			bool result = false;
			try
			{
				bool valid = true;
                WorkPlaceSDO wp = null;
                HIS_EXP_MEST_BLOOD expMestBlood = null;
				HisTransfusionSumCheck checker = new HisTransfusionSumCheck(param);
                HisExpMestBloodCheck expBloodChecker = new HisExpMestBloodCheck(param);
				valid = valid && checker.VerifyRequireField(data);
                valid = valid && this.HasWorkPlaceInfo(data.ROOM_ID.Value, ref wp);
                valid = valid && expBloodChecker.VerifyId(data.EXP_MEST_BLOOD_ID, ref expMestBlood);
                valid = valid && expBloodChecker.IsExported(expMestBlood);
                valid = valid && expBloodChecker.IsUnLock(expMestBlood);
                valid = valid && checker.CheckTreatmentId(data, expMestBlood);
                valid = valid && checker.CheckExist(data);
                valid = valid && checker.VefifyTreatment(data);
				if (valid)
				{
                    data.DEPARTMENT_ID = wp.DepartmentId;
					if (!DAOWorker.HisTransfusionSumDAO.Create(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransfusionSum_ThemMoiThatBai);
						throw new Exception("Them moi thong tin HisTransfusionSum that bai." + LogUtil.TraceData("data", data));
					}
					this.recentHisTransfusionSums.Add(data);
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
		
		internal bool CreateList(List<HIS_TRANSFUSION_SUM> listData)
		{
			bool result = false;
			try
			{
				bool valid = true;
				valid = IsNotNullOrEmpty(listData);
				HisTransfusionSumCheck checker = new HisTransfusionSumCheck(param);
				foreach (var data in listData)
				{
					valid = valid && checker.VerifyRequireField(data);
				}
				if (valid)
				{
					if (!DAOWorker.HisTransfusionSumDAO.CreateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransfusionSum_ThemMoiThatBai);
						throw new Exception("Them moi thong tin HisTransfusionSum that bai." + LogUtil.TraceData("listData", listData));
					}
					this.recentHisTransfusionSums.AddRange(listData);
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
			if (IsNotNullOrEmpty(this.recentHisTransfusionSums))
			{
				if (!DAOWorker.HisTransfusionSumDAO.TruncateList(this.recentHisTransfusionSums))
				{
					LogSystem.Warn("Rollback du lieu HisTransfusionSum that bai, can kiem tra lai." + LogUtil.TraceData("recentHisTransfusionSums", this.recentHisTransfusionSums));
				}
				this.recentHisTransfusionSums = null;
			}
		}
	}
}
