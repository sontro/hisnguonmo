using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCaroAccountBook
{
    partial class HisCaroAccountBookCreate : BusinessBase
    {
		private List<HIS_CARO_ACCOUNT_BOOK> recentHisCaroAccountBooks = new List<HIS_CARO_ACCOUNT_BOOK>();
		
        internal HisCaroAccountBookCreate()
            : base()
        {

        }

        internal HisCaroAccountBookCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CARO_ACCOUNT_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCaroAccountBookCheck checker = new HisCaroAccountBookCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisCaroAccountBookDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCaroAccountBook_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCaroAccountBook that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisCaroAccountBooks.Add(data);
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
		
		internal bool CreateList(List<HIS_CARO_ACCOUNT_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCaroAccountBookCheck checker = new HisCaroAccountBookCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisCaroAccountBookDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCaroAccountBook_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCaroAccountBook that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisCaroAccountBooks.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisCaroAccountBooks))
            {
                if (!DAOWorker.HisCaroAccountBookDAO.TruncateList(this.recentHisCaroAccountBooks))
                {
                    LogSystem.Warn("Rollback du lieu HisCaroAccountBook that bai, can kiem tra lai." + LogUtil.TraceData("recentHisCaroAccountBooks", this.recentHisCaroAccountBooks));
                }
				this.recentHisCaroAccountBooks = null;
            }
        }
    }
}
