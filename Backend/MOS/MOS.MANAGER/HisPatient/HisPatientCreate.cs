using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MOS.MANAGER.HisPatient
{
    class HisPatientCreate : BusinessBase
    {
        private List<HIS_PATIENT> recentHisPatients = new List<HIS_PATIENT>();

        internal HisPatientCreate()
            : base()
        {

        }

        internal HisPatientCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PATIENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientCheck checker = new HisPatientCheck(param);
                HisPatientTypeAlterCheck patientTypeChecker = new HisPatientTypeAlterCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsStoreCode(data.PATIENT_STORE_CODE, null);
                if (valid)
                {
                    this.GetBranch(new List<HIS_PATIENT>() { data });
                    if (!DAOWorker.HisPatientDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatient_ThemMoiThatBai);
                        throw new Exception("Khong them moi duoc thong tin HisPatient." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPatients.Add(data);

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

        internal bool CreateList(List<HIS_PATIENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPatientCheck checker = new HisPatientCheck(param);
                HisPatientTypeAlterCheck patientTypeChecker = new HisPatientTypeAlterCheck(param);
                valid = valid && checker.CheckDuplicateStoreCode(listData);

                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsStoreCode(data.PATIENT_STORE_CODE, null);
                }

                if (valid)
                {
                    this.GetBranch(listData);
                    if (!DAOWorker.HisPatientDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatient_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPatient that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisPatients.AddRange(listData);
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

        private void GetBranch(List<HIS_PATIENT> listData)
        {
            try
            {
                HIS_BRANCH branch = new Token.TokenManager().GetBranch();
                if (branch != null)
                {
                    listData.ForEach(o => o.BRANCH_ID = branch.ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal void RollbackData()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentHisPatients) && !DAOWorker.HisPatientDAO.TruncateList(this.recentHisPatients))
                {
                    LogSystem.Warn("Rollback du lieu HisPatient that bai, can kiem tra lai." + LogUtil.TraceData("HisPatients", this.recentHisPatients));
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
