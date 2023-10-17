using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccountBook
{
    partial class HisAccountBookCreate : BusinessBase
    {
        private List<HIS_ACCOUNT_BOOK> recentHisAccountBooks = new List<HIS_ACCOUNT_BOOK>();

        internal HisAccountBookCreate()
            : base()
        {

        }

        internal HisAccountBookCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ACCOUNT_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccountBookCheck checker = new HisAccountBookCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyBookType(data);
                valid = valid && checker.ExistsCode(data.ACCOUNT_BOOK_CODE, null);
                if (valid)
                {
                    this.ProcessUserAccountBook(data, Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName());
                    if (!DAOWorker.HisAccountBookDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccountBook_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAccountBook that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAccountBooks.Add(data);
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

        internal bool CreateList(List<HIS_ACCOUNT_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAccountBookCheck checker = new HisAccountBookCheck(param);

                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ACCOUNT_BOOK_CODE, null);
                }

                if (valid)
                {
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    listData.ForEach(o => this.ProcessUserAccountBook(o, loginname));
                    if (!DAOWorker.HisAccountBookDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccountBook_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAccountBook that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisAccountBooks.AddRange(listData);
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

        private void ProcessUserAccountBook(HIS_ACCOUNT_BOOK accountBook, string loginname)
        {
            if (accountBook != null)
            {
                HIS_USER_ACCOUNT_BOOK user = new HIS_USER_ACCOUNT_BOOK();
                user.LOGINNAME = loginname;
                user.CREATOR = loginname;
                List<HIS_USER_ACCOUNT_BOOK> users = new List<HIS_USER_ACCOUNT_BOOK>();
                users.Add(user);
                accountBook.HIS_USER_ACCOUNT_BOOK = users;
            }
        }

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentHisAccountBooks))
            {
                if (!DAOWorker.HisAccountBookDAO.TruncateList(this.recentHisAccountBooks))
                {
                    LogSystem.Warn("Rollback du lieu HisAccountBook that bai, can kiem tra lai." + LogUtil.TraceData("HisAccountBook", this.recentHisAccountBooks));
                }
                this.recentHisAccountBooks = null;
            }
        }
    }
}
