using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatmentBedRoom;
using MOS.SDO;
using MOS.ServicePaty;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update.Finish
{
    class AutoCreateBedLog
    {
        public HIS_SERVICE_REQ ServiceReq { get; set; }
        public HIS_SERE_SERV SereServ { get; set; }
        public HIS_BED_LOG BedLog { get; set; }
    }

    class AutoBedAssignProcessor : BusinessBase
    {
        private HisBedLogCreateList hisBedLogCreate;
        private HisServiceReqCreate hisServiceReqCreate;
        private HisSereServCreate hisSereServCreate;

        internal AutoBedAssignProcessor()
            : base()
        {
            this.Init();
        }

        internal AutoBedAssignProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisBedLogCreate = new HisBedLogCreateList(param);
            this.hisServiceReqCreate = new HisServiceReqCreate(param);
            this.hisSereServCreate = new HisSereServCreate(param);
        }

        public void Run(HisTreatmentFinishSDO data, HIS_TREATMENT treatment, List<HIS_SERE_SERV> existsSereServs, List<HIS_PATIENT_TYPE_ALTER> ptas, ref List<HIS_SERE_SERV> newServServs)
        {
            try
            {
                //Chi xu ly nghiep vu nay neu ko bat cau hinh "giuong tam"
                if (treatment != null && treatment.CLINICAL_IN_TIME.HasValue && treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU && !HisSereServCFG.IS_USING_BED_TEMP)
                {
                    DateTime inTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(treatment.CLINICAL_IN_TIME.Value).Value;
                    DateTime outTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.TreatmentFinishTime).Value;

                    double hours = (outTime - inTime).TotalHours;

                    if (hours >= BhytConstant.CLINICAL_TIME_FOR_EMERGENCY)
                    {
                        decimal treatmentDays = (outTime.Date - inTime.Date).Days + 1;

                        V_HIS_ROOM endRoom = HisRoomCFG.DATA.Where(o => o.ID == data.EndRoomId).FirstOrDefault();
                        HIS_DEPARTMENT endDepartment = HisDepartmentCFG.DATA.Where(o => o.ID == endRoom.DEPARTMENT_ID).FirstOrDefault();

                        bool isExistsBed = IsNotNullOrEmpty(existsSereServs) && existsSereServs.Exists(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G);

                        //neu co cau hinh tu dong chi dinh giuong khi ket thuc dieu tri
                        if (endDepartment.AUTO_BED_ASSIGN_OPTION == Constant.IS_TRUE && !isExistsBed)
                        {
                            List<HIS_TREATMENT_BED_ROOM> treatmentBedRooms = new HisTreatmentBedRoomGet().GetByTreatmentId(data.TreatmentId);
                            if (IsNotNullOrEmpty(treatmentBedRooms) && IsNotNullOrEmpty(HisBedCFG.DATA))
                            {
                                treatmentBedRooms = treatmentBedRooms.OrderBy(o => o.ADD_TIME).ToList();
                                

                                List<V_HIS_SERVICE_PATY> servicePaties = HisServicePatyCFG.DATA
                                    .Where(o => o.BRANCH_ID == endDepartment.BRANCH_ID
                                        && o.IS_ACTIVE == Constant.IS_TRUE
                                        && o.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G)
                                    .OrderByDescending(o => o.ID).ToList();

                                V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW
                                    .Where(o => o.ID == servicePaties[0].SERVICE_ID).FirstOrDefault();

                                DateTime? previousFinishTime = null;

                                List<AutoCreateBedLog> autoCreateBedLogs = new List<AutoCreateBedLog>();
                                List<HIS_BED_LOG> toInsertBedLogs = new List<HIS_BED_LOG>();

                                //Duyet tung ngay vao buong
                                //Voi moi buong tao ra 1 bed_log tuong ung
                                //Trong truong hop thoi gian vao 2 buong co giao nhau ngay 
                                //(thoi gian ra cua buong nay, trung ngay voi thoi gian vao cua buong ke tiep),
                                //thi thoi gian vao cua buong ke tiep tu dong tang len 1 ngay de dam bao 
                                //tong so ngay giuong sau khi cong giua cac buong ko vuot qua so ngay dieu tri (ngay ra - ngay vao + 1)

                                
                                for (int i = 0; i < treatmentBedRooms.Count; i++)
                                {
                                    long startTime = treatmentBedRooms[i].ADD_TIME;
                                    //Neu la thoi gian ra buong cua buong cuoi cung thi gan lai = ngay ra vien de dam bao tien giuong khoi voi thong tin ra vien
                                    long finishTime = !treatmentBedRooms[i].REMOVE_TIME.HasValue || i == treatmentBedRooms.Count - 1 ? data.TreatmentFinishTime : treatmentBedRooms[i].REMOVE_TIME.Value;

                                    DateTime startTimeTmp = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(startTime).Value;
                                    DateTime finishTimeTmp = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(finishTime).Value;

                                    //Neu ngay vao buong = ngay ra buong truoc do
                                    if (previousFinishTime.HasValue && previousFinishTime.Value.Date == startTimeTmp.Date)
                                    {
                                        DateTime nextTmp = startTimeTmp.AddDays(1);
                                        if (nextTmp > finishTimeTmp)
                                        {
                                            startTime = finishTime;
                                        }
                                        else if (nextTmp < finishTimeTmp && nextTmp.Date == finishTimeTmp.Date)
                                        {
                                            startTime = finishTime;
                                        }
                                        else
                                        {
                                            startTime = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(nextTmp).Value;
                                        }
                                    }

                                    previousFinishTime = finishTimeTmp;

                                    HIS_PATIENT_TYPE_ALTER pta = new HisPatientTypeAlterGet().GetApplied(data.TreatmentId, startTime, ptas);

                                    //Lay giuong ko co nguoi nam trong khoang thoi gian do
                                    List<HIS_BED> availableBeds = this.GetAvailableBeds(treatmentBedRooms[i].BED_ROOM_ID, startTime, finishTime);
                                    HIS_BED bed = IsNotNullOrEmpty(availableBeds) ? availableBeds.OrderBy(o => o.BED_CODE).FirstOrDefault() : HisBedCFG.DATA.Where(o => o.BED_ROOM_ID == treatmentBedRooms[i].BED_ROOM_ID).OrderBy(o => o.BED_CODE).FirstOrDefault();
                                    V_HIS_BED_ROOM bedRoom = HisBedRoomCFG.DATA.Where(o => o.ID == treatmentBedRooms[i].BED_ROOM_ID).FirstOrDefault();

                                    HIS_BED_LOG bedLog = new HIS_BED_LOG();
                                    bedLog.TREATMENT_BED_ROOM_ID = treatmentBedRooms[i].ID;
                                    bedLog.START_TIME = startTime;
                                    bedLog.FINISH_TIME = finishTime;
                                    bedLog.BED_ID = bed.ID;
                                    bedLog.IS_SERVICE_REQ_ASSIGNED = Constant.IS_TRUE;
                                    bedLog.PATIENT_TYPE_ID = pta.PATIENT_TYPE_ID;
                                    bedLog.BED_SERVICE_TYPE_ID = service.ID;

                                    toInsertBedLogs.Add(bedLog);

                                    //Voi moi bed_log, xu ly de tao ra nhieu service_req va sere_serv
                                    //(moi 1 chi dinh tuong ung voi 1 ngay giuong)
                                    DateTime instructionTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(startTime).Value;
                                    DateTime max = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(finishTime).Value;
                                    while (instructionTime <= max)
                                    {
                                        long instr = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(instructionTime).Value;
                                        HIS_SERVICE_REQ serviceReq = this.MakeServiceReq(endRoom, endDepartment, bedRoom, instr, treatment, pta);
                                        HIS_SERE_SERV sereServ = this.MakeSereSev(service, pta.PATIENT_TYPE_ID, treatment);
                                        AutoCreateBedLog cbl = new AutoCreateBedLog();
                                        cbl.SereServ = sereServ;
                                        cbl.ServiceReq = serviceReq;
                                        cbl.BedLog = bedLog;

                                        autoCreateBedLogs.Add(cbl);

                                        DateTime next = instructionTime.AddDays(1);
                                        if (next < max && next.Date <= max.Date)
                                        {
                                            instructionTime = next;
                                        }
                                        else if (next > max && next.Date == max.Date)
                                        {
                                            instructionTime = max;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }

                                if (IsNotNullOrEmpty(toInsertBedLogs) && !this.hisBedLogCreate.CreateList(toInsertBedLogs))
                                {
                                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                                }

                                List<HIS_SERVICE_REQ> toInsertServiceReqs = new List<HIS_SERVICE_REQ>();
                                foreach (AutoCreateBedLog acb in autoCreateBedLogs)
                                {
                                    acb.ServiceReq.BED_LOG_ID = acb.BedLog.ID;
                                    toInsertServiceReqs.Add(acb.ServiceReq);
                                }

                                if (!this.hisServiceReqCreate.CreateList(toInsertServiceReqs, treatment))
                                {
                                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                                }

                                List<HIS_SERE_SERV> toInsertSereServs = new List<HIS_SERE_SERV>();

                                HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);
                                foreach (AutoCreateBedLog acb in autoCreateBedLogs)
                                {
                                    acb.SereServ.SERVICE_REQ_ID = acb.ServiceReq.ID;
                                    HisSereServUtil.SetTdl(acb.SereServ, acb.ServiceReq);
                                    if (!priceAdder.AddPrice(acb.SereServ, acb.SereServ.TDL_INTRUCTION_TIME, acb.SereServ.TDL_EXECUTE_BRANCH_ID, acb.SereServ.TDL_REQUEST_ROOM_ID, acb.SereServ.TDL_REQUEST_DEPARTMENT_ID, acb.SereServ.TDL_EXECUTE_ROOM_ID))
                                    {
                                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                                    }

                                    toInsertSereServs.Add(acb.SereServ);
                                }

                                if (!this.hisSereServCreate.CreateList(toInsertSereServs, toInsertServiceReqs, false))
                                {
                                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                                }
                                newServServs = toInsertSereServs;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.Rollback();
            }
        }

        private void Rollback()
        {
            this.hisSereServCreate.RollbackData();
            this.hisServiceReqCreate.RollbackData();
            this.hisBedLogCreate.RollbackData();
        }

        private HIS_SERE_SERV MakeSereSev(V_HIS_SERVICE service, long patientTypeId, HIS_TREATMENT treatment)
        {
            HIS_SERE_SERV sereServ = new HIS_SERE_SERV();
            sereServ.SERVICE_ID = service.ID;
            sereServ.AMOUNT = 1; //giuong luon lay so luong la 1
            sereServ.PATIENT_TYPE_ID = patientTypeId;
            if (service.OTHER_PAY_SOURCE_ID.HasValue)
            {
                if (service.OTHER_PAY_SOURCE_ICDS == null || 
                    (!string.IsNullOrWhiteSpace(treatment.ICD_CODE) && ("," + service.OTHER_PAY_SOURCE_ICDS + ",").ToLower().Contains(treatment.ICD_CODE.ToLower())))
                {
                    sereServ.OTHER_PAY_SOURCE_ID = service.OTHER_PAY_SOURCE_ID;
                }
            }
            
            return sereServ;
        }

        private HIS_SERVICE_REQ MakeServiceReq(V_HIS_ROOM endRoom, HIS_DEPARTMENT endDepartment, V_HIS_BED_ROOM bedRoom, long instructionTime, HIS_TREATMENT treatment, HIS_PATIENT_TYPE_ALTER pta)
        {
            HIS_SERVICE_REQ serviceReq = new HIS_SERVICE_REQ();
            serviceReq.REQUEST_ROOM_ID = endRoom.ID;
            serviceReq.REQUEST_DEPARTMENT_ID = endDepartment.ID;
            serviceReq.EXECUTE_DEPARTMENT_ID = bedRoom.DEPARTMENT_ID;
            serviceReq.EXECUTE_ROOM_ID = bedRoom.ROOM_ID;
            serviceReq.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__G;
            serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
            serviceReq.INTRUCTION_TIME = instructionTime;
            serviceReq.TREATMENT_ID = treatment.ID;
            serviceReq.ICD_TEXT = treatment.ICD_TEXT;//icd lay mac dinh trong treatment
            serviceReq.ICD_NAME = treatment.ICD_NAME;//icd lay mac dinh trong treatment
            serviceReq.ICD_CODE = treatment.ICD_CODE;//icd lay mac dinh trong treatment
            serviceReq.ICD_CAUSE_CODE = treatment.ICD_CAUSE_CODE;
            serviceReq.ICD_CAUSE_NAME = treatment.ICD_CAUSE_NAME;
            serviceReq.TDL_PATIENT_ID = treatment.PATIENT_ID;
            serviceReq.TREATMENT_TYPE_ID = pta.TREATMENT_TYPE_ID;
            return serviceReq;
        }

        private List<HIS_BED> GetAvailableBeds(long bedRoomId, long startTime, long finishTime)
        {
            string sql = "SELECT * FROM HIS_BED B "
                        + " WHERE B.BED_ROOM_ID = :param1 "
                        + " AND B.IS_ACTIVE = :param2 "
                        + " AND NOT EXISTS (SELECT 1 FROM HIS_BED_LOG BL "
                        + "         WHERE BL.BED_ID = B.ID AND ( "
                        + "             (BL.START_TIME <= :param3 AND (BL.FINISH_TIME IS NULL OR BL.FINISH_TIME >= :param4)) "
                        + "             OR (BL.START_TIME <= :param5 AND (BL.FINISH_TIME IS NULL OR BL.FINISH_TIME >= :param6))))";
            return DAOWorker.SqlDAO.GetSql<HIS_BED>(sql, bedRoomId, Constant.IS_TRUE, startTime, startTime, finishTime, finishTime);
        }
    }
}
