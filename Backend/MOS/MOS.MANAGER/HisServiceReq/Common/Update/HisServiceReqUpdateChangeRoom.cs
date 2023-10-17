using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using MOS.MANAGER.HisServiceReq.Exam;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.UTILITY;
using MOS.MANAGER.HisNumOrderIssue;

namespace MOS.MANAGER.HisServiceReq
{
    partial class HisServiceReqUpdateChangeRoom : BusinessBase
    {
        private HIS_SERVICE_REQ beforeUpdate;
        private HisSereServUpdate hisSereServUpdate;
        private HisTreatmentUpdate hisTreatmentUpdate;

        internal HisServiceReqUpdateChangeRoom()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqUpdateChangeRoom(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Run(HIS_SERVICE_REQ data, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                this.SetServerTime(data);
                HIS_SERVICE_REQ raw = null;
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisServiceReqExamCheck examChecker = new HisServiceReqExamCheck(param);
                List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByServiceReqId(data.ID);
                HIS_TREATMENT treatment = null;
                WorkPlaceSDO workPlace = null;
                bool valid = checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsProcessable(data.EXECUTE_ROOM_ID, sereServs);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotFinished(raw);
                valid = valid && this.HasWorkPlaceInfo(data.REQUEST_ROOM_ID, ref workPlace);
                valid = valid && this.IsAllow(workPlace, raw);
                valid = valid && treatmentChecker.IsUnLock(raw.TREATMENT_ID, ref treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                valid = valid && treatmentChecker.IsUnLockHein(treatment);
                valid = valid && (!raw.TREATMENT_TYPE_ID.HasValue || examChecker.IsNotExceedLimit(data.EXECUTE_ROOM_ID, raw.TREATMENT_ID, raw.TREATMENT_TYPE_ID.Value, raw.INTRUCTION_TIME));
                if (valid)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    this.beforeUpdate = Mapper.Map<HIS_SERVICE_REQ>(raw);

                    V_HIS_ROOM newRoom = HisRoomCFG.DATA.Where(o => o.ID == data.EXECUTE_ROOM_ID).FirstOrDefault();
                    V_HIS_ROOM oldRoom = HisRoomCFG.DATA.Where(o => o.ID == raw.EXECUTE_ROOM_ID).FirstOrDefault();
                    raw.EXECUTE_ROOM_ID = newRoom.ID;
                    raw.EXECUTE_DEPARTMENT_ID = newRoom.DEPARTMENT_ID;
                    raw.PRIORITY = data.PRIORITY.HasValue ? data.PRIORITY : 0;
                    raw.INTRUCTION_TIME = data.INTRUCTION_TIME;

                    //Neu y lenh cu co chi dinh nguoi xu ly thi can cap nhat lai thong tin nguoi xu ly theo phong moi
                    if (!string.IsNullOrWhiteSpace(raw.ASSIGNED_EXECUTE_LOGINNAME))
                    {
                        raw.ASSIGNED_EXECUTE_LOGINNAME = newRoom.RESPONSIBLE_LOGINNAME;
                        raw.ASSIGNED_EXECUTE_USERNAME = newRoom.RESPONSIBLE_USERNAME;
                    }
                    
                    if (newRoom.ID != oldRoom.ID)
                    {
                        //Cap STT kham
                        new HisNumOrderIssueUtil(param).SetNumOrder(raw, null, null, null);
                    }
                    
                    HisServiceReqNumOrderBase.SetNumOrderBase(raw);

                    if (!DAOWorker.HisServiceReqDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceReq that bai." + LogUtil.TraceData("data", data));
                    }

                    this.ProcessTreatment(raw, treatment);
                    this.ProcessSereServ(sereServs, treatment);

                    //Goi lai de lay du lieu num_order (trong truong hop trigger DB sinh ra)
                    resultData = new HisServiceReqGet().GetById(raw.ID);
                    result = true;

                    new EventLogGenerator(EventLog.Enum.HisServiceReq_DoiPhongThucHien, oldRoom.ROOM_NAME, oldRoom.ROOM_CODE, newRoom.ROOM_NAME, newRoom.ROOM_CODE)
                            .TreatmentCode(raw.TDL_TREATMENT_CODE)
                            .ServiceReqCode(raw.SERVICE_REQ_CODE)
                            .Run();
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

        private void ProcessSereServ(List<HIS_SERE_SERV> sereServs, HIS_TREATMENT treatment)
        {
            if (IsNotNullOrEmpty(sereServs))
            {
                //cap nhat lai gia, tranh truong hop doi phong xu ly dan den thay doi chinh sach gia
                //Khong can cap nhat TDL_EXECUTE_ROOM_ID, TDL_EXECUTE_DEPARTMENT_ID vi da duoc update bang trigger khi update service_req 
                if (!new HisSereServUpdateHein(param, treatment, false).UpdateDb())
                {
                    throw new Exception("Cap nhat lai thong tin gia sere_serv that bai");
                }
            }
        }

        /// <summary>
        /// Cap nhat thong tin du thua du lieu TDL_FIRST_EXAM_ROOM_ID cua his_treatment
        /// </summary>
        /// <param name="serviceReq"></param>
        /// <param name="treatment"></param>
        private void ProcessTreatment(HIS_SERVICE_REQ serviceReq, HIS_TREATMENT treatment)
        {
            try
            {
                //neu chi dinh la chi dinh kham thi xu ly nghiep vu de cap nhat TDL_FIRST_EXAM_ROOM_ID cua treatment
                if (serviceReq != null && serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                {
                    //neu co su thay doi du lieu va cong kham la cong kham chinh thi moi tiep tuc xu ly
                    if (treatment.TDL_FIRST_EXAM_ROOM_ID != serviceReq.EXECUTE_ROOM_ID && serviceReq.IS_MAIN_EXAM == Constant.IS_TRUE)
                    {
                        Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                        HIS_TREATMENT beforeUpdate = Mapper.Map<HIS_TREATMENT>(treatment);
                        treatment.TDL_FIRST_EXAM_ROOM_ID = serviceReq.EXECUTE_ROOM_ID;
                        if (!this.hisTreatmentUpdate.Update(treatment, beforeUpdate))
                        {
                            LogSystem.Warn("Cap nhat TDL_FIRST_EXAM_ROOM_ID cho HIS_TREATMENT that bai ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private bool IsAllow(WorkPlaceSDO workPlace, HIS_SERVICE_REQ raw)
        {
            try
            {
                if (workPlace == null || (raw.EXECUTE_ROOM_ID != workPlace.RoomId && raw.REQUEST_ROOM_ID != workPlace.RoomId))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChiChoPhepChuyenPhongDoiVoiYcDoPhongMinhXuLy);
                    return false;
                }

                //Ko cho phep chuyen phong doi voi y/c da hoan thanh
                if (raw.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongChoPhepChuyenPhongVoiYeuCauDaHoanThanh, raw.SERVICE_REQ_CODE);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        private void Rollback()
        {
            try
            {
                if (this.beforeUpdate != null)
                {
                    if (!DAOWorker.HisServiceReqDAO.Update(this.beforeUpdate))
                    {

                        LogSystem.Warn("Rollback that bai");
                    }
                }

                this.hisSereServUpdate.RollbackData();
                this.hisTreatmentUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void SetServerTime(HIS_SERVICE_REQ data)
        {
            //Neu cau hinh su dung gio server thi gan lai theo gio server cac du lieu thoi gian truyen len
            if (SystemCFG.IS_USING_SERVER_TIME && data != null)
            {
                long now = Inventec.Common.DateTime.Get.Now().Value;
                data.INTRUCTION_TIME = now;
            }
        }
    }
}
