using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodBlty
{
    partial class HisMestPeriodBltyCreate : BusinessBase
    {
		private List<HIS_MEST_PERIOD_BLTY> recentHisMestPeriodBltys = new List<HIS_MEST_PERIOD_BLTY>();
		
        internal HisMestPeriodBltyCreate()
            : base()
        {

        }

        internal HisMestPeriodBltyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEST_PERIOD_BLTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPeriodBltyCheck checker = new HisMestPeriodBltyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMestPeriodBltyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPeriodBlty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestPeriodBlty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMestPeriodBltys.Add(data);
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
		
		internal bool CreateList(List<HIS_MEST_PERIOD_BLTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPeriodBltyCheck checker = new HisMestPeriodBltyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMestPeriodBltyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPeriodBlty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestPeriodBlty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMestPeriodBltys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMestPeriodBltys))
            {
                if (!new HisMestPeriodBltyTruncate(param).TruncateList(this.recentHisMestPeriodBltys))
                {
                    LogSystem.Warn("Rollback du lieu HisMestPeriodBlty that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMestPeriodBltys", this.recentHisMestPeriodBltys));
                }
            }
        }
    }
}
