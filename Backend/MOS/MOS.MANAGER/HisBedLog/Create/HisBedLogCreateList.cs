using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedLog
{
    partial class HisBedLogCreateList : BusinessBase
    {
        private List<HIS_BED_LOG> recentHisBedLogs = new List<HIS_BED_LOG>();

        internal HisBedLogCreateList()
            : base()
        {

        }

        internal HisBedLogCreateList(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool CreateList(List<HIS_BED_LOG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBedLogCheck checker = new HisBedLogCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBedLogDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBedLog_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBedLog that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisBedLogs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisBedLogs))
            {
                if (!DAOWorker.HisBedLogDAO.TruncateList(this.recentHisBedLogs))
                {
                    LogSystem.Warn("Rollback du lieu HisBedLog that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBedLogs", this.recentHisBedLogs));
                }
            }
        }
    }
}
