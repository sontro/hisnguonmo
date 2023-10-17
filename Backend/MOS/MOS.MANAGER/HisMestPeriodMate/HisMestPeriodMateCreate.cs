using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodMate
{
    class HisMestPeriodMateCreate : BusinessBase
    {
        private List<HIS_MEST_PERIOD_MATE> recentHisMestPeriodMateDTOs = new List<HIS_MEST_PERIOD_MATE>();
		
        internal HisMestPeriodMateCreate()
            : base()
        {

        }

        internal HisMestPeriodMateCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEST_PERIOD_MATE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPeriodMateCheck checker = new HisMestPeriodMateCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisMestPeriodMateDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPeriodMate_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestPeriodMate that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMestPeriodMateDTOs.Add(data);
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

        internal bool CreateList(List<HIS_MEST_PERIOD_MATE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPeriodMateCheck checker = new HisMestPeriodMateCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMestPeriodMateDAO.CreateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPeriodMate_ThemMoiThatBai);
							throw new Exception("Them moi thong tin HisMestPeriodMate that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMestPeriodMateDTOs.AddRange(listData);
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
			if (IsNotNullOrEmpty(this.recentHisMestPeriodMateDTOs))
            {
                if (!new HisMestPeriodMateTruncate(param).TruncateList(this.recentHisMestPeriodMateDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisMestPeriodMate that bai, can kiem tra lai." + LogUtil.TraceData("HisMestPeriodMateDTOs", this.recentHisMestPeriodMateDTOs));
                }
            }
        }
    }
}
