using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHtu
{
	partial class HisHtuCreate : BusinessBase
	{
		private List<HIS_HTU> recentHisHtus = new List<HIS_HTU>();
		
		internal HisHtuCreate()
			: base()
		{

		}

		internal HisHtuCreate(CommonParam paramCreate)
			: base(paramCreate)
		{

		}

		internal bool Create(HIS_HTU data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisHtuCheck checker = new HisHtuCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				if (valid)
				{
					if (!DAOWorker.HisHtuDAO.Create(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHtu_ThemMoiThatBai);
						throw new Exception("Them moi thong tin HisHtu that bai." + LogUtil.TraceData("data", data));
					}
					this.recentHisHtus.Add(data);
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
		
		internal bool CreateList(List<HIS_HTU> listData)
		{
			bool result = false;
			try
			{
				bool valid = true;
				valid = IsNotNullOrEmpty(listData);
				HisHtuCheck checker = new HisHtuCheck(param);
				foreach (var data in listData)
				{
					valid = valid && checker.VerifyRequireField(data);
				}
				if (valid)
				{
					if (!DAOWorker.HisHtuDAO.CreateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHtu_ThemMoiThatBai);
						throw new Exception("Them moi thong tin HisHtu that bai." + LogUtil.TraceData("listData", listData));
					}
					this.recentHisHtus.AddRange(listData);
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
			if (IsNotNullOrEmpty(this.recentHisHtus))
			{
				if (!new HisHtuTruncate(param).TruncateList(this.recentHisHtus))
				{
					LogSystem.Warn("Rollback du lieu HisHtu that bai, can kiem tra lai." + LogUtil.TraceData("recentHisHtus", this.recentHisHtus));
				}
			}
		}
	}
}
