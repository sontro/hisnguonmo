using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentUpdate : BusinessBase
    {
        private List<HIS_TREATMENT> beforeUpdateHisTreatments = new List<HIS_TREATMENT>();

        internal HisTreatmentUpdate()
            : base()
        {

        }

        internal HisTreatmentUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TREATMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT raw = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsUnTemporaryLock(raw);
                valid = valid && checker.IsValidInOutTime(data.IN_TIME, data.OUT_TIME, data.CLINICAL_IN_TIME);
                if (valid)
                {
                    this.beforeUpdateHisTreatments.Add(raw);
                    if (!DAOWorker.HisTreatmentDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatment that bai." + LogUtil.TraceData("data", data));
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

        internal bool Update(HIS_TREATMENT data, HIS_TREATMENT beforeUpdate, bool isNotCheckUnlock = false)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && (isNotCheckUnlock || checker.IsUnLock(beforeUpdate));
                valid = valid && (isNotCheckUnlock || checker.IsUnTemporaryLock(beforeUpdate));
                valid = valid && checker.IsValidInOutTime(data.IN_TIME, data.OUT_TIME, data.CLINICAL_IN_TIME);
                if (valid)
                {
                    this.beforeUpdateHisTreatments.Add(beforeUpdate);
                    if (!DAOWorker.HisTreatmentDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatment that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_TREATMENT> listData, List<HIS_TREATMENT> befores)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(listData))
                {
                    this.beforeUpdateHisTreatments.AddRange(befores);
                    if (!DAOWorker.HisTreatmentDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HIS_TREATMENT that bai." + LogUtil.TraceData("listData", listData));
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

        internal bool LockHein(HIS_TREATMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT raw = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsUnTemporaryLock(raw);
                valid = valid && checker.IsValidInOutTime(data.IN_TIME, data.OUT_TIME, data.CLINICAL_IN_TIME);
                valid = valid && checker.ExistsCode(data.TREATMENT_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisTreatments.Add(raw);
                    if (!DAOWorker.HisTreatmentDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatment that bai." + LogUtil.TraceData("data", data));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisTreatments))
            {
                if (!DAOWorker.HisTreatmentDAO.UpdateList(this.beforeUpdateHisTreatments))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatment that bai, can kiem tra lai." + LogUtil.TraceData("HisTreatment", this.beforeUpdateHisTreatments));
                }
            }
        }
    }
}
