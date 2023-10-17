using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDocumentBook
{
    partial class HisDocumentBookCreate : BusinessBase
    {
		private List<HIS_DOCUMENT_BOOK> recentHisDocumentBooks = new List<HIS_DOCUMENT_BOOK>();
		
        internal HisDocumentBookCreate()
            : base()
        {

        }

        internal HisDocumentBookCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DOCUMENT_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDocumentBookCheck checker = new HisDocumentBookCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.DOCUMENT_BOOK_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisDocumentBookDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDocumentBook_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDocumentBook that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDocumentBooks.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisDocumentBooks))
            {
                if (!DAOWorker.HisDocumentBookDAO.TruncateList(this.recentHisDocumentBooks))
                {
                    LogSystem.Warn("Rollback du lieu HisDocumentBook that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDocumentBooks", this.recentHisDocumentBooks));
                }
				this.recentHisDocumentBooks = null;
            }
        }
    }
}
