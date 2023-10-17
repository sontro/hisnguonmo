using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmotionlessResult
{
    partial class HisEmotionlessResultCreate : BusinessBase
    {
		private List<HIS_EMOTIONLESS_RESULT> recentHisEmotionlessResults = new List<HIS_EMOTIONLESS_RESULT>();
		
        internal HisEmotionlessResultCreate()
            : base()
        {

        }

        internal HisEmotionlessResultCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EMOTIONLESS_RESULT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmotionlessResultCheck checker = new HisEmotionlessResultCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EMOTIONLESS_RESULT_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisEmotionlessResultDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmotionlessResult_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEmotionlessResult that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisEmotionlessResults.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisEmotionlessResults))
            {
                if (!DAOWorker.HisEmotionlessResultDAO.TruncateList(this.recentHisEmotionlessResults))
                {
                    LogSystem.Warn("Rollback du lieu HisEmotionlessResult that bai, can kiem tra lai." + LogUtil.TraceData("recentHisEmotionlessResults", this.recentHisEmotionlessResults));
                }
				this.recentHisEmotionlessResults = null;
            }
        }
    }
}
