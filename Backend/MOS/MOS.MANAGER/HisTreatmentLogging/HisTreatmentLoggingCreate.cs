using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentLogging
{
    partial class HisTreatmentLoggingCreate : BusinessBase
    {
        private List<HIS_TREATMENT_LOGGING> recentHisTreatmentLoggings = new List<HIS_TREATMENT_LOGGING>();

        internal HisTreatmentLoggingCreate()
            : base()
        {

        }

        internal HisTreatmentLoggingCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TREATMENT_LOGGING data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentLoggingCheck checker = new HisTreatmentLoggingCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisTreatmentLoggingDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentLogging_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTreatmentLogging that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisTreatmentLoggings.Add(data);
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

        internal bool CreateList(List<HIS_TREATMENT_LOGGING> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentLoggingCheck checker = new HisTreatmentLoggingCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisTreatmentLoggingDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentLogging_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTreatmentLogging that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisTreatmentLoggings.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisTreatmentLoggings))
            {
                if (!new HisTreatmentLoggingTruncate(param).TruncateList(this.recentHisTreatmentLoggings))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatmentLogging that bai, can kiem tra lai." + LogUtil.TraceData("recentHisTreatmentLoggings", this.recentHisTreatmentLoggings));
                }
            }
        }
    }
}
