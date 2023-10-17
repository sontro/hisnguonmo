using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSeseDepoRepay
{
    partial class HisSeseDepoRepayCreate : BusinessBase
    {
		private List<HIS_SESE_DEPO_REPAY> recentHisSeseDepoRepays = new List<HIS_SESE_DEPO_REPAY>();
		
        internal HisSeseDepoRepayCreate()
            : base()
        {

        }

        internal HisSeseDepoRepayCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SESE_DEPO_REPAY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSeseDepoRepayCheck checker = new HisSeseDepoRepayCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisSeseDepoRepayDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSeseDepoRepay_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSeseDepoRepay that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSeseDepoRepays.Add(data);
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
		
		internal bool CreateList(List<HIS_SESE_DEPO_REPAY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSeseDepoRepayCheck checker = new HisSeseDepoRepayCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSeseDepoRepayDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSeseDepoRepay_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSeseDepoRepay that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSeseDepoRepays.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisSeseDepoRepays))
            {
                if (!new HisSeseDepoRepayTruncate(param).TruncateList(this.recentHisSeseDepoRepays))
                {
                    LogSystem.Warn("Rollback du lieu HisSeseDepoRepay that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSeseDepoRepays", this.recentHisSeseDepoRepays));
                }
            }
        }
    }
}
