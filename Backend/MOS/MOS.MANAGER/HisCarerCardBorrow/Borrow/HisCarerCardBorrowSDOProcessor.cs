using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisCarerCard;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisCarerCardBorrow
{
    class HisCarerCardBorrowSDOProcessor : BusinessBase
    {
        private HisCarerCardBorrowCreate hisCarerCardBorrowCreate;
        private HisServiceReqCreate hisServiceReqCreate;
        private HisSereServCreate hisSereServCreate;

        private HisSereServUpdateHein hisSereServUpdateHein;
        private HisSereServPackage37 hisSereServPackage37;
        private HisSereServPackageBirth hisSereServPackageBirth;
        private HisSereServPackagePttm hisSereServPackagePttm;

        internal HisCarerCardBorrowSDOProcessor()
            : base()
        {
            this.Init();
        }

        internal HisCarerCardBorrowSDOProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisCarerCardBorrowCreate = new HisCarerCardBorrowCreate(param);
            this.hisServiceReqCreate = new HisServiceReqCreate(param);
            this.hisSereServCreate = new HisSereServCreate(param);
        }

        internal bool Run(HisCarerCardBorrowSDO data, HisCarerCardSDOInfo card, HIS_TREATMENT treatment, WorkPlaceSDO workPlace, List<HIS_CARER_CARD> carerCards)
        {
            bool result = false;
            try
            {
                List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(treatment.ID);
                List<HIS_SERE_SERV> existSereServs = new HisSereServGet().GetByTreatmentId(treatment.ID);
                HIS_SERVICE_REQ serviceReq = null;
                HIS_SERE_SERV sereServ = null;
                V_HIS_SERVICE service = null;
                HIS_CARER_CARD_BORROW cardborrow = null;
                this.ProcessCarerCardBorrow(data, card, treatment.ID, ref cardborrow);
                this.ProcessServiceReq(cardborrow, carerCards, treatment, workPlace, ref serviceReq, ref service);
                this.ProcessSereServ(cardborrow, treatment, serviceReq, service, ptas, existSereServs, workPlace, ref sereServ);

                result = true;

            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
        }

        private void ProcessSereServ(HIS_CARER_CARD_BORROW cardborrow, HIS_TREATMENT treatment, HIS_SERVICE_REQ serviceReq, V_HIS_SERVICE service, List<HIS_PATIENT_TYPE_ALTER> ptas, List<HIS_SERE_SERV> existSereServs, WorkPlaceSDO workPlace, ref HIS_SERE_SERV sereServ)
        {
            HisSereServSetInfo addInfo = new HisSereServSetInfo(param, ptas);
            HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);
            HIS_PATIENT_TYPE_ALTER pta = null;

            sereServ = new HIS_SERE_SERV();
            if (service != null)
            {
                sereServ.SERVICE_ID = service.ID;
            }
            HisCarerCardBorrowUtil.ChangeAmountWithBorrowTime(sereServ, cardborrow.BORROW_TIME);
            sereServ.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE;
            sereServ.SERVICE_REQ_ID = serviceReq.ID;
            //sereServ.SHARE_COUNT = bedLog.SHARE_COUNT;
            sereServ.OTHER_PAY_SOURCE_ID = treatment.OTHER_PAY_SOURCE_ID;

            HisSereServUtil.SetTdl(sereServ, serviceReq);
            sereServ.TDL_CARER_CARD_BORROW_ID = serviceReq.CARER_CARD_BORROW_ID;

            addInfo.AddInfo(sereServ, ref pta);
            if (!priceAdder.AddPrice(sereServ, sereServ.TDL_INTRUCTION_TIME, sereServ.TDL_EXECUTE_BRANCH_ID, sereServ.TDL_REQUEST_ROOM_ID, sereServ.TDL_REQUEST_DEPARTMENT_ID, sereServ.TDL_EXECUTE_ROOM_ID))
            {
                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
            }

            this.hisSereServPackage37 = new HisSereServPackage37(param, treatment.ID, workPlace.RoomId, workPlace.DepartmentId, existSereServs);
            this.hisSereServPackageBirth = new HisSereServPackageBirth(param, workPlace.DepartmentId, existSereServs);
            this.hisSereServPackagePttm = new HisSereServPackagePttm(param, workPlace.DepartmentId, existSereServs);

            //Xu ly de ap dung chinh sach gia 3 ngay 7 ngay
            this.hisSereServPackage37.Apply3Day7Day(sereServ, serviceReq.INTRUCTION_TIME);
            //Xu ly de ap dung goi de
            this.hisSereServPackageBirth.Run(sereServ, sereServ.PARENT_ID);
            //Xu ly de ap dung goi phau thuat tham my
            this.hisSereServPackagePttm.Run(sereServ, sereServ.PARENT_ID, serviceReq.INTRUCTION_TIME);

            if (!this.hisSereServCreate.Create(sereServ, serviceReq, false))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }

            //Neu doi tuong thanh toan la BHYT thi xu ly de bo sung them cac thong tin BHYT (thong tin doi tuong, thong tin ti le thanh toan)
            //Luu y: thong tin nay chi tuong doi (vi day la tien tam tinh)
            if (sereServ.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
            {
                this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, ptas, false);
                //Cap nhat ti le BHYT cho sere_serv: chi thuc hien khi co y/c, tranh thuc hien nhieu lan, giam hieu nang

                List<HIS_SERE_SERV> sereServs = new List<HIS_SERE_SERV>() { sereServ };
                if (IsNotNullOrEmpty(existSereServs))
                {
                    sereServs.AddRange(existSereServs);
                }

                if (!this.hisSereServUpdateHein.UpdateDb(sereServs))
                {
                    throw new Exception("Du lieu se bi rollback. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessServiceReq(HIS_CARER_CARD_BORROW cardborrow, List<HIS_CARER_CARD> carerCards, HIS_TREATMENT treatment, WorkPlaceSDO workPlace, ref HIS_SERVICE_REQ serviceReq, ref V_HIS_SERVICE service)
        {
            serviceReq = new HIS_SERVICE_REQ();
            serviceReq.CARER_CARD_BORROW_ID = cardborrow.ID;
            serviceReq.REQUEST_LOGINNAME = cardborrow.GIVING_LOGINNAME;
            serviceReq.REQUEST_USERNAME = cardborrow.GIVING_USERNAME;
            serviceReq.INTRUCTION_TIME = cardborrow.BORROW_TIME;
            serviceReq.REQUEST_ROOM_ID = workPlace.RoomId;
            serviceReq.REQUEST_DEPARTMENT_ID = workPlace.DepartmentId;
            serviceReq.EXECUTE_ROOM_ID = workPlace.RoomId;
            serviceReq.EXECUTE_DEPARTMENT_ID = workPlace.DepartmentId;
            serviceReq.TREATMENT_TYPE_ID = treatment.TDL_TREATMENT_TYPE_ID;
            serviceReq.TDL_PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__HOSPITAL_FEE;
            HIS_CARER_CARD carercard = IsNotNullOrEmpty(carerCards) ? carerCards.FirstOrDefault(o => o.ID == cardborrow.CARER_CARD_ID) : null;
            service = IsNotNull(carercard) ? HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == carercard.SERVICE_ID) : null;
            if (IsNotNull(service))
            {
                serviceReq.SERVICE_REQ_TYPE_ID = HisServiceReqTypeMappingCFG.SERVICE_TYPE_SERVICE_REQ_TYPE_MAPPING[service.SERVICE_TYPE_ID];
            }
            serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
            serviceReq.TREATMENT_ID = treatment.ID;
            serviceReq.ICD_TEXT = treatment.ICD_TEXT;//icd lay mac dinh trong treatment
            serviceReq.ICD_NAME = treatment.ICD_NAME;//icd lay mac dinh trong treatment
            serviceReq.ICD_CODE = treatment.ICD_CODE;//icd lay mac dinh trong treatment
            serviceReq.ICD_CAUSE_CODE = treatment.ICD_CAUSE_CODE;
            serviceReq.ICD_CAUSE_NAME = treatment.ICD_CAUSE_NAME;
            serviceReq.TDL_PATIENT_ID = treatment.PATIENT_ID;
            serviceReq.SESSION_CODE = Guid.NewGuid().ToString();

            if (!this.hisServiceReqCreate.Create(serviceReq, treatment))
            {
                throw new Exception("Chi dinh dich vu muon the that bai. Rollback du lieu. Ket thuc nghiep vu");
            }
        }

        private void ProcessCarerCardBorrow(HisCarerCardBorrowSDO data, HisCarerCardSDOInfo card, long treatmentId, ref HIS_CARER_CARD_BORROW cardborrow)
        {
            cardborrow = new HIS_CARER_CARD_BORROW();
            cardborrow.CARER_CARD_ID = card.CarerCardId;
            cardborrow.BORROW_TIME = card.BorrowTime;
            cardborrow.TREATMENT_ID = treatmentId;
            cardborrow.GIVING_LOGINNAME = data.GivingLoginName;
            cardborrow.GIVING_USERNAME = data.GivingUserName;
            HisCarerCardBorrowCreate processor = new HisCarerCardBorrowCreate(param);
            if (!this.hisCarerCardBorrowCreate.Create(cardborrow))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCarerCardBorrow_ThemMoiThatBai);
                throw new Exception("Them moi thong tin HisCarerCardBorrow khi cho muon the that bai." + LogUtil.TraceData("data", cardborrow));
            }
        }

        public void RollbackData()
        {
            this.hisSereServCreate.RollbackData();
            this.hisServiceReqCreate.RollbackData();
            this.hisCarerCardBorrowCreate.RollbackData();
        }
    }
}
