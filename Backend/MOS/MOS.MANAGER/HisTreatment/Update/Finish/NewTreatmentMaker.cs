using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisDepartmentTran.Create;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisTreatment.Util;
using MOS.MANAGER.HisTreatmentBedRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update.Finish
{
    class NewTreatmentMaker : BusinessBase
    {
        private HisTreatmentCreate hisTreatmentCreate;
        private HisPatientTypeAlterCreate hisPatientTypeAlterCreate;
        private HisDepartmentTranCreate hisDepartmentTranCreate;
        private HisTreatmentBedRoomCreate hisTreatmentBedRoomCreate;

        private HIS_TREATMENT recentTreatment;
        private HIS_DEPARTMENT_TRAN recentDepartmentTran;
        private HIS_PATIENT_TYPE_ALTER recentPatientTypeAlter;


        internal NewTreatmentMaker()
            : base()
        {
            this.Init();
        }

        internal NewTreatmentMaker(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTreatmentCreate = new HisTreatmentCreate(param);
            this.hisPatientTypeAlterCreate = new HisPatientTypeAlterCreate(param);
            this.hisDepartmentTranCreate = new HisDepartmentTranCreate(param);
            this.hisTreatmentBedRoomCreate = new HisTreatmentBedRoomCreate(param);
        }

        internal bool Run(HisTreatmentFinishSDO data, HIS_TREATMENT treatment, HIS_DEPARTMENT_TRAN lastDt, List<HIS_PATIENT_TYPE_ALTER> ptas)
        {
            bool result = false;
            try
            {
                if (data.IsTemporary || !data.IsCreateNewTreatment)
                {
                    return true;
                }

                HisTreatmentBedRoomFilterQuery bedRoomFilter = new HisTreatmentBedRoomFilterQuery();
                bedRoomFilter.TREATMENT_ID = treatment.ID;
                bedRoomFilter.HAS_CO_TREATMENT_ID = false;

                List<HIS_TREATMENT_BED_ROOM> lstBedRoom = new HisTreatmentBedRoomGet().Get(bedRoomFilter);
                HIS_TREATMENT_BED_ROOM lastBedRoom = lstBedRoom != null ? lstBedRoom.OrderByDescending(o => o.REMOVE_TIME).FirstOrDefault() : null;
                HIS_PATIENT_TYPE_ALTER lastPaty = ptas.OrderByDescending(o => o.LOG_TIME).ThenByDescending(t => t.ID).FirstOrDefault();
                bool validPatientTypeAlter = data.patientTypeAlter != null
                                                && IsGreaterThanZero(data.patientTypeAlter.PATIENT_TYPE_ID)
                                                && IsGreaterThanZero(data.patientTypeAlter.TREATMENT_TYPE_ID);
                if (validPatientTypeAlter)
                    lastPaty = data.patientTypeAlter;
                   

                this.ProcessTreatment(data, treatment, lastDt, lastPaty);

                this.ProcessHisDepartmentTran(data, lastDt, lastPaty);

                this.ProcessHisPatientTypeAlter(data,lastPaty);

                this.ProcessTreatmentBedRoom(lastBedRoom);

                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        private void ProcessTreatment(HisTreatmentFinishSDO data, HIS_TREATMENT treatment, HIS_DEPARTMENT_TRAN lastDt, HIS_PATIENT_TYPE_ALTER lastPaty)
        {

            HIS_TREATMENT newTreat = new HIS_TREATMENT();
            newTreat.BRANCH_ID = treatment.BRANCH_ID;
            newTreat.CLINICAL_IN_TIME = data.NewTreatmentInTime.Value;
            newTreat.CONTRAINDICATION_IDS = treatment.CONTRAINDICATION_IDS;
            newTreat.DEPARTMENT_IDS = lastDt.DEPARTMENT_ID.ToString();
            newTreat.EMR_COVER_TYPE_ID = treatment.EMR_COVER_TYPE_ID;
            newTreat.ICD_CAUSE_CODE = treatment.ICD_CAUSE_CODE;
            newTreat.ICD_CAUSE_NAME = treatment.ICD_CAUSE_NAME;
            newTreat.ICD_CODE = treatment.ICD_CODE;
            newTreat.ICD_ID__DELETE = treatment.ICD_ID__DELETE;
            newTreat.ICD_NAME = treatment.ICD_NAME;
            newTreat.ICD_SUB_CODE = treatment.ICD_SUB_CODE;
            newTreat.ICD_TEXT = treatment.ICD_TEXT;
            newTreat.IS_AUTO_DISCOUNT = treatment.IS_AUTO_DISCOUNT;
            newTreat.IS_EMERGENCY = treatment.IS_EMERGENCY;
            newTreat.IS_HOLD_BHYT_CARD = treatment.IS_HOLD_BHYT_CARD;
            newTreat.LAST_DEPARTMENT_ID = lastDt.DEPARTMENT_ID;
            newTreat.MEDI_ORG_CODE = treatment.MEDI_ORG_CODE;
            newTreat.MEDI_ORG_NAME = treatment.MEDI_ORG_NAME;
            newTreat.MEDI_RECORD_ID = treatment.MEDI_RECORD_ID;
            newTreat.MEDI_RECORD_TYPE_ID = treatment.MEDI_RECORD_TYPE_ID;
            newTreat.PATIENT_CONDITION = treatment.PATIENT_CONDITION;
            newTreat.PATIENT_ID = treatment.PATIENT_ID;
            newTreat.PROGRAM_ID = treatment.PROGRAM_ID;
            newTreat.IS_CHRONIC = treatment.IS_CHRONIC;
            newTreat.TDL_HEIN_CARD_FROM_TIME = lastPaty.HEIN_CARD_FROM_TIME;
            newTreat.TDL_HEIN_CARD_NUMBER = lastPaty.HEIN_CARD_NUMBER;
            newTreat.TDL_HEIN_CARD_TO_TIME = lastPaty.HEIN_CARD_TO_TIME;
            newTreat.TDL_HEIN_MEDI_ORG_CODE = lastPaty.HEIN_MEDI_ORG_CODE;
            newTreat.TDL_HEIN_MEDI_ORG_NAME = lastPaty.HEIN_MEDI_ORG_NAME;

            newTreat.TDL_PATIENT_TYPE_ID = lastPaty.PATIENT_TYPE_ID;
            newTreat.TDL_TREATMENT_TYPE_ID = lastPaty.TREATMENT_TYPE_ID;

            newTreat.IN_TIME = data.NewTreatmentInTime.Value;

            //thong tin chuyen tuyen
            newTreat.IS_TRANSFER_IN = treatment.IS_TRANSFER_IN;
            newTreat.TRANSFER_IN_TIME_FROM = treatment.TRANSFER_IN_TIME_FROM;
            newTreat.TRANSFER_IN_TIME_TO = treatment.TRANSFER_IN_TIME_TO;
            newTreat.TRANSFER_IN_MEDI_ORG_CODE = treatment.TRANSFER_IN_MEDI_ORG_CODE;
            newTreat.TRANSFER_IN_MEDI_ORG_NAME = treatment.TRANSFER_IN_MEDI_ORG_NAME;
            newTreat.TRANSFER_IN_CODE = treatment.TRANSFER_IN_CODE;
            newTreat.TRANSFER_IN_REASON_ID = treatment.TRANSFER_IN_REASON_ID;
            newTreat.TRANSFER_IN_FORM_ID = treatment.TRANSFER_IN_FORM_ID;
            newTreat.TRANSFER_IN_CMKT = treatment.TRANSFER_IN_CMKT;
            newTreat.TRANSFER_IN_REVIEWS = treatment.TRANSFER_IN_REVIEWS;
            newTreat.TRANSFER_IN_ICD_CODE = treatment.TRANSFER_IN_ICD_CODE;
            newTreat.TRANSFER_IN_ICD_NAME = treatment.TRANSFER_IN_ICD_NAME;

            //Neu dien doi tuong la dieu tri thi thuc hien nhap cac thong tin lien quan den vao vien
            if (HisTreatmentTypeCFG.TREATMENTs.Contains(lastPaty.TREATMENT_TYPE_ID))
            {
                newTreat.IN_DEPARTMENT_ID = lastDt.DEPARTMENT_ID;
                newTreat.HOSPITALIZE_DEPARTMENT_ID = lastDt.DEPARTMENT_ID;
                newTreat.IN_TREATMENT_TYPE_ID = lastPaty.TREATMENT_TYPE_ID;
                newTreat.IN_ROOM_ID = data.EndRoomId;
                newTreat.IN_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                newTreat.IN_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                newTreat.IN_ICD_NAME = treatment.ICD_NAME;
                newTreat.IN_ICD_CODE = treatment.ICD_CODE;
                newTreat.IN_ICD_SUB_CODE = treatment.ICD_SUB_CODE;
                newTreat.IN_ICD_TEXT = treatment.ICD_TEXT;

                HisTreatmentInCode.SetInCode(newTreat, newTreat.IN_TIME, lastDt.DEPARTMENT_ID, lastPaty.TREATMENT_TYPE_ID);
            }

            HisTreatmentUtil.SetTdl(newTreat, treatment);

            //Thuc hien them moi treatment, neu them moi that bai thi ket thuc nghiep vu
            if (!this.hisTreatmentCreate.CreateWithoutValidate(newTreat, null, null))
            {
                throw new Exception("Rollback du lieu, nghiep vu tiep theo se ko duoc thuc hien");
            }
            HisTreatmentInCode.FinishDB(newTreat);//xac nhan da xu ly DB (phuc vu nghiep vu sinh so vao vien)
            this.recentTreatment = newTreat;
        }


        /// <summary>
        /// Xu ly thong tin thay doi khoa phong
        /// - Them moi thong tin HisTreatmentLog
        /// - Them moi thong tin HisDepartmentTran
        /// </summary>
        /// <param name="data"></param>
        private void ProcessHisDepartmentTran(HisTreatmentFinishSDO data, HIS_DEPARTMENT_TRAN lastDt, HIS_PATIENT_TYPE_ALTER lastPaty)
        {

            HisDepartmentTranSDO sdo = new HisDepartmentTranSDO();
            sdo.TreatmentId = this.recentTreatment.ID;

            //Neu doi tuong dieu tri la "dieu tri noi tru", "dieu tri ngoai tru" thi danh dau ban ghi chuyen khoa cung la ban ghi nhap vien
            if (lastPaty != null
                && (lastPaty.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU
                || lastPaty.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU))
            {
                sdo.IsHospitalized = true;
            }
            sdo.IsReceive = true;
            sdo.Time = this.recentTreatment.IN_TIME;
            sdo.DepartmentId = lastDt.DEPARTMENT_ID;
            sdo.RequestRoomId = data.EndRoomId;

            HIS_DEPARTMENT_TRAN resultData = new HIS_DEPARTMENT_TRAN();
            if (!this.hisDepartmentTranCreate.Create(sdo, true, ref resultData))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentDepartmentTran = resultData;
        }

        /// <summary>
        /// Xu ly thong tin thay doi dien doi tuong benh nhan
        /// - Them moi thong tin HisTreatmentLog
        /// - Them moi thong tin HisPatientTypeAlter (co bo sung thong tin BHYT neu co su dung the BHYT)
        /// </summary>
        /// <param name="data"></param>
        private void ProcessHisPatientTypeAlter(HisTreatmentFinishSDO data, HIS_PATIENT_TYPE_ALTER lastPaty)
        {
            HIS_PATIENT_TYPE_ALTER sdo = new HIS_PATIENT_TYPE_ALTER();
            sdo.TREATMENT_ID = this.recentTreatment.ID;
            sdo.LOG_TIME = this.recentTreatment.IN_TIME;
            sdo.EXECUTE_ROOM_ID = data.EndRoomId;
            sdo.DEPARTMENT_TRAN_ID = this.recentDepartmentTran.ID;
            sdo.BHYT_URL = lastPaty.BHYT_URL;//tiennv
            sdo.ADDRESS = lastPaty.ADDRESS;
            sdo.HAS_BIRTH_CERTIFICATE = lastPaty.HAS_BIRTH_CERTIFICATE;
            sdo.HEIN_CARD_FROM_TIME = lastPaty.HEIN_CARD_FROM_TIME;
            sdo.HEIN_CARD_NUMBER = lastPaty.HEIN_CARD_NUMBER;
            sdo.HEIN_CARD_TO_TIME = lastPaty.HEIN_CARD_TO_TIME;
            sdo.HEIN_MEDI_ORG_CODE = lastPaty.HEIN_MEDI_ORG_CODE;
            sdo.HEIN_MEDI_ORG_NAME = lastPaty.HEIN_MEDI_ORG_NAME;
            sdo.JOIN_5_YEAR = lastPaty.JOIN_5_YEAR;
            sdo.LIVE_AREA_CODE = lastPaty.LIVE_AREA_CODE;
            sdo.PAID_6_MONTH = lastPaty.PAID_6_MONTH;
            sdo.RIGHT_ROUTE_CODE = lastPaty.RIGHT_ROUTE_CODE;
            sdo.RIGHT_ROUTE_TYPE_CODE = lastPaty.RIGHT_ROUTE_TYPE_CODE;
            sdo.TDL_PATIENT_ID = lastPaty.TDL_PATIENT_ID;
            sdo.IS_TEMP_QN = lastPaty.IS_TEMP_QN;
            sdo.PATIENT_TYPE_ID = lastPaty.PATIENT_TYPE_ID;
            sdo.BHYT_URL = lastPaty.BHYT_URL;
            sdo.LEVEL_CODE = lastPaty.LEVEL_CODE;
            sdo.TREATMENT_TYPE_ID = lastPaty.TREATMENT_TYPE_ID;
            sdo.JOIN_5_YEAR_TIME = lastPaty.JOIN_5_YEAR_TIME;
            sdo.FREE_CO_PAID_TIME = lastPaty.FREE_CO_PAID_TIME;

            if (!this.hisPatientTypeAlterCreate.Create(sdo, this.recentTreatment, true))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentPatientTypeAlter = sdo;
        }


        private void ProcessTreatmentBedRoom(HIS_TREATMENT_BED_ROOM lastBedRoom)
        {
            if (lastBedRoom!=null)
            {
                HIS_TREATMENT_BED_ROOM bedRoom = new HIS_TREATMENT_BED_ROOM();
                bedRoom.ADD_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                bedRoom.ADD_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                bedRoom.ADD_TIME = this.recentTreatment.IN_TIME;
                bedRoom.BED_ID = lastBedRoom.BED_ID;
                bedRoom.BED_ROOM_ID = lastBedRoom.BED_ROOM_ID;
                bedRoom.TREATMENT_ID = this.recentTreatment.ID;

                if (!this.hisTreatmentBedRoomCreate.Create(bedRoom))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        internal void Rollback()
        {
            try
            {
                this.hisTreatmentBedRoomCreate.RollbackData();
                this.hisPatientTypeAlterCreate.RollbackData();
                this.hisDepartmentTranCreate.RollbackData();
                this.hisTreatmentCreate.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
