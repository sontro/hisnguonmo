using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBed
{
    partial class HisBedCreate : BusinessBase
    {
        private List<HIS_BED> recentHisBeds = new List<HIS_BED>();

        internal HisBedCreate()
            : base()
        {

        }

        internal HisBedCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BED data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBedCheck checker = new HisBedCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.CheckExists(data);
                if (valid)
                {
                    if (!DAOWorker.HisBedDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBed_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBed that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBeds.Add(data);
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

        internal bool CreateList(List<HIS_BED> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBedCheck checker = new HisBedCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                valid = valid && checker.CheckExists(listData);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBedDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBed_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBed that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisBeds.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisBeds))
            {
                if (!DAOWorker.HisBedDAO.TruncateList(this.recentHisBeds))
                {
                    LogSystem.Warn("Rollback du lieu HisBed that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBeds", this.recentHisBeds));
                }
            }
        }
    }
}
