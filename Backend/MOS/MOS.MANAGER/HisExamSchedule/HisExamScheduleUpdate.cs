using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExamSchedule
{
    partial class HisExamScheduleUpdate : BusinessBase
    {
        private List<HIS_EXAM_SCHEDULE> beforeUpdateHisExamSchedules = new List<HIS_EXAM_SCHEDULE>();

        internal HisExamScheduleUpdate()
            : base()
        {

        }

        internal HisExamScheduleUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXAM_SCHEDULE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExamScheduleCheck checker = new HisExamScheduleCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EXAM_SCHEDULE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.VerifyTime(data);
                valid = valid && checker.CheckExists(data, data.ID);
                if (valid)
                {
                    if (!DAOWorker.HisExamScheduleDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExamSchedule_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExamSchedule that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisExamSchedules.Add(raw);
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

        internal bool UpdateList(List<HIS_EXAM_SCHEDULE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExamScheduleCheck checker = new HisExamScheduleCheck(param);
                List<HIS_EXAM_SCHEDULE> listRaw = new List<HIS_EXAM_SCHEDULE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.VerifyTime(data);
                    valid = valid && checker.CheckExists(data, data.ID);
                }
                if (valid)
                {
                    if (!DAOWorker.HisExamScheduleDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExamSchedule_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExamSchedule that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisExamSchedules.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisExamSchedules))
            {
                if (!DAOWorker.HisExamScheduleDAO.UpdateList(this.beforeUpdateHisExamSchedules))
                {
                    LogSystem.Warn("Rollback du lieu HisExamSchedule that bai, can kiem tra lai." + LogUtil.TraceData("HisExamSchedules", this.beforeUpdateHisExamSchedules));
                }
                this.beforeUpdateHisExamSchedules = null;
            }
        }
    }
}
