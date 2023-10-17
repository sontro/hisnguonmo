using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCaroAccountBook
{
    partial class HisCaroAccountBookUpdate : BusinessBase
    {
		private List<HIS_CARO_ACCOUNT_BOOK> beforeUpdateHisCaroAccountBooks = new List<HIS_CARO_ACCOUNT_BOOK>();
		
        internal HisCaroAccountBookUpdate()
            : base()
        {

        }

        internal HisCaroAccountBookUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CARO_ACCOUNT_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCaroAccountBookCheck checker = new HisCaroAccountBookCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CARO_ACCOUNT_BOOK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisCaroAccountBookDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCaroAccountBook_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCaroAccountBook that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisCaroAccountBooks.Add(raw);
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

        internal bool UpdateList(List<HIS_CARO_ACCOUNT_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCaroAccountBookCheck checker = new HisCaroAccountBookCheck(param);
                List<HIS_CARO_ACCOUNT_BOOK> listRaw = new List<HIS_CARO_ACCOUNT_BOOK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisCaroAccountBookDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCaroAccountBook_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCaroAccountBook that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisCaroAccountBooks.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisCaroAccountBooks))
            {
                if (!DAOWorker.HisCaroAccountBookDAO.UpdateList(this.beforeUpdateHisCaroAccountBooks))
                {
                    LogSystem.Warn("Rollback du lieu HisCaroAccountBook that bai, can kiem tra lai." + LogUtil.TraceData("HisCaroAccountBooks", this.beforeUpdateHisCaroAccountBooks));
                }
				this.beforeUpdateHisCaroAccountBooks = null;
            }
        }
    }
}
