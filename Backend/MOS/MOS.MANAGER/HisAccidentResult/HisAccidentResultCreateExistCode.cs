using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentResult
{
    partial class HisAccidentResultCreate : BusinessBase
    {
		private List<HIS_ACCIDENT_RESULT> recentHisAccidentResults = new List<HIS_ACCIDENT_RESULT>();
		
        internal HisAccidentResultCreate()
            : base()
        {

        }

        internal HisAccidentResultCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ACCIDENT_RESULT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentResultCheck checker = new HisAccidentResultCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ACCIDENT_RESULT_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAccidentResultDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentResult_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAccidentResult that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAccidentResults.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAccidentResults))
            {
                if (!new HisAccidentResultTruncate(param).TruncateList(this.recentHisAccidentResults))
                {
                    LogSystem.Warn("Rollback du lieu HisAccidentResult that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAccidentResults", this.recentHisAccidentResults));
                }
            }
        }
    }
}
