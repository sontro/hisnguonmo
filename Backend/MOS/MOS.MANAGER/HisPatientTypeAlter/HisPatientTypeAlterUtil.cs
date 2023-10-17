using AutoMapper;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt.HeinRightRouteType;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Update;
using MOS.MANAGER.HisTreatment.Util;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPatientTypeAlter
{
    class HisPatientTypeAlterUtil : BusinessBase
    {
        private HisPatientUpdate hisPatientUpdate;
        private HisTreatmentUpdate hisTreatmentUpdate;

        internal HisPatientTypeAlterUtil(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
            this.hisPatientUpdate = new HisPatientUpdate(param);
        }

        internal void ProcessPatientAndTreatment(HIS_PATIENT patient, HIS_TREATMENT treatment, HisPatientTypeAlterAndTranPatiSDO data)
        {
            List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(treatment.ID);
            this.ProcessPatient(ptas, patient);
            this.ProcessTreatment(data, treatment, ptas);
        }

        internal void ProcessPatient(List<HIS_PATIENT_TYPE_ALTER> ptas, HIS_PATIENT patient)
        {
            if (patient != null)
            {
                Mapper.CreateMap<HIS_PATIENT, HIS_PATIENT>();
                HIS_PATIENT beforeUpdate = Mapper.Map<HIS_PATIENT>(patient);//clone phuc vu rollback

                HisPatientUtil.SetTdl(patient, ptas);

                //Neu co thay doi du lieu thi moi thuc hien update
                if (ValueChecker.IsPrimitiveDiff<HIS_PATIENT>(beforeUpdate, patient)
                    && !this.hisPatientUpdate.Update(patient, beforeUpdate))
                {
                    throw new Exception("Cap nhat thong tin TDL_HEIN_CARD_NUMBER cho bang patient that bai. Rollback du lieu");
                }
            }
        }

        private void ProcessTreatment(HisPatientTypeAlterAndTranPatiSDO data, HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> ptas)
        {
            Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
            HIS_TREATMENT beforeUpdate = Mapper.Map<HIS_TREATMENT>(treatment);//clone phuc vu rollback

            if (ptas != null)
            {
                //Luu du thua du lieu
                //Can chay ham nay truoc de cap nhat lai "clinical_in_time" se dung o ham sau
                HisTreatmentUtil.SetTdl(treatment, ptas);

                //Xu ly nghiep vu sinh so vao vien
                this.SetInCode(treatment, data.PatientTypeAlter, ptas);

                //Cap nhat thong tin cap cuu
                if (data.PatientTypeAlter.RIGHT_ROUTE_TYPE_CODE == HeinRightRouteTypeCode.EMERGENCY && treatment.IS_EMERGENCY != MOS.UTILITY.Constant.IS_TRUE)
                {
                    treatment.IS_EMERGENCY = MOS.UTILITY.Constant.IS_TRUE;
                }

                //Cap nhat lai thong tin chuyen tuyen
                if (this.IsChangeTranPati(treatment, data))
                {
                    treatment.TRANSFER_IN_FORM_ID = data.TransferInFormId;
                    treatment.TRANSFER_IN_ICD_CODE = data.TransferInIcdCode;
                    treatment.TRANSFER_IN_ICD_NAME = data.TransferInIcdName;
                    treatment.TRANSFER_IN_MEDI_ORG_CODE = data.TransferInMediOrgCode;
                    treatment.TRANSFER_IN_MEDI_ORG_NAME = data.TransferInMediOrgName;
                    treatment.TRANSFER_IN_REASON_ID = data.TransferInReasonId;
                    treatment.TRANSFER_IN_CMKT = data.TransferInCmkt;
                    treatment.TRANSFER_IN_CODE = data.TransferInCode;
                    treatment.TRANSFER_IN_TIME_FROM = data.TransferInTimeFrom;
                    treatment.TRANSFER_IN_TIME_TO = data.TransferInTimeTo;
                }

                //Neu co thay doi thi thuc hien update
                if (ValueChecker.IsPrimitiveDiff<HIS_TREATMENT>(beforeUpdate, treatment)
                    && !this.hisTreatmentUpdate.Update(treatment, beforeUpdate))
                {
                    throw new Exception("Cap nhat thong tin CLINICAL_IN_TIME cho bang treatment that bai. Rollback du lieu");
                }
                HisTreatmentInCode.FinishDB(treatment);//xac nhan da xu ly DB (phuc vu nghiep vu sinh so vao vien)
            }
        }

        /// <summary>
        /// Xu ly nghiep vu de sinh ma vao vien khi tao moi/thay doi thong tin dien doi tuong
        /// </summary>
        /// <param name="treatment"></param>
        /// <param name="data"></param>
        private void SetInCode(HIS_TREATMENT treatment, HIS_PATIENT_TYPE_ALTER data, List<HIS_PATIENT_TYPE_ALTER> allPtas)
        {
            //Neu dien doi tuong la "dieu tri" (dieu tri noi tru hoac dieu tri ngoai tru) va khong cau hinh "nhap thu cong so vao vien"
            //thi kiem tra de sinh so vao vien
            if (HisTreatmentTypeCFG.TREATMENTs.Contains(data.TREATMENT_TYPE_ID) && treatment.CLINICAL_IN_TIME.HasValue && !HisTreatmentCFG.IS_MANUAL_IN_CODE)
            {
                bool generateInCode = false;

                //Neu hồ sơ chua co so "vao vien" thi thực hiện sinh số vào viện
                if (string.IsNullOrWhiteSpace(treatment.IN_CODE))
                {
                    generateInCode = true;
                }
                //Nếu đã có so vao vien nhung trong truong hop cau hinh 
                //sinh ma nhập viện theo option2 (sinh ma theo mã diện điều trị)
                //và khi tiếp nhận vào khoa người dùng thay đổi diện điều trị so với lúc yêu cầu
                else if (HisTreatmentCFG.IN_CODE_FORMAT_OPTION == (int)HisTreatmentCFG.InCodeFormatOption.OPTION2)
                {
                    //Lay cac dien doi tuong dieu tri truoc do
                    List<HIS_PATIENT_TYPE_ALTER> beforeTreatments = allPtas.Where(o => o.LOG_TIME <= data.LOG_TIME && HisTreatmentTypeCFG.TREATMENTs.Contains(o.TREATMENT_TYPE_ID) && o.ID != data.ID).ToList();
                    
                    //Neu truoc do ko co dien doi tuong dieu tri nao --> day la dien doi tuong dieu tri noi tru/ngoai tru
                    //dau tien cua BN, va co su thay doi dien dieu tri so voi luc yeu cau
                    //==> co sinh lai so vao vien
                    if (!IsNotNullOrEmpty(beforeTreatments) && treatment.IN_TREATMENT_TYPE_ID != data.TREATMENT_TYPE_ID)
                    {
                        generateInCode = true;
                    }
                }

                if (generateInCode)
                {
                    HIS_DEPARTMENT_TRAN dt = new HisDepartmentTranGet().GetById(data.DEPARTMENT_TRAN_ID);
                    HisTreatmentInCode.SetInCode(treatment, treatment.CLINICAL_IN_TIME.Value, dt.DEPARTMENT_ID, data.TREATMENT_TYPE_ID);
                }
            }
        }

        //Kiem tra xem thong tin chuyen tuyen trong treatment co khac voi thong tin chuyen tuyen y/c tao moi hay khong
        private bool IsChangeTranPati(HIS_TREATMENT treatment, HisPatientTypeAlterAndTranPatiSDO data)
        {
            return treatment.TRANSFER_IN_FORM_ID != data.TransferInFormId
                || treatment.TRANSFER_IN_ICD_CODE != data.TransferInIcdCode
                || treatment.TRANSFER_IN_ICD_NAME != data.TransferInIcdName
                || treatment.TRANSFER_IN_MEDI_ORG_CODE != data.TransferInMediOrgCode
                || treatment.TRANSFER_IN_MEDI_ORG_NAME != data.TransferInMediOrgName
                || treatment.TRANSFER_IN_REASON_ID != data.TransferInReasonId
                || treatment.TRANSFER_IN_CODE != data.TransferInCode
                || treatment.TRANSFER_IN_CMKT != data.TransferInCmkt
                || treatment.TRANSFER_IN_TIME_FROM != data.TransferInTimeFrom
                || treatment.TRANSFER_IN_TIME_TO != data.TransferInTimeTo;
        }

        internal void Rollback()
        {
            this.hisPatientUpdate.RollbackData();
            this.hisTreatmentUpdate.RollbackData();
        }
    }
}
