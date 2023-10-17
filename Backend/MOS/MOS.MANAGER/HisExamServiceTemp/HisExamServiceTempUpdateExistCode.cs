using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExamServiceTemp
{
    partial class HisExamServiceTempUpdate : BusinessBase
    {
		private List<HIS_EXAM_SERVICE_TEMP> beforeUpdateHisExamServiceTemps = new List<HIS_EXAM_SERVICE_TEMP>();
		
        internal HisExamServiceTempUpdate()
            : base()
        {

        }

        internal HisExamServiceTempUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXAM_SERVICE_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExamServiceTempCheck checker = new HisExamServiceTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EXAM_SERVICE_TEMP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.EXAM_SERVICE_TEMP_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisExamServiceTemps.Add(raw);
					if (!DAOWorker.HisExamServiceTempDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExamServiceTemp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExamServiceTemp that bai." + LogUtil.TraceData("data", data));
                    }
                    
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

        internal bool UpdateList(List<HIS_EXAM_SERVICE_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExamServiceTempCheck checker = new HisExamServiceTempCheck(param);
                List<HIS_EXAM_SERVICE_TEMP> listRaw = new List<HIS_EXAM_SERVICE_TEMP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.EXAM_SERVICE_TEMP_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisExamServiceTemps.AddRange(listRaw);
					if (!DAOWorker.HisExamServiceTempDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExamServiceTemp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExamServiceTemp that bai." + LogUtil.TraceData("listData", listData));
                    }
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisExamServiceTemps))
            {
                if (!new HisExamServiceTempUpdate(param).UpdateList(this.beforeUpdateHisExamServiceTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisExamServiceTemp that bai, can kiem tra lai." + LogUtil.TraceData("HisExamServiceTemps", this.beforeUpdateHisExamServiceTemps));
                }
            }
        }
    }
}
