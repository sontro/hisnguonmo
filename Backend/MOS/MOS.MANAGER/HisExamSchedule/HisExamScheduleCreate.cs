using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExamSchedule
{
    partial class HisExamScheduleCreate : BusinessBase
    {
        private List<HIS_EXAM_SCHEDULE> recentHisExamSchedules = new List<HIS_EXAM_SCHEDULE>();

        internal HisExamScheduleCreate()
            : base()
        {

        }

        internal HisExamScheduleCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXAM_SCHEDULE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExamScheduleCheck checker = new HisExamScheduleCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyTime(data);
                valid = valid && checker.CheckExists(data, 0);
                if (valid)
                {
                    if (!DAOWorker.HisExamScheduleDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExamSchedule_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExamSchedule that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExamSchedules.Add(data);
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

        internal bool CreateList(List<HIS_EXAM_SCHEDULE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExamScheduleCheck checker = new HisExamScheduleCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.VerifyTime(data);
                    valid = valid && checker.CheckExists(data, 0);
                }
                if (valid)
                {
                    if (!DAOWorker.HisExamScheduleDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExamSchedule_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExamSchedule that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisExamSchedules.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisExamSchedules))
            {
                if (!DAOWorker.HisExamScheduleDAO.TruncateList(this.recentHisExamSchedules))
                {
                    LogSystem.Warn("Rollback du lieu HisExamSchedule that bai, can kiem tra lai." + LogUtil.TraceData("recentHisExamSchedules", this.recentHisExamSchedules));
                }
                this.recentHisExamSchedules = null;
            }
        }
    }
}
