using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Bed.CreateTempByBedLog
{
    internal class HisServiceReqCreateTempByBedLogProcessor: BusinessBase
    {
        private HisServiceReqCreate hisServiceReqCreate;
        private HisSereServCreate hisSereServCreate;
        private HisSereServExtCreate hisSereServExtCreate;
        private HisSereServUpdateHein hisSereServUpdateHein;
        private HisSereServPackage37 hisSereServPackage37;
        private HisSereServPackageBirth hisSereServPackageBirth;
        private HisSereServPackagePttm hisSereServPackagePttm;
        
        internal HisServiceReqCreateTempByBedLogProcessor()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqCreateTempByBedLogProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqCreate = new HisServiceReqCreate(param);
            this.hisSereServCreate = new HisSereServCreate(param);
            this.hisSereServExtCreate = new HisSereServExtCreate(param);
        }

        /// <summary>
        /// Chi xu ly khi co bat cau hinh "su dung chi phi giuong tam tinh"
        /// Va bed_log chua co thong tin finish_time, va da co thong tin doi tuong thanh toan, dich vu giuong
        /// </summary>
        /// <param name="bedLog"></param>
        /// <param name="treatment"></param>
        /// <param name="workPlace"></param>
        /// <param name="ptas"></param>
        /// <param name="existSereServs"></param>
        internal bool Run(HIS_BED_LOG bedLog, HIS_TREATMENT treatment, long workingRoomId, List<HIS_PATIENT_TYPE_ALTER> ptas, List<HIS_SERE_SERV> existSereServs)
        {
            try
            {
                HIS_SERVICE_REQ serviceReq = null;
                HIS_SERE_SERV sereServ = null;
                WorkPlaceSDO workPlace = null;

                if (HisSereServCFG.IS_USING_BED_TEMP
                    && !bedLog.FINISH_TIME.HasValue && bedLog.PATIENT_TYPE_ID.HasValue && bedLog.BED_SERVICE_TYPE_ID.HasValue
                    && this.HasWorkPlaceInfo(workingRoomId, ref workPlace))
                {
                    this.ProcessServiceReq(bedLog, treatment, ptas, workPlace, ref serviceReq);
                    this.ProcessSereServ(bedLog, treatment, serviceReq, ptas, existSereServs, workPlace, ref sereServ);
                    this.ProcessSereServExt(bedLog, sereServ);
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
                return false;
            }
        }

        private void ProcessServiceReq(HIS_BED_LOG bedLog, HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> ptas, WorkPlaceSDO workPlace, ref HIS_SERVICE_REQ serviceReq)
        {
            HIS_PATIENT_TYPE_ALTER pta = new HisPatientTypeAlterGet().GetApplied(treatment.ID, bedLog.START_TIME, ptas);

            serviceReq = new HIS_SERVICE_REQ();
            serviceReq.REQUEST_ROOM_ID = workPlace.RoomId;
            serviceReq.REQUEST_DEPARTMENT_ID = workPlace.DepartmentId;
            serviceReq.EXECUTE_DEPARTMENT_ID = workPlace.DepartmentId;
            serviceReq.EXECUTE_ROOM_ID = workPlace.RoomId;
            serviceReq.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__G;
            serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
            serviceReq.INTRUCTION_TIME = bedLog.START_TIME;
            serviceReq.BED_LOG_ID = bedLog.ID;
            serviceReq.TREATMENT_ID = treatment.ID;
            serviceReq.ICD_TEXT = treatment.ICD_TEXT;//icd lay mac dinh trong treatment
            serviceReq.ICD_NAME = treatment.ICD_NAME;//icd lay mac dinh trong treatment
            serviceReq.ICD_CODE = treatment.ICD_CODE;//icd lay mac dinh trong treatment
            serviceReq.ICD_CAUSE_CODE = treatment.ICD_CAUSE_CODE;
            serviceReq.ICD_CAUSE_NAME = treatment.ICD_CAUSE_NAME;
            serviceReq.TDL_PATIENT_ID = treatment.PATIENT_ID;
            serviceReq.TREATMENT_TYPE_ID = pta.TREATMENT_TYPE_ID;
            serviceReq.SESSION_CODE = Guid.NewGuid().ToString();
            if (!this.hisServiceReqCreate.Create(serviceReq, treatment))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
        }

        private void ProcessSereServ(HIS_BED_LOG bedLog, HIS_TREATMENT treatment, HIS_SERVICE_REQ serviceReq, List<HIS_PATIENT_TYPE_ALTER> ptas, List<HIS_SERE_SERV> existSereServs, WorkPlaceSDO workPlace, ref HIS_SERE_SERV sereServ)
        {
            HisSereServSetInfo addInfo = new HisSereServSetInfo(param, ptas);
            HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);
            HIS_PATIENT_TYPE_ALTER pta = null;

            sereServ = new HIS_SERE_SERV();
            sereServ.SERVICE_ID = bedLog.BED_SERVICE_TYPE_ID.Value;
            sereServ.AMOUNT = 0;

            if (treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU && treatment.IS_OLD_TEMP_BED == Constant.IS_TRUE)
            {
                sereServ.AMOUNT_TEMP = 1;
            }
            else if (treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU && treatment.IS_OLD_TEMP_BED != Constant.IS_TRUE)
            {
                sereServ.AMOUNT_TEMP = 0;
            }
            
            sereServ.PATIENT_TYPE_ID = bedLog.PATIENT_TYPE_ID.Value;
            sereServ.SHARE_COUNT = bedLog.SHARE_COUNT;
            sereServ.SERVICE_REQ_ID = serviceReq.ID;
            sereServ.OTHER_PAY_SOURCE_ID = treatment.OTHER_PAY_SOURCE_ID;

            if (HisSereServCFG.SET_PRIMARY_PATIENT_TYPE == HisSereServCFG.SetPrimaryPatientType.AUTO
                || HisSereServCFG.SET_PRIMARY_PATIENT_TYPE == HisSereServCFG.SetPrimaryPatientType.YES)
            {
                sereServ.PRIMARY_PATIENT_TYPE_ID = bedLog.PRIMARY_PATIENT_TYPE_ID;
            }

            HisSereServUtil.SetTdl(sereServ, serviceReq);

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

        private void ProcessSereServExt(HIS_BED_LOG bedLog, HIS_SERE_SERV sereServ)
        {
            HIS_SERE_SERV_EXT ext = new HIS_SERE_SERV_EXT();
            ext.SERE_SERV_ID = sereServ.ID;
            ext.BED_LOG_ID = bedLog.ID;
            HisSereServExtUtil.SetTdl(ext, sereServ);
            if (!this.hisSereServExtCreate.Create(ext))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
        }

        internal void Rollback()
        {
            this.hisSereServExtCreate.RollbackData();
            this.hisSereServCreate.RollbackData();
            this.hisServiceReqCreate.RollbackData();
        }
    }
}
