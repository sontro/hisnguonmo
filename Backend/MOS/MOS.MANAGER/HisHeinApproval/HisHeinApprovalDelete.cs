using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHeinApproval
{
    partial class HisHeinApprovalDelete : BusinessBase
    {
        HisSereServUpdate hisSereServUpdate;

        internal HisHeinApprovalDelete()
            : base()
        {
            this.hisSereServUpdate = new HisSereServUpdate(param);
        }

        internal HisHeinApprovalDelete(CommonParam paramDelete)
            : base(paramDelete)
        {
            this.hisSereServUpdate = new HisSereServUpdate(param);
        }

        internal bool Delete(long heinApprovalId)
        {
            bool result = false;
            try
            {
                this.ProcessHisSereServ(heinApprovalId);
                if (!this.DeleteHeinApproval(heinApprovalId))
                {
                    throw new Exception("Rollback du lieu");
                }
                result = true;
            }
            catch (Exception ex)
            {
                this.hisSereServUpdate.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessHisSereServ(long heinApprovalId)
        {
            List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByHeinApprovalId(heinApprovalId);
            Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
            List<HIS_SERE_SERV> beforeUpdates = Mapper.Map<List<HIS_SERE_SERV>>(hisSereServs); //phuc vu rollback
            if (IsNotNullOrEmpty(hisSereServs))
            {
                hisSereServs.ForEach(o => o.HEIN_APPROVAL_ID = null);
                if (!this.hisSereServUpdate.UpdateList(hisSereServs, beforeUpdates, false))
                {
                    throw new Exception("Ket thuc nghiep vu.");
                }
            }
        }

        private bool DeleteHeinApproval(long heinApprovalId)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHeinApprovalCheck checker = new HisHeinApprovalCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HIS_HEIN_APPROVAL raw = null;
                HIS_TREATMENT treatment = null;
                valid = valid && checker.VerifyId(heinApprovalId, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && treatmentChecker.VerifyId(raw.TREATMENT_ID, ref treatment);
                valid = valid && treatmentChecker.IsUnLockHein(treatment);
                if (valid)
                {
                    result = DAOWorker.HisHeinApprovalDAO.Delete(raw);
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

        internal bool DeleteList(List<HIS_HEIN_APPROVAL> heinApprovals)
        {
            bool result = false;
            try
            {
                bool valid = true;
                if (valid)
                {
                    result = DAOWorker.HisHeinApprovalDAO.DeleteList(heinApprovals);
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
    }
}
