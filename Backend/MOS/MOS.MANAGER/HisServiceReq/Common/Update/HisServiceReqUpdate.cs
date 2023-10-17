using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq
{
    partial class HisServiceReqUpdate : BusinessBase
    {
        private List<HIS_SERVICE_REQ> beforeUpdateHisServiceReqs = new List<HIS_SERVICE_REQ>();

        internal HisServiceReqUpdate()
            : base()
        {

        }

        internal HisServiceReqUpdate(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool Update(HIS_SERVICE_REQ data, bool verifyTreatment)
        {
            return this.Update(data, null, verifyTreatment);
        }

        internal bool Update(HIS_SERVICE_REQ data, HIS_SERVICE_REQ old, bool verifyTreatment)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                if (verifyTreatment)
                {
                    HIS_TREATMENT hisTreatment = null;
                    valid = valid && checker.IsUnLock(data);
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && treatmentChecker.IsUnLock(data.TREATMENT_ID, ref hisTreatment);
                    valid = valid && treatmentChecker.IsUnpause(hisTreatment);
                    valid = valid && treatmentChecker.IsUnTemporaryLock(hisTreatment);
                    valid = valid && treatmentChecker.IsUnLockHein(hisTreatment);
                    //luu du thua du lieu
                    data.TDL_PATIENT_ID = hisTreatment.PATIENT_ID;
                }
                if (valid)
                {
                    if (old != null)
                    {
                        this.beforeUpdateHisServiceReqs.Add(old);
                    }

                    if (!DAOWorker.HisServiceReqDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceReq that bai." + LogUtil.TraceData("data", data));
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

        /// <summary>
        /// Luu y: ham update nay khong validate cac nghiep vu dac thu (vd: kiem tra ho so dieu tri, ...)
        /// </summary>
        /// <param name="listData"></param>
        /// <returns></returns>
        internal bool UpdateList(List<HIS_SERVICE_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                List<HIS_SERVICE_REQ> listRaw = new List<HIS_SERVICE_REQ>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsUnLock(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisServiceReqs.AddRange(listRaw);
                    if (!DAOWorker.HisServiceReqDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HIS_SERVICE_REQ that bai." + LogUtil.TraceData("listData", listData));
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

        internal bool UpdateList(List<HIS_SERVICE_REQ> listData, List<HIS_SERVICE_REQ> befores)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                valid = valid && checker.IsUnLock(befores);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisServiceReqs.AddRange(befores);
                    if (!DAOWorker.HisServiceReqDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HIS_SERVICE_REQ that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisServiceReqs))
            {
                if (!DAOWorker.HisServiceReqDAO.UpdateList(this.beforeUpdateHisServiceReqs))
                {
                    LogSystem.Warn("Rollback du lieu beforeUpdateHisServiceReqs that bai, can kiem tra lai." + LogUtil.TraceData("beforeUpdateHisServiceReqs", this.beforeUpdateHisServiceReqs));
                }
                else
                {
                    this.beforeUpdateHisServiceReqs = null;
                }
            }
        }
    }
}
