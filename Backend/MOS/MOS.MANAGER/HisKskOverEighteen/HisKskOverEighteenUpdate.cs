using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskOverEighteen
{
    partial class HisKskOverEighteenUpdate : BusinessBase
    {
		private List<HIS_KSK_OVER_EIGHTEEN> beforeUpdateHisKskOverEighteens = new List<HIS_KSK_OVER_EIGHTEEN>();
		
        internal HisKskOverEighteenUpdate()
            : base()
        {

        }

        internal HisKskOverEighteenUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_KSK_OVER_EIGHTEEN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskOverEighteenCheck checker = new HisKskOverEighteenCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_KSK_OVER_EIGHTEEN raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisKskOverEighteenDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskOverEighteen_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskOverEighteen that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisKskOverEighteens.Add(raw);
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

        internal bool UpdateList(List<HIS_KSK_OVER_EIGHTEEN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskOverEighteenCheck checker = new HisKskOverEighteenCheck(param);
                List<HIS_KSK_OVER_EIGHTEEN> listRaw = new List<HIS_KSK_OVER_EIGHTEEN>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisKskOverEighteenDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskOverEighteen_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskOverEighteen that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisKskOverEighteens.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisKskOverEighteens))
            {
                if (!DAOWorker.HisKskOverEighteenDAO.UpdateList(this.beforeUpdateHisKskOverEighteens))
                {
                    LogSystem.Warn("Rollback du lieu HisKskOverEighteen that bai, can kiem tra lai." + LogUtil.TraceData("HisKskOverEighteens", this.beforeUpdateHisKskOverEighteens));
                }
				this.beforeUpdateHisKskOverEighteens = null;
            }
        }
    }
}
