using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodMedi
{
    class HisMestPeriodMediCreate : BusinessBase
    {
        private List<HIS_MEST_PERIOD_MEDI> recentHisMestPeriodMediDTOs = new List<HIS_MEST_PERIOD_MEDI>();
		
        internal HisMestPeriodMediCreate()
            : base()
        {

        }

        internal HisMestPeriodMediCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEST_PERIOD_MEDI data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPeriodMediCheck checker = new HisMestPeriodMediCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisMestPeriodMediDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPeriodMedi_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestPeriodMedi that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMestPeriodMediDTOs.Add(data);
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

        internal bool CreateList(List<HIS_MEST_PERIOD_MEDI> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestPeriodMediCheck checker = new HisMestPeriodMediCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMestPeriodMediDAO.CreateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPeriodMedi_ThemMoiThatBai);
							throw new Exception("Them moi thong tin HisMestPeriodMedi that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMestPeriodMediDTOs.AddRange(listData);
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
			if (IsNotNullOrEmpty(this.recentHisMestPeriodMediDTOs))
            {
                if (!new HisMestPeriodMediTruncate(param).TruncateList(this.recentHisMestPeriodMediDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisMestPeriodMedi that bai, can kiem tra lai." + LogUtil.TraceData("HisMestPeriodMediDTOs", this.recentHisMestPeriodMediDTOs));
                }
            }
        }
    }
}
