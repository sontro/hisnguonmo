using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserAccountBook
{
    partial class HisUserAccountBookCreate : BusinessBase
    {
		private List<HIS_USER_ACCOUNT_BOOK> recentHisUserAccountBooks = new List<HIS_USER_ACCOUNT_BOOK>();
		
        internal HisUserAccountBookCreate()
            : base()
        {

        }

        internal HisUserAccountBookCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_USER_ACCOUNT_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUserAccountBookCheck checker = new HisUserAccountBookCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisUserAccountBookDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUserAccountBook_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisUserAccountBook that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisUserAccountBooks.Add(data);
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
		
		internal bool CreateList(List<HIS_USER_ACCOUNT_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisUserAccountBookCheck checker = new HisUserAccountBookCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisUserAccountBookDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUserAccountBook_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisUserAccountBook that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisUserAccountBooks.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisUserAccountBooks))
            {
                if (!DAOWorker.HisUserAccountBookDAO.TruncateList(this.recentHisUserAccountBooks))
                {
                    LogSystem.Warn("Rollback du lieu HisUserAccountBook that bai, can kiem tra lai." + LogUtil.TraceData("recentHisUserAccountBooks", this.recentHisUserAccountBooks));
                }
				this.recentHisUserAccountBooks = null;
            }
        }
    }
}
