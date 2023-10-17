using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Sale.Create
{
    class HisExpMestProcessor : BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;
        private HisPatientCreate hisPatientCreate;

        internal HisExpMestProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
            this.hisPatientCreate = new HisPatientCreate(param);
        }

        internal bool Run(HisExpMestSaleSDO data, ref HIS_EXP_MEST hisExpMest, AutoEnum en = AutoEnum.NONE, long? time = null, string loginname = null, string username = null)
        {
            try
            {
                HIS_EXP_MEST expMest = new HIS_EXP_MEST();

                //Tao exp_mest
                if (!string.IsNullOrWhiteSpace(data.PatientName))
                {
                    data.PatientName = data.PatientName.Trim();

                    int idx = data.PatientName.LastIndexOf(" ");
                    expMest.TDL_PATIENT_NAME = data.PatientName;
                    expMest.TDL_PATIENT_FIRST_NAME = (idx > -1 ? data.PatientName.Substring(idx).Trim() : data.PatientName);
                    expMest.TDL_PATIENT_LAST_NAME = (idx > -1 ? data.PatientName.Substring(0, idx).Trim() : "");
                }
                expMest.SALE_PATIENT_TYPE_ID = data.PatientTypeId;
                expMest.MEDI_STOCK_ID = data.MediStockId;
                expMest.REQ_ROOM_ID = data.ReqRoomId;
                expMest.DESCRIPTION = data.Description;
                expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BAN; //xuat ban
                expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST; //xuat ban khi tao ra o trang thai duyet (Thay doi van de o trang thai yeu cau)
                expMest.TDL_PATIENT_ADDRESS = data.PatientAddress;
                expMest.TDL_PATIENT_GENDER_ID = data.PatientGenderId;
                expMest.TDL_PATIENT_PHONE = data.PatientPhone;
                if (data.PatientGenderId.HasValue)
                {
                    HIS_GENDER gender = HisGenderCFG.DATA.Where(o => o.ID == data.PatientGenderId).FirstOrDefault();
                    expMest.TDL_PATIENT_GENDER_NAME = gender != null ? gender.GENDER_NAME : null;
                }
                expMest.TDL_PATIENT_DOB = data.PatientDob;
                expMest.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = data.IsHasNotDayDob ? (short?)Constant.IS_TRUE : null;
                expMest.PRESCRIPTION_ID = data.PrescriptionId;
                expMest.DISCOUNT = data.Discount;
                expMest.IS_NOT_IN_WORKING_TIME = data.IsNotInWorkingTime ? (short?)Constant.IS_TRUE : null;
                expMest.CASHIER_ROOM_ID = data.CashierRoomId;
                expMest.TDL_PATIENT_TAX_CODE = data.PatientTaxCode;
                expMest.TDL_PATIENT_ACCOUNT_NUMBER = data.PatientAccountNumber;
                expMest.TDL_PATIENT_WORK_PLACE = data.PatientWorkPlace;
                expMest.TDL_INTRUCTION_TIME = data.InstructionTime;
                expMest.TDL_INTRUCTION_DATE = data.InstructionTime.HasValue ? data.InstructionTime - data.InstructionTime % 1000000 : null;
                expMest.TDL_PATIENT_ID = data.PatientId;
                expMest.ICD_CODE = data.IcdCode;
                expMest.ICD_NAME = data.IcdName;
                expMest.ICD_SUB_CODE = data.IcdSubCode;
                expMest.ICD_TEXT = data.IcdText;

                if (data.PrescriptionId.HasValue)
                {
                    HIS_SERVICE_REQ prescription = new HisServiceReqGet().GetById(data.PrescriptionId.Value);
                    if (prescription == null)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("prescription_id ko ton tai");
                    }
                    HisExpMestUtil.SetTdl(expMest, prescription, true);
                    expMest.TDL_PRESCRIPTION_CODE = prescription.SERVICE_REQ_CODE;
                }
                else if (data.TreatmentId.HasValue)
                {
                    HIS_TREATMENT treatment = new HisTreatmentGet().GetById(data.TreatmentId.Value);
                    if (treatment != null)
                    {
                        expMest.TDL_TREATMENT_CODE = treatment.TREATMENT_CODE;
                        expMest.TDL_TREATMENT_ID = treatment.ID;
                        expMest.TDL_PATIENT_TYPE_ID = treatment.TDL_PATIENT_TYPE_ID;
                        expMest.TDL_PATIENT_ID = treatment.PATIENT_ID;
                        expMest.TDL_PATIENT_CODE = treatment.TDL_PATIENT_CODE;
                    }
                }
                else if (data.PatientId.HasValue)
                {
                    HIS_PATIENT patient = new HisPatientGet().GetById(data.PatientId.Value);
                    if (patient != null)
                    {
                        expMest.TDL_PATIENT_CODE = patient.PATIENT_CODE;
                        expMest.TDL_PATIENT_ID = patient.ID;
                    }
                }
                else if (Config.HisExpMestCFG.MANAGE_PATIENT_IN_SALE)
                {
                    //tao benh nhan moi gan vao phieu
                    ProcessCreatePatient(expMest);
                }

                expMest.TOTAL_SERVICE_ATTACH_PRICE = data.TotalServiceAttachPrice;

                if (en == AutoEnum.APPROVE)
                {
                    expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE;
                    expMest.IS_EXPORT_EQUAL_APPROVE = null;
                    expMest.LAST_APPROVAL_TIME = time;
                    expMest.LAST_APPROVAL_LOGINNAME = loginname;
                    expMest.LAST_APPROVAL_USERNAME = username;
                    expMest.LAST_APPROVAL_DATE = expMest.LAST_APPROVAL_TIME - expMest.LAST_APPROVAL_TIME % 1000000;
                }
                else if (en == AutoEnum.APPROVE_EXPORT && (expMest.DEBT_ID.HasValue || expMest.BILL_ID.HasValue || !HisExpMestCFG.EXPORT_SALE_MUST_BILL))
                {
                    expMest.IS_EXPORT_EQUAL_APPROVE = null;
                    expMest.LAST_APPROVAL_TIME = time;
                    expMest.LAST_APPROVAL_LOGINNAME = loginname;
                    expMest.LAST_APPROVAL_USERNAME = username;
                    expMest.LAST_APPROVAL_DATE = expMest.LAST_APPROVAL_TIME - expMest.LAST_APPROVAL_TIME % 1000000;

                    expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE;
                    expMest.FINISH_TIME = time;
                    expMest.IS_EXPORT_EQUAL_APPROVE = MOS.UTILITY.Constant.IS_TRUE;
                    expMest.LAST_EXP_LOGINNAME = loginname;
                    expMest.LAST_EXP_TIME = time;
                    expMest.LAST_EXP_USERNAME = username;
                }
                expMest.TDL_PRESCRIPTION_REQ_USERNAME = data.PrescriptionReqUsername;
                expMest.TDL_PRESCRIPTION_REQ_LOGINNAME = data.PrescriptionReqLoginname;

                if (!this.hisExpMestCreate.Create(expMest))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
                hisExpMest = expMest;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        private void ProcessCreatePatient(HIS_EXP_MEST expMest)
        {
            if (!expMest.TDL_PATIENT_GENDER_ID.HasValue)
            {
                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatient_KhongCoThongTinGioiTinh);
                throw new Exception("thieu thong tin gioi tinh khong tao duoc benh nhan");
            }

            if (!expMest.TDL_PATIENT_DOB.HasValue)
            {
                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatient_KhongCoThongTinTuoi);
                throw new Exception("thieu thong tin tuoi khong tao duoc benh nhan");
            }

            HIS_PATIENT patientCreate = new HIS_PATIENT();
            patientCreate.FIRST_NAME = expMest.TDL_PATIENT_FIRST_NAME;
            patientCreate.LAST_NAME = expMest.TDL_PATIENT_LAST_NAME;
            patientCreate.ADDRESS = expMest.TDL_PATIENT_ADDRESS;
            patientCreate.GENDER_ID = expMest.TDL_PATIENT_GENDER_ID.Value;
            patientCreate.PHONE = expMest.TDL_PATIENT_PHONE;
            patientCreate.DOB = expMest.TDL_PATIENT_DOB.Value;
            patientCreate.IS_HAS_NOT_DAY_DOB = expMest.TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
            patientCreate.TAX_CODE = expMest.TDL_PATIENT_TAX_CODE;
            patientCreate.ACCOUNT_NUMBER = expMest.TDL_PATIENT_ACCOUNT_NUMBER;
            patientCreate.WORK_PLACE = expMest.TDL_PATIENT_WORK_PLACE;

            if (!this.hisPatientCreate.Create(patientCreate))
            {
                throw new Exception("Tao patien that bai. Rollback du lieu. Ket thuc nghiep vu");
            }

            expMest.TDL_PATIENT_ID = patientCreate.ID;
            expMest.TDL_PATIENT_CODE = patientCreate.PATIENT_CODE;
        }

        private void ProcessUpdateServiceReq(HIS_EXP_MEST expMest)
        {

        }

        internal void Rollback()
        {
            this.hisExpMestCreate.RollbackData();
            this.hisPatientCreate.RollbackData();
        }
    }
}
