using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttGroupBest
{
    partial class HisPtttGroupBestCreate : BusinessBase
    {
		private List<HIS_PTTT_GROUP_BEST> recentHisPtttGroupBests = new List<HIS_PTTT_GROUP_BEST>();
		
        internal HisPtttGroupBestCreate()
            : base()
        {

        }

        internal HisPtttGroupBestCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PTTT_GROUP_BEST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttGroupBestCheck checker = new HisPtttGroupBestCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisPtttGroupBestDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttGroupBest_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPtttGroupBest that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPtttGroupBests.Add(data);
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
		
		internal bool CreateList(List<HIS_PTTT_GROUP_BEST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPtttGroupBestCheck checker = new HisPtttGroupBestCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisPtttGroupBestDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttGroupBest_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPtttGroupBest that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisPtttGroupBests.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisPtttGroupBests))
            {
                if (!DAOWorker.HisPtttGroupBestDAO.TruncateList(this.recentHisPtttGroupBests))
                {
                    LogSystem.Warn("Rollback du lieu HisPtttGroupBest that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPtttGroupBests", this.recentHisPtttGroupBests));
                }
				this.recentHisPtttGroupBests = null;
            }
        }
    }
}
