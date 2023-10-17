using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStentConclude
{
    partial class HisStentConcludeCreate : BusinessBase
    {
		private List<HIS_STENT_CONCLUDE> recentHisStentConcludes = new List<HIS_STENT_CONCLUDE>();
		
        internal HisStentConcludeCreate()
            : base()
        {

        }

        internal HisStentConcludeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_STENT_CONCLUDE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisStentConcludeCheck checker = new HisStentConcludeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisStentConcludeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisStentConclude_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisStentConclude that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisStentConcludes.Add(data);
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
		
		internal bool CreateList(List<HIS_STENT_CONCLUDE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisStentConcludeCheck checker = new HisStentConcludeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisStentConcludeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisStentConclude_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisStentConclude that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisStentConcludes.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisStentConcludes))
            {
                if (!DAOWorker.HisStentConcludeDAO.TruncateList(this.recentHisStentConcludes))
                {
                    LogSystem.Warn("Rollback du lieu HisStentConclude that bai, can kiem tra lai." + LogUtil.TraceData("recentHisStentConcludes", this.recentHisStentConcludes));
                }
				this.recentHisStentConcludes = null;
            }
        }
    }
}
