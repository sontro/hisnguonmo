using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskPeriodDriver
{
    partial class HisKskPeriodDriverCreate : BusinessBase
    {
		private List<HIS_KSK_PERIOD_DRIVER> recentHisKskPeriodDrivers = new List<HIS_KSK_PERIOD_DRIVER>();
		
        internal HisKskPeriodDriverCreate()
            : base()
        {

        }

        internal HisKskPeriodDriverCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_KSK_PERIOD_DRIVER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskPeriodDriverCheck checker = new HisKskPeriodDriverCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisKskPeriodDriverDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskPeriodDriver_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskPeriodDriver that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisKskPeriodDrivers.Add(data);
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
		
		internal bool CreateList(List<HIS_KSK_PERIOD_DRIVER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskPeriodDriverCheck checker = new HisKskPeriodDriverCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisKskPeriodDriverDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskPeriodDriver_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskPeriodDriver that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisKskPeriodDrivers.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisKskPeriodDrivers))
            {
                if (!DAOWorker.HisKskPeriodDriverDAO.TruncateList(this.recentHisKskPeriodDrivers))
                {
                    LogSystem.Warn("Rollback du lieu HisKskPeriodDriver that bai, can kiem tra lai." + LogUtil.TraceData("recentHisKskPeriodDrivers", this.recentHisKskPeriodDrivers));
                }
				this.recentHisKskPeriodDrivers = null;
            }
        }
    }
}
