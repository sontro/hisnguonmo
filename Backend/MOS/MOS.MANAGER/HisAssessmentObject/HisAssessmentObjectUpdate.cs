using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAssessmentObject
{
    partial class HisAssessmentObjectUpdate : BusinessBase
    {
		private List<HIS_ASSESSMENT_OBJECT> beforeUpdateHisAssessmentObjects = new List<HIS_ASSESSMENT_OBJECT>();
		
        internal HisAssessmentObjectUpdate()
            : base()
        {

        }

        internal HisAssessmentObjectUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ASSESSMENT_OBJECT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAssessmentObjectCheck checker = new HisAssessmentObjectCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ASSESSMENT_OBJECT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ASSESSMENT_OBJECT_CODE, data.ID);
                if (valid)
                {                    
					if (!DAOWorker.HisAssessmentObjectDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAssessmentObject_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAssessmentObject that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisAssessmentObjects.Add(raw);
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

        internal bool UpdateList(List<HIS_ASSESSMENT_OBJECT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAssessmentObjectCheck checker = new HisAssessmentObjectCheck(param);
                List<HIS_ASSESSMENT_OBJECT> listRaw = new List<HIS_ASSESSMENT_OBJECT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisAssessmentObjectDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAssessmentObject_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAssessmentObject that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisAssessmentObjects.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAssessmentObjects))
            {
                if (!DAOWorker.HisAssessmentObjectDAO.UpdateList(this.beforeUpdateHisAssessmentObjects))
                {
                    LogSystem.Warn("Rollback du lieu HisAssessmentObject that bai, can kiem tra lai." + LogUtil.TraceData("HisAssessmentObjects", this.beforeUpdateHisAssessmentObjects));
                }
				this.beforeUpdateHisAssessmentObjects = null;
            }
        }
    }
}
