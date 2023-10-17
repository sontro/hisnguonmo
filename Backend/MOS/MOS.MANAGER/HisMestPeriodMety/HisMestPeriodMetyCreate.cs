using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodMety
{
    class HisMestPeriodMetyCreate : BusinessBase
    {
        private List<HIS_MEST_PERIOD_METY> recentHisMestPeriodMetyDTOs = new List<HIS_MEST_PERIOD_METY>();
		
        internal HisMestPeriodMetyCreate()
            : base()
        {

        }

        internal HisMestPeriodMetyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEST_PERIOD_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPeriodMetyCheck checker = new HisMestPeriodMetyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisMestPeriodMetyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPeriodMety_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestPeriodMety that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMestPeriodMetyDTOs.Add(data);
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

        internal bool CreateList(List<HIS_MEST_PERIOD_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPeriodMetyCheck checker = new HisMestPeriodMetyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMestPeriodMetyDAO.CreateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPeriodMety_ThemMoiThatBai);
							throw new Exception("Them moi thong tin HisMestPeriodMety that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMestPeriodMetyDTOs.AddRange(listData);
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
			if (IsNotNullOrEmpty(this.recentHisMestPeriodMetyDTOs))
            {
                if (!new HisMestPeriodMetyTruncate(param).TruncateList(this.recentHisMestPeriodMetyDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisMestPeriodMety that bai, can kiem tra lai." + LogUtil.TraceData("HisMestPeriodMetyDTOs", this.recentHisMestPeriodMetyDTOs));
                }
            }
        }
    }
}
