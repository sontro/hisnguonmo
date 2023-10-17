using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBornResult
{
    partial class HisBornResultCreate : BusinessBase
    {
		private List<HIS_BORN_RESULT> recentHisBornResults = new List<HIS_BORN_RESULT>();
		
        internal HisBornResultCreate()
            : base()
        {

        }

        internal HisBornResultCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BORN_RESULT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBornResultCheck checker = new HisBornResultCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.BORN_RESULT_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisBornResultDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBornResult_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBornResult that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBornResults.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisBornResults))
            {
                if (!new HisBornResultTruncate(param).TruncateList(this.recentHisBornResults))
                {
                    LogSystem.Warn("Rollback du lieu HisBornResult that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBornResults", this.recentHisBornResults));
                }
            }
        }
    }
}
