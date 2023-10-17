using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatment.Util;
using MOS.MANAGER.HisPatientTypeAlter;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    class HisTreatmentCreate : BusinessBase
    {
        private List<HIS_TREATMENT> recentHisTreatments = new List<HIS_TREATMENT>();

        internal HisTreatmentCreate()
            : base()
        {
        }

        internal HisTreatmentCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TREATMENT data, HIS_PATIENT patient, bool isNewPatient, HIS_PATIENT_TYPE_ALTER pta)
        {
            bool result = false;
            try
            {
                if (this.ValidateBeforeCreate(data, patient, pta, isNewPatient)
                    && new HisTreatmentCheck(param).VerifyRequireField(data))
                {
                    result = this.CreateWithoutValidate(data, patient, pta);
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

        internal bool CreateWithoutValidate(HIS_TREATMENT data, HIS_PATIENT patient, HIS_PATIENT_TYPE_ALTER pta)
        {
            bool result = false;
            try
            {
                HisTreatmentUtil.SetTdl(data, patient, pta);
                if (new HisTreatmentCheck(param).VerifyRequireField(data))
                {
                    if (!DAOWorker.HisTreatmentDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTreatment that bai." + LogUtil.TraceData("HisTreatment", data));
                    }
                    this.recentHisTreatments.Add(data);
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
        /// Thuc hien validate cac dieu kien truoc khi thuc hien tao treatment
        /// </summary>
        /// <param name="data"></param>
        /// <param name="patient"></param>
        /// <param name="pta"></param>
        /// <param name="isNewPatient"></param>
        /// <returns></returns>
        internal bool ValidateBeforeCreate(HIS_TREATMENT data, HIS_PATIENT patient, HIS_PATIENT_TYPE_ALTER pta, bool isNewPatient)
        {
            HisTreatmentUtil.SetTdl(data, patient, pta);
            bool valid = true;
            HisTreatmentCheck checker = new HisTreatmentCheck(param);
            HisPatientTypeAlterCheck ptaChecker = new HisPatientTypeAlterCheck(param);
            valid = valid && checker.IsNotUseAppointmentCode(data);
            valid = valid && checker.IsValidProgramId(data);
            //kiem tra thong tin chuyen den co hop le hay khong
            valid = valid && checker.IsValidTransferInInfo(data);
            //kiem tra thong tin cap cuu co hop le hay khong
            valid = valid && checker.IsValidEmergencyInfo(data);
            //neu benh nhan moi tren he thong thi khong can check
            valid = valid && (isNewPatient || checker.IsValidOpenTreatmentPolicy(data, pta.PATIENT_TYPE_ID));
            //Voi ho so BHYT, thi benh nhan chi duoc tao 1 ho so "khong phai la cap cuu", cac ho so con lai bat buoc la cap cuu. 
            valid = valid && ptaChecker.IsValidOpenNotBhytTreatmentPolicy(patient.ID, data, pta);
            return valid;
        }

        internal void Rollback()
        {
            if (IsNotNullOrEmpty(this.recentHisTreatments))
            {
                if (!DAOWorker.HisTreatmentDAO.TruncateList(this.recentHisTreatments))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatment that bai, can kiem tra lai." + LogUtil.TraceData("HisTreatments", this.recentHisTreatments));
                }
            }
        }
    }
}
