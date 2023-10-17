using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBaby
{
    partial class HisBabyCreate : BusinessBase
    {
		private List<HIS_BABY> recentHisBabys = new List<HIS_BABY>();
		
        internal HisBabyCreate()
            : base()
        {

        }

        internal HisBabyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BABY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBabyCheck checker = new HisBabyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisBabyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBaby_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBaby that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBabys.Add(data);
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
		
		internal bool CreateList(List<HIS_BABY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBabyCheck checker = new HisBabyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBabyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBaby_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBaby that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisBabys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisBabys))
            {
                if (!DAOWorker.HisBabyDAO.TruncateList(this.recentHisBabys))
                {
                    LogSystem.Warn("Rollback du lieu HisBaby that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBabys", this.recentHisBabys));
                }
            }
        }
    }
}
