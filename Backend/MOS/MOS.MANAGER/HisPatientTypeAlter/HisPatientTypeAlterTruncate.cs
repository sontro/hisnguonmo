using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Update;
using MOS.MANAGER.HisTreatment.Util;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPatientTypeAlter
{
    class HisPatientTypeAlterTruncate : BusinessBase
    {
        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisPatientTypeAlterUtil hisPatientTypeAlterUtil;
        private HisSereServUpdateHein hisSereServUpdateHein;
        private HIS_PATIENT_TYPE_ALTER recentPatientTypeAlter;

        internal HisPatientTypeAlterTruncate()
            : base()
        {
            this.Init();
        }

        internal HisPatientTypeAlterTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
            this.hisPatientTypeAlterUtil = new HisPatientTypeAlterUtil(param);
        }

        internal bool Truncate(DeletePatientTypeAlterSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientTypeAlterCheck checker = new HisPatientTypeAlterCheck(param);

                HIS_PATIENT_TYPE_ALTER raw = null;
                valid = valid && checker.VerifyId(data.PatientTypeAlterId, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.VerifySereServPatientType(raw, null);

                if (valid)
                {
                    //Lay thong tin treatment_id de tu dong cap nhat thong tin gia va bao hiem cua sere_serv
                    HIS_TREATMENT treatment = null;
                    HIS_PATIENT patient = null;
                    HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                    HisPatientCheck patientChecker = new HisPatientCheck(param);
                    valid = valid && treatmentChecker.IsUnLock(raw.TREATMENT_ID, ref treatment);//chi cho cap nhat khi chua bi khoa
                    valid = valid && patientChecker.VerifyId(treatment.PATIENT_ID, ref patient);
                    valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                    valid = valid && treatmentChecker.IsUnpause(treatment);//chi cho cap nhat khi chua bi tam khoa
                    valid = valid && treatmentChecker.IsUnLockHein(treatment);//chi cho cap nhat khi chua duyet khoa BH

                    if (valid)
                    {
                        if (!DAOWorker.HisPatientTypeAlterDAO.Delete(raw))
                        {
                            throw new Exception("Delete du lieu HIS_PATIENT_TYPE_ALTER that bai. ID:" + raw.ID);
                        }
                        this.recentPatientTypeAlter = raw;

                        this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, false);

                        //Update thong tin sere_serv
                        if (!this.hisSereServUpdateHein.UpdateDb())
                        {
                            throw new Exception("Khong the cap nhat lai thong tin sere_sev tuong ung voi treatment_id: " + raw.TREATMENT_ID + ". Rollback du lieu");
                        }
                        List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(treatment.ID);

                        this.ProcessTreatment(data, treatment, raw, ptas);
                        this.hisPatientTypeAlterUtil.ProcessPatient(ptas, patient);

                        if (!DAOWorker.HisPatientTypeAlterDAO.Truncate(raw))
                        {
                            throw new Exception("Truncate du lieu HIS_PATIENT_TYPE_ALTER that bai. ID:" + raw.ID);
                        }
                        this.recentPatientTypeAlter = null;
                        HisTreatmentLog.Run(treatment.TREATMENT_CODE, raw, EventLog.Enum.HisPatientTypeAlter_XoaThongTinDienDoiTuong);
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessTreatment(DeletePatientTypeAlterSDO data, HIS_TREATMENT treatment, HIS_PATIENT_TYPE_ALTER raw, List<HIS_PATIENT_TYPE_ALTER> remains)
        {
            Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
            HIS_TREATMENT beforeUpdate = Mapper.Map<HIS_TREATMENT>(treatment);//clone phuc vu rollback
            
            //Trong truong hop du lieu bi xoa la "dieu tri"
            //Thi kiem tra xem co phai la du lieu dieu tri dau tien cua BN ko
            if (HisTreatmentTypeCFG.TREATMENTs.Contains(raw.TREATMENT_TYPE_ID))
            {
                List<HIS_PATIENT_TYPE_ALTER> allBefore = new List<HIS_PATIENT_TYPE_ALTER>(){raw};
                if (IsNotNullOrEmpty(remains))
                {
                    allBefore.AddRange(remains);
                }

                HIS_PATIENT_TYPE_ALTER firstClinical = allBefore
                    .Where(o => HisTreatmentTypeCFG.TREATMENTs.Contains(o.TREATMENT_TYPE_ID))
                    .OrderBy(o => o.LOG_TIME).ThenBy(o => o.ID).FirstOrDefault();

                //Neu xoa dữ liệu "nhập viện" (dữ liệu "điều trị" đầu tiên) và người dùng chọn xóa số vào viện
                if (firstClinical != null && firstClinical.ID == data.PatientTypeAlterId && data.IsDeleteInCode)
                {
                    treatment.IN_CODE = null;
                }
            }

            //Luu du thua du lieu
            HisTreatmentUtil.SetTdl(treatment, remains);

            if (ValueChecker.IsPrimitiveDiff<HIS_TREATMENT>(beforeUpdate, treatment)
                && !this.hisTreatmentUpdate.Update(treatment, beforeUpdate))
            {
                throw new Exception("Cap nhat thong tin CLINICAL_IN_TIME cho bang treatment that bai. Rollback du lieu");
            }
        }


        internal void RollbackData()
        {
            try
            {
                if (this.recentPatientTypeAlter != null)
                {
                    if (!DAOWorker.SqlDAO.Execute("UPDATE HIS_PATIENT_TYPE_ALTER SET IS_DELETE = 0 WHERE ID = :param1", this.recentPatientTypeAlter.ID))
                    {
                        LogSystem.Warn("Rollback viec huy dien doi tuong that bai");
                    }
                }
                this.hisPatientTypeAlterUtil.Rollback();
                this.hisTreatmentUpdate.RollbackData();
                this.hisSereServUpdateHein.RollbackData();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
