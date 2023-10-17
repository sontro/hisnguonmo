using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodMaty
{
    class HisMestPeriodMatyCreate : BusinessBase
    {
        private List<HIS_MEST_PERIOD_MATY> recentHisMestPeriodMatyDTOs = new List<HIS_MEST_PERIOD_MATY>();

        internal HisMestPeriodMatyCreate()
            : base()
        {

        }

        internal HisMestPeriodMatyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEST_PERIOD_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPeriodMatyCheck checker = new HisMestPeriodMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisMestPeriodMatyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPeriodMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestPeriodMaty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMestPeriodMatyDTOs.Add(data);
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

        internal bool CreateList(List<HIS_MEST_PERIOD_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPeriodMatyCheck checker = new HisMestPeriodMatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMestPeriodMatyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPeriodMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestPeriodMaty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMestPeriodMatyDTOs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMestPeriodMatyDTOs))
            {
                if (!new HisMestPeriodMatyTruncate(param).TruncateList(this.recentHisMestPeriodMatyDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisMestPeriodMaty that bai, can kiem tra lai." + LogUtil.TraceData("HisMestPeriodMatyDTOs", this.recentHisMestPeriodMatyDTOs));
                }
            }
        }
    }
}
