using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBhytParam
{
    partial class HisBhytParamCreate : BusinessBase
    {
		private List<HIS_BHYT_PARAM> recentHisBhytParams = new List<HIS_BHYT_PARAM>();
		
        internal HisBhytParamCreate()
            : base()
        {

        }

        internal HisBhytParamCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BHYT_PARAM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBhytParamCheck checker = new HisBhytParamCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.BHYT_PARAM_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisBhytParamDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBhytParam_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBhytParam that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBhytParams.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisBhytParams))
            {
                if (!DAOWorker.HisBhytParamDAO.TruncateList(this.recentHisBhytParams))
                {
                    LogSystem.Warn("Rollback du lieu HisBhytParam that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBhytParams", this.recentHisBhytParams));
                }
				this.recentHisBhytParams = null;
            }
        }
    }
}
