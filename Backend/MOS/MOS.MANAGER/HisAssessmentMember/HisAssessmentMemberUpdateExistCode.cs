using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAssessmentMember
{
    partial class HisAssessmentMemberUpdate : BusinessBase
    {
		private List<HIS_ASSESSMENT_MEMBER> beforeUpdateHisAssessmentMembers = new List<HIS_ASSESSMENT_MEMBER>();
		
        internal HisAssessmentMemberUpdate()
            : base()
        {

        }

        internal HisAssessmentMemberUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ASSESSMENT_MEMBER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAssessmentMemberCheck checker = new HisAssessmentMemberCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ASSESSMENT_MEMBER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ASSESSMENT_MEMBER_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisAssessmentMemberDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAssessmentMember_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAssessmentMember that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisAssessmentMembers.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_ASSESSMENT_MEMBER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAssessmentMemberCheck checker = new HisAssessmentMemberCheck(param);
                List<HIS_ASSESSMENT_MEMBER> listRaw = new List<HIS_ASSESSMENT_MEMBER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ASSESSMENT_MEMBER_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisAssessmentMemberDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAssessmentMember_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAssessmentMember that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisAssessmentMembers.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAssessmentMembers))
            {
                if (!DAOWorker.HisAssessmentMemberDAO.UpdateList(this.beforeUpdateHisAssessmentMembers))
                {
                    LogSystem.Warn("Rollback du lieu HisAssessmentMember that bai, can kiem tra lai." + LogUtil.TraceData("HisAssessmentMembers", this.beforeUpdateHisAssessmentMembers));
                }
				this.beforeUpdateHisAssessmentMembers = null;
            }
        }
    }
}
