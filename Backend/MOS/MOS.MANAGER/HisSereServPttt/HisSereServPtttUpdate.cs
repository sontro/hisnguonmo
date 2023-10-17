using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServPttt
{
	partial class HisSereServPtttUpdate : BusinessBase
	{
		private List<HIS_SERE_SERV_PTTT> beforeUpdateHisSereServPttts = new List<HIS_SERE_SERV_PTTT>();
		
		internal HisSereServPtttUpdate()
			: base()
		{

		}

		internal HisSereServPtttUpdate(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Update(HIS_SERE_SERV_PTTT data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisSereServPtttCheck checker = new HisSereServPtttCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				HIS_SERE_SERV_PTTT raw = null;
				valid = valid && checker.VerifyId(data.ID, ref raw);
				valid = valid && checker.IsUnLock(raw);
				if (valid)
				{
					this.beforeUpdateHisSereServPttts.Add(raw);
					if (!DAOWorker.HisSereServPtttDAO.Update(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServPttt_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisSereServPttt that bai." + LogUtil.TraceData("data", data));
					}
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

		internal bool UpdateList(List<HIS_SERE_SERV_PTTT> listData)
		{
			bool result = false;
			try
			{
				bool valid = true;
				valid = IsNotNullOrEmpty(listData);
				HisSereServPtttCheck checker = new HisSereServPtttCheck(param);
				List<HIS_SERE_SERV_PTTT> listRaw = new List<HIS_SERE_SERV_PTTT>();
				List<long> listId = listData.Select(o => o.ID).ToList();
				valid = valid && checker.VerifyIds(listId, listRaw);
				valid = valid && checker.IsUnLock(listRaw);
				foreach (var data in listData)
				{
					valid = valid && checker.VerifyRequireField(data);
				}
				if (valid)
				{
					this.beforeUpdateHisSereServPttts.AddRange(listRaw);
					if (!DAOWorker.HisSereServPtttDAO.UpdateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServPttt_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisSereServPttt that bai." + LogUtil.TraceData("listData", listData));
					}
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

        internal bool UpdateList(List<HIS_SERE_SERV_PTTT> listData,List<HIS_SERE_SERV_PTTT> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServPtttCheck checker = new HisSereServPtttCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSereServPtttDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServPttt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServPttt that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisSereServPttts.AddRange(listBefore);
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
			if (IsNotNullOrEmpty(this.beforeUpdateHisSereServPttts))
			{
				if (!DAOWorker.HisSereServPtttDAO.UpdateList(this.beforeUpdateHisSereServPttts))
				{
					LogSystem.Warn("Rollback du lieu HisSereServPttt that bai, can kiem tra lai." + LogUtil.TraceData("beforeUpdateHisSereServPttts", this.beforeUpdateHisSereServPttts));
				}
			}
		}
	}
}
