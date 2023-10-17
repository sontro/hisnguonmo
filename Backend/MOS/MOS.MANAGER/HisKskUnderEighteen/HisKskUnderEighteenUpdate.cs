using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskUnderEighteen
{
    partial class HisKskUnderEighteenUpdate : BusinessBase
    {
		private List<HIS_KSK_UNDER_EIGHTEEN> beforeUpdateHisKskUnderEighteens = new List<HIS_KSK_UNDER_EIGHTEEN>();
		
        internal HisKskUnderEighteenUpdate()
            : base()
        {

        }

        internal HisKskUnderEighteenUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_KSK_UNDER_EIGHTEEN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskUnderEighteenCheck checker = new HisKskUnderEighteenCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_KSK_UNDER_EIGHTEEN raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisKskUnderEighteenDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskUnderEighteen_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskUnderEighteen that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisKskUnderEighteens.Add(raw);
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

        internal bool UpdateList(List<HIS_KSK_UNDER_EIGHTEEN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskUnderEighteenCheck checker = new HisKskUnderEighteenCheck(param);
                List<HIS_KSK_UNDER_EIGHTEEN> listRaw = new List<HIS_KSK_UNDER_EIGHTEEN>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisKskUnderEighteenDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskUnderEighteen_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskUnderEighteen that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisKskUnderEighteens.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisKskUnderEighteens))
            {
                if (!DAOWorker.HisKskUnderEighteenDAO.UpdateList(this.beforeUpdateHisKskUnderEighteens))
                {
                    LogSystem.Warn("Rollback du lieu HisKskUnderEighteen that bai, can kiem tra lai." + LogUtil.TraceData("HisKskUnderEighteens", this.beforeUpdateHisKskUnderEighteens));
                }
				this.beforeUpdateHisKskUnderEighteens = null;
            }
        }
    }
}
