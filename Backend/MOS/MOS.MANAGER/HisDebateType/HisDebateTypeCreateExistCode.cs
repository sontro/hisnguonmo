using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateType
{
    partial class HisDebateTypeCreate : BusinessBase
    {
		private List<HIS_DEBATE_TYPE> recentHisDebateTypes = new List<HIS_DEBATE_TYPE>();
		
        internal HisDebateTypeCreate()
            : base()
        {

        }

        internal HisDebateTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DEBATE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebateTypeCheck checker = new HisDebateTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.DEBATE_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisDebateTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebateType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDebateType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDebateTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisDebateTypes))
            {
                if (!DAOWorker.HisDebateTypeDAO.TruncateList(this.recentHisDebateTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisDebateType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDebateTypes", this.recentHisDebateTypes));
                }
				this.recentHisDebateTypes = null;
            }
        }
    }
}
