using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPeriodDriverDity
{
    partial class HisPeriodDriverDityCreate : BusinessBase
    {
		private List<HIS_PERIOD_DRIVER_DITY> recentHisPeriodDriverDitys = new List<HIS_PERIOD_DRIVER_DITY>();
		
        internal HisPeriodDriverDityCreate()
            : base()
        {

        }

        internal HisPeriodDriverDityCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PERIOD_DRIVER_DITY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPeriodDriverDityCheck checker = new HisPeriodDriverDityCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisPeriodDriverDityDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPeriodDriverDity_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPeriodDriverDity that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPeriodDriverDitys.Add(data);
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
		
		internal bool CreateList(List<HIS_PERIOD_DRIVER_DITY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPeriodDriverDityCheck checker = new HisPeriodDriverDityCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisPeriodDriverDityDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPeriodDriverDity_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPeriodDriverDity that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisPeriodDriverDitys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisPeriodDriverDitys))
            {
                if (!DAOWorker.HisPeriodDriverDityDAO.TruncateList(this.recentHisPeriodDriverDitys))
                {
                    LogSystem.Warn("Rollback du lieu HisPeriodDriverDity that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPeriodDriverDitys", this.recentHisPeriodDriverDitys));
                }
				this.recentHisPeriodDriverDitys = null;
            }
        }
    }
}
