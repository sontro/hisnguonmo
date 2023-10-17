using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestInveUser
{
    partial class HisMestInveUserCreate : BusinessBase
    {
        private List<HIS_MEST_INVE_USER> recentHisMestInveUsers = new List<HIS_MEST_INVE_USER>();

        internal HisMestInveUserCreate()
            : base()
        {

        }

        internal HisMestInveUserCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEST_INVE_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestInveUserCheck checker = new HisMestInveUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisMestInveUserDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestInveUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestInveUser that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMestInveUsers.Add(data);
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

        internal bool CreateList(List<HIS_MEST_INVE_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestInveUserCheck checker = new HisMestInveUserCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMestInveUserDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestInveUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestInveUser that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMestInveUsers.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMestInveUsers))
            {
                if (!new HisMestInveUserTruncate(param).TruncateList(this.recentHisMestInveUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisMestInveUser that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMestInveUsers", this.recentHisMestInveUsers));
                }
            }
        }
    }
}
