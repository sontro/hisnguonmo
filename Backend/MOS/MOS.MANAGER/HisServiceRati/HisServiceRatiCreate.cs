using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceRati
{
	partial class HisServiceRatiCreate : BusinessBase
	{
		private List<HIS_SERVICE_RATI> recentHisServiceRatis = new List<HIS_SERVICE_RATI>();
		
		internal HisServiceRatiCreate()
			: base()
		{

		}

		internal HisServiceRatiCreate(CommonParam paramCreate)
			: base(paramCreate)
		{

		}

		internal bool Create(HIS_SERVICE_RATI data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisServiceRatiCheck checker = new HisServiceRatiCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				if (valid)
				{
					if (!DAOWorker.HisServiceRatiDAO.Create(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceRati_ThemMoiThatBai);
						throw new Exception("Them moi thong tin HisServiceRati that bai." + LogUtil.TraceData("data", data));
					}
					this.recentHisServiceRatis.Add(data);
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
		
		internal bool CreateList(List<HIS_SERVICE_RATI> listData)
		{
			bool result = false;
			try
			{
				bool valid = true;
				valid = IsNotNullOrEmpty(listData);
				HisServiceRatiCheck checker = new HisServiceRatiCheck(param);
				foreach (var data in listData)
				{
					valid = valid && checker.VerifyRequireField(data);
				}
				if (valid)
				{
					if (!DAOWorker.HisServiceRatiDAO.CreateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceRati_ThemMoiThatBai);
						throw new Exception("Them moi thong tin HisServiceRati that bai." + LogUtil.TraceData("listData", listData));
					}
					this.recentHisServiceRatis.AddRange(listData);
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
			if (IsNotNullOrEmpty(this.recentHisServiceRatis))
			{
				if (!DAOWorker.HisServiceRatiDAO.TruncateList(this.recentHisServiceRatis))
				{
					LogSystem.Warn("Rollback du lieu HisServiceRati that bai, can kiem tra lai." + LogUtil.TraceData("recentHisServiceRatis", this.recentHisServiceRatis));
				}
				this.recentHisServiceRatis = null;
			}
		}
	}
}
