using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisTransReq;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisTreatment.Util;
using MOS.MANAGER.Config;
using MOS.SDO;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisWorkPlace;
using MOS.UTILITY;

namespace MOS.MANAGER.HisTreatment.ChangePatient
{
    partial class HisTreatmentChangePatient : BusinessBase
    {
        internal HisTreatmentChangePatient()
            : base()
        {
        }

        internal HisTreatmentChangePatient(CommonParam paramUpdate)
            : base(paramUpdate)
        {
        }

        internal bool Run(HisTreatmentUpdatePatiSDO data, ref List<HIS_TREATMENT> resultData)
        {
            bool result = false;
            try
            {
                HIS_TREATMENT raw = null;
                HIS_PATIENT newPatient = null;
                HIS_PATIENT oldPatient = null;
                List<HIS_TREATMENT> listTreatmentProcess = new List<HIS_TREATMENT>();;

                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                HisPatientCheck patientChecker = new HisPatientCheck(param);
                HisPatientTypeAlterCheck patyChecker = new HisPatientTypeAlterCheck(param);

                bool valid = true;
                valid = valid && checker.VerifyId(data.HisTreatment.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsUnTemporaryLock(raw);
                valid = valid && patientChecker.VerifyId(data.HisTreatment.PATIENT_ID, ref newPatient);
                valid = valid && patientChecker.VerifyId(raw.PATIENT_ID, ref oldPatient);
                valid = valid && patyChecker.IsValidForChangePatient(raw.ID, oldPatient, newPatient);
                
                if (valid)
                {
                    // Radio checked cap nhat cac ho so chua khoa vien phi
                    if (data.IsUpdateTreatmentUnLocked.HasValue && data.IsUpdateTreatmentUnLocked.Value)
                    {
                        // Lay danh sach ho so benh nhan cu chua khoa vien phi
                        HisTreatmentFilterQuery filter = new HisTreatmentFilterQuery();
                        filter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                        filter.PATIENT_ID = raw.PATIENT_ID;
                        List<HIS_TREATMENT> treatmentUnLocked = new HisTreatmentGet().Get(filter);

                        listTreatmentProcess.AddRange(treatmentUnLocked);
                    }
                    else if (data.IsUpdateAllOtherTreatements.HasValue && data.IsUpdateAllOtherTreatements.Value)
                    {
                        // Lay tat ca danh sach ho so benh nhan cu
                        List<HIS_TREATMENT> allTreatments = new HisTreatmentGet().GetByPatientId(raw.PATIENT_ID);
                        listTreatmentProcess.AddRange(allTreatments);
                    }
                    else
                    {
                        listTreatmentProcess.Add(raw);
                    }

                    if (IsNotNullOrEmpty(listTreatmentProcess))
                    {
                        List<long> treatmentIds = listTreatmentProcess.Select(o => o.ID).ToList();
                        List<string> codes = listTreatmentProcess.Select(o => o.TREATMENT_CODE).ToList();

                        if (!new ChangePatientProcessor().Run(treatmentIds, newPatient.ID))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                            throw new Exception("Cap nhat thong tin HisTreatment that bai." + LogUtil.TraceData("data", data));
                        }

                        result = true;
                        resultData = new HisTreatmentGet().GetByIds(treatmentIds).ToList();
                        this.ProcessUpdateForEmr(data.IsUpdateEmr, resultData);
                        this.EventLogPatient(string.Join(",", codes), oldPatient.PATIENT_CODE, newPatient.PATIENT_CODE);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void EventLogPatient(string treatmentCode, string oldPatientCode, string newPatientCode)
        {
            try
            {
                string data = string.Format("{0}: {1} => {2}: {3}", SimpleEventKey.PATIENT_CODE, oldPatientCode, SimpleEventKey.PATIENT_CODE, newPatientCode);
                new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisPatient_GhepMaBenhNhan, data).TreatmentCode(treatmentCode).Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessUpdateForEmr(bool? isUpdateEmr, List<HIS_TREATMENT> treatments)
        {
            try
            {
                if (isUpdateEmr.HasValue && isUpdateEmr.Value)
                {
                    foreach (var treatment in treatments)
                    {
                        HisTreatmentUploadEmr uploadEmr = new HisTreatmentUploadEmr();
                        uploadEmr.Run(treatment);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void Rollback()
        {
        }
    }
}