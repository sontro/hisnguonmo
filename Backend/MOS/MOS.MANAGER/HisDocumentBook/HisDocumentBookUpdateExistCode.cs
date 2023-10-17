using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDocumentBook
{
    partial class HisDocumentBookUpdate : BusinessBase
    {
		private List<HIS_DOCUMENT_BOOK> beforeUpdateHisDocumentBooks = new List<HIS_DOCUMENT_BOOK>();
		
        internal HisDocumentBookUpdate()
            : base()
        {

        }

        internal HisDocumentBookUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DOCUMENT_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDocumentBookCheck checker = new HisDocumentBookCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DOCUMENT_BOOK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.DOCUMENT_BOOK_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisDocumentBookDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDocumentBook_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDocumentBook that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisDocumentBooks.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_DOCUMENT_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDocumentBookCheck checker = new HisDocumentBookCheck(param);
                List<HIS_DOCUMENT_BOOK> listRaw = new List<HIS_DOCUMENT_BOOK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.DOCUMENT_BOOK_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisDocumentBookDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDocumentBook_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDocumentBook that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisDocumentBooks.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisDocumentBooks))
            {
                if (!DAOWorker.HisDocumentBookDAO.UpdateList(this.beforeUpdateHisDocumentBooks))
                {
                    LogSystem.Warn("Rollback du lieu HisDocumentBook that bai, can kiem tra lai." + LogUtil.TraceData("HisDocumentBooks", this.beforeUpdateHisDocumentBooks));
                }
				this.beforeUpdateHisDocumentBooks = null;
            }
        }
    }
}
