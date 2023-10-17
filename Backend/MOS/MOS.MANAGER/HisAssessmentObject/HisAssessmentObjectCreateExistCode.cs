using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAssessmentObject
{
    partial class HisAssessmentObjectCreate : BusinessBase
    {
		private List<HIS_ASSESSMENT_OBJECT> recentHisAssessmentObjects = new List<HIS_ASSESSMENT_OBJECT>();
		
        internal HisAssessmentObjectCreate()
            : base()
        {

        }

        internal HisAssessmentObjectCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ASSESSMENT_OBJECT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAssessmentObjectCheck checker = new HisAssessmentObjectCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ASSESSMENT_OBJECT_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAssessmentObjectDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAssessmentObject_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAssessmentObject that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAssessmentObjects.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAssessmentObjects))
            {
                if (!DAOWorker.HisAssessmentObjectDAO.TruncateList(this.recentHisAssessmentObjects))
                {
                    LogSystem.Warn("Rollback du lieu HisAssessmentObject that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAssessmentObjects", this.recentHisAssessmentObjects));
                }
				this.recentHisAssessmentObjects = null;
            }
        }
    }
}
