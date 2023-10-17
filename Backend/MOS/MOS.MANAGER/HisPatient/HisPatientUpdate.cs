using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MOS.MANAGER.HisPatient
{
    partial class HisPatientUpdate : BusinessBase
    {
        private HIS_PATIENT beforeUpdateHisPatientDTO;

        internal HisPatientUpdate()
            : base()
        {

        }

        internal HisPatientUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PATIENT data)
        {
            bool result = false;
            try
            {
                HIS_PATIENT raw = new HisPatientGet().GetById(data.ID);
                return this.Update(data, raw);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool UpdateWithoutChecking(HIS_PATIENT data, HIS_PATIENT before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientCheck checker = new HisPatientCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisPatientDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatient_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPatient that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisPatientDTO = before;
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

        internal bool Update(HIS_PATIENT data, HIS_PATIENT before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientTypeAlterCheck patientTypeChecker = new HisPatientTypeAlterCheck(param);
                HisPatientCheck checker = new HisPatientCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(before);
                valid = valid && checker.ExistsStoreCode(data.PATIENT_STORE_CODE, data.ID);
                if (valid)
                {
                    if (data.CAREER_ID.HasValue)
                    {
                        HIS_CAREER carrer = HisCareerCFG.DATA.FirstOrDefault(o => o.ID == data.CAREER_ID.Value);
                        if (carrer != null)
                        {
                            data.CAREER_CODE = carrer.CAREER_CODE;
                            data.CAREER_NAME = carrer.CAREER_NAME;
                        }
                    }
                    else
                    {
                        data.CAREER_CODE = null;
                        data.CAREER_NAME = null;
                    }
                    if (!DAOWorker.HisPatientDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatient_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPatient that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisPatientDTO = before;
                    this.InitThreadUpdateToLis(data, before);
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

        private void InitThreadUpdateToLis(HIS_PATIENT newPatient, HIS_PATIENT oldPatient)
        {
            try
            {
                if (HisPatientUtil.CheckIsDiffForLis(newPatient, oldPatient))
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(this.UpdateInfoToLis));
                    thread.Priority = ThreadPriority.AboveNormal;
                    thread.Start(newPatient.ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void UpdateInfoToLis(object threadData)
        {
            try
            {
                if (threadData == null || threadData.GetType() != typeof(long)) return;
                long patientId = (long)threadData;
                new HisPatientUpdateToLis().Run(patientId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal void RollbackData()
        {
            if (this.beforeUpdateHisPatientDTO != null)
            {
                if (!DAOWorker.HisPatientDAO.Update(this.beforeUpdateHisPatientDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisPatient that bai, can kiem tra lai." + LogUtil.TraceData("HisPatientDTO", this.beforeUpdateHisPatientDTO));
                }
            }
        }
    }
}
