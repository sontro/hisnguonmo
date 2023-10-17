using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAssessmentMember
{
    partial class HisAssessmentMemberCreate : BusinessBase
    {
		private List<HIS_ASSESSMENT_MEMBER> recentHisAssessmentMembers = new List<HIS_ASSESSMENT_MEMBER>();
		
        internal HisAssessmentMemberCreate()
            : base()
        {

        }

        internal HisAssessmentMemberCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ASSESSMENT_MEMBER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAssessmentMemberCheck checker = new HisAssessmentMemberCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ASSESSMENT_MEMBER_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAssessmentMemberDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAssessmentMember_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAssessmentMember that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAssessmentMembers.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAssessmentMembers))
            {
                if (!DAOWorker.HisAssessmentMemberDAO.TruncateList(this.recentHisAssessmentMembers))
                {
                    LogSystem.Warn("Rollback du lieu HisAssessmentMember that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAssessmentMembers", this.recentHisAssessmentMembers));
                }
				this.recentHisAssessmentMembers = null;
            }
        }
    }
}
