using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisUserAccountBook
{
    partial class HisUserAccountBookUpdate : BusinessBase
    {
		private List<HIS_USER_ACCOUNT_BOOK> beforeUpdateHisUserAccountBooks = new List<HIS_USER_ACCOUNT_BOOK>();
		
        internal HisUserAccountBookUpdate()
            : base()
        {

        }

        internal HisUserAccountBookUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_USER_ACCOUNT_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUserAccountBookCheck checker = new HisUserAccountBookCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_USER_ACCOUNT_BOOK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisUserAccountBookDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUserAccountBook_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisUserAccountBook that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisUserAccountBooks.Add(raw);
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

        internal bool UpdateList(List<HIS_USER_ACCOUNT_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisUserAccountBookCheck checker = new HisUserAccountBookCheck(param);
                List<HIS_USER_ACCOUNT_BOOK> listRaw = new List<HIS_USER_ACCOUNT_BOOK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisUserAccountBookDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUserAccountBook_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisUserAccountBook that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisUserAccountBooks.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisUserAccountBooks))
            {
                if (!DAOWorker.HisUserAccountBookDAO.UpdateList(this.beforeUpdateHisUserAccountBooks))
                {
                    LogSystem.Warn("Rollback du lieu HisUserAccountBook that bai, can kiem tra lai." + LogUtil.TraceData("HisUserAccountBooks", this.beforeUpdateHisUserAccountBooks));
                }
				this.beforeUpdateHisUserAccountBooks = null;
            }
        }
    }
}
