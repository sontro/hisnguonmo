using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialPaty
{
	class HisMaterialPatyCreate : BusinessBase
	{
		private List<HIS_MATERIAL_PATY> recentHisMaterialPatyDTOs;
		
		internal HisMaterialPatyCreate()
			: base()
		{

		}

		internal HisMaterialPatyCreate(CommonParam paramCreate)
			: base(paramCreate)
		{

		}

		internal bool Create(HIS_MATERIAL_PATY data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisMaterialPatyCheck checker = new HisMaterialPatyCheck(param);
				valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsNotExists(data);
				if (valid)
				{
					if (!DAOWorker.HisMaterialPatyDAO.Create(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterialPaty_ThemMoiThatBai);
						throw new Exception("Them moi thong tin HisMaterialPaty that bai." + LogUtil.TraceData("data", data));
					}
					if (this.recentHisMaterialPatyDTOs == null)
					{
						this.recentHisMaterialPatyDTOs = new List<HIS_MATERIAL_PATY>();
					}
					this.recentHisMaterialPatyDTOs.Add(data);
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

		internal bool CreateList(List<HIS_MATERIAL_PATY> listData)
		{
			bool result = false;
			try
			{
				bool valid = true;
				valid = IsNotNullOrEmpty(listData);
				HisMaterialPatyCheck checker = new HisMaterialPatyCheck(param);
				foreach (var data in listData)
				{
					valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsNotExists(data);
				}
				if (valid)
				{
					if (!DAOWorker.HisMaterialPatyDAO.CreateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterialPaty_ThemMoiThatBai);
							throw new Exception("Them moi thong tin HisMaterialPaty that bai." + LogUtil.TraceData("listData", listData));
					}
					if (this.recentHisMaterialPatyDTOs == null)
					{
						this.recentHisMaterialPatyDTOs = new List<HIS_MATERIAL_PATY>();
					}
                    this.recentHisMaterialPatyDTOs.AddRange(listData);
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
			if (this.recentHisMaterialPatyDTOs != null)
			{
				if (!new HisMaterialPatyTruncate(param).TruncateList(this.recentHisMaterialPatyDTOs))
				{
					LogSystem.Warn("Rollback du lieu HisMaterialPaty that bai, can kiem tra lai." + LogUtil.TraceData("HisMaterialPatyDTOs", this.recentHisMaterialPatyDTOs));
				}
			}
		}
	}
}
