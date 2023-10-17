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

namespace MOS.MANAGER.HisServiceReq.Update.ChangeListRoom
{
    partial class HisServiceReqUpdateChangeListRoom : BusinessBase
    {
        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisServiceReqUpdate hisServiceReqUpdate;
        private List<HisSereServUpdateHein> hisSereServUpdateHeins;

        internal HisServiceReqUpdateChangeListRoom()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqUpdateChangeListRoom(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisSereServUpdateHeins = new List<HisSereServUpdateHein>();
        }

        internal bool Run(ChangeRoomSDO data)
        {
            bool result = false;
            try
            {
                HisServiceReqUpdateChangeListRoomCheck checker = new HisServiceReqUpdateChangeListRoomCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisServiceReqExamCheck examChecker = new HisServiceReqExamCheck(param);
                List<HIS_SERVICE_REQ> serviceReqs = null;
                List<HIS_SERVICE_REQ> oldServiceReqs = null;
                List<HIS_SERE_SERV> sereServs = null;
                List<HIS_TREATMENT> treatments = null;
                WorkPlaceSDO workPlace = null;
                bool valid = true;
                valid = valid && checker.IsProcessable(data, ref sereServs);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && checker.IsAllow(workPlace, data, ref serviceReqs, ref treatments);
                
                if (valid)
                {
                    this.ProcessServiceReq(serviceReqs, data.ExecuteRoomId, ref oldServiceReqs);
                    this.ProcessTreatment(serviceReqs, treatments);
                    this.ProcessSereServ(sereServs, treatments);
                    result = true;

                    foreach (HIS_SERVICE_REQ serviceReq in serviceReqs)
                    {
                        HIS_SERVICE_REQ old = oldServiceReqs != null ? oldServiceReqs.Where(o => o.ID == serviceReq.ID).FirstOrDefault() : null;
                        if (old != null)
                        {
                            V_HIS_ROOM oldRoom = HisRoomCFG.DATA.Where(o => o.ID == old.EXECUTE_ROOM_ID).FirstOrDefault();
                            V_HIS_ROOM newRoom = HisRoomCFG.DATA.Where(o => o.ID == serviceReq.EXECUTE_ROOM_ID).FirstOrDefault();

                            new EventLogGenerator(EventLog.Enum.HisServiceReq_DoiPhongThucHien, oldRoom.ROOM_NAME, oldRoom.ROOM_CODE, newRoom.ROOM_NAME, newRoom.ROOM_CODE)
                                .TreatmentCode(serviceReq.TDL_TREATMENT_CODE)
                                .ServiceReqCode(serviceReq.SERVICE_REQ_CODE)
                                .Run();
                        }
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

        private void ProcessServiceReq(List<HIS_SERVICE_REQ> serviceReqs, long newExecuteRoomId, ref List<HIS_SERVICE_REQ> befores)
        {
            if (IsNotNullOrEmpty(serviceReqs))
            {
                V_HIS_ROOM newRoom = HisRoomCFG.DATA.Where(o => o.ID == newExecuteRoomId).FirstOrDefault();

                Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                befores = Mapper.Map<List<HIS_SERVICE_REQ>>(serviceReqs);

                foreach (HIS_SERVICE_REQ serviceReq in serviceReqs)
                {
                    long oldRoomId = serviceReq.EXECUTE_ROOM_ID;

                    serviceReq.EXECUTE_ROOM_ID = newRoom.ID;
                    serviceReq.EXECUTE_DEPARTMENT_ID = newRoom.DEPARTMENT_ID;

                    //Neu y lenh cu co chi dinh nguoi xu ly thi can cap nhat lai thong tin nguoi xu ly theo phong moi
                    if (!string.IsNullOrWhiteSpace(serviceReq.ASSIGNED_EXECUTE_LOGINNAME))
                    {
                        serviceReq.ASSIGNED_EXECUTE_LOGINNAME = newRoom.RESPONSIBLE_LOGINNAME;
                        serviceReq.ASSIGNED_EXECUTE_USERNAME = newRoom.RESPONSIBLE_USERNAME;
                    }

                    HisServiceReqNumOrderBase.SetNumOrderBase(serviceReq);

                    if (newRoom.ID != oldRoomId)
                    {
                        //Cap lai STT kham
                        new HisNumOrderIssueUtil(param).SetNumOrder(serviceReq, null, null, null);
                    }
                }

                if (!this.hisServiceReqUpdate.UpdateList(serviceReqs, befores))
                {
                    throw new Exception("Cap nhat HIS_SERVICE_REQ that bai.");
                }
            }
        }

        private void ProcessSereServ(List<HIS_SERE_SERV> sereServs, List<HIS_TREATMENT> treatments)
        {
            if (IsNotNullOrEmpty(sereServs) && IsNotNullOrEmpty(treatments))
            {
                foreach(HIS_TREATMENT treatment in treatments)
                {
                    HisSereServUpdateHein updateHein = new HisSereServUpdateHein(param, treatment, false);
                    this.hisSereServUpdateHeins.Add(updateHein);

                    //Cap nhat lai gia, tranh truong hop doi phong xu ly dan den thay doi chinh sach gia
                    //Khong can cap nhat TDL_EXECUTE_ROOM_ID, TDL_EXECUTE_DEPARTMENT_ID vi da duoc update bang trigger khi update service_req
                    if (!updateHein.UpdateDb())
                    {
                        throw new Exception("Cap nhat lai thong tin gia sere_serv that bai");
                    }
                }
            }
        }

        /// <summary>
        /// Cap nhat thong tin du thua du lieu TDL_FIRST_EXAM_ROOM_ID cua his_treatment
        /// </summary>
        /// <param name="serviceReq"></param>
        /// <param name="treatment"></param>
        private void ProcessTreatment(List<HIS_SERVICE_REQ> serviceReqs, List<HIS_TREATMENT> treatments)
        {
            try
            {
                if (IsNotNullOrEmpty(serviceReqs) && IsNotNullOrEmpty(treatments))
                {
                    Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();

                    List<HIS_TREATMENT> toUpdates = new List<HIS_TREATMENT>();
                    List<HIS_TREATMENT> befores = new List<HIS_TREATMENT>();

                    foreach (HIS_SERVICE_REQ serviceReq in serviceReqs)
                    {
                        //neu chi dinh la kham chinh thi xu ly nghiep vu de cap nhat TDL_FIRST_EXAM_ROOM_ID cua treatment
                        if (serviceReq != null && serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH && serviceReq.IS_MAIN_EXAM == Constant.IS_TRUE)
                        {
                            HIS_TREATMENT treatment = treatments.Where(o => o.ID == serviceReq.TREATMENT_ID).FirstOrDefault();

                            //neu co su thay doi du lieu va cong kham la cong kham chinh thi moi tiep tuc xu ly
                            if (treatment != null && treatment.TDL_FIRST_EXAM_ROOM_ID != serviceReq.EXECUTE_ROOM_ID)
                            {
                                HIS_TREATMENT before = Mapper.Map<HIS_TREATMENT>(treatment);
                                treatment.TDL_FIRST_EXAM_ROOM_ID = serviceReq.EXECUTE_ROOM_ID;

                                befores.Add(before);
                                toUpdates.Add(treatment);
                            }
                        }
                    }

                    if (IsNotNullOrEmpty(toUpdates) && !this.hisTreatmentUpdate.UpdateList(toUpdates, befores))
                    {
                        LogSystem.Warn("Cap nhat TDL_FIRST_EXAM_ROOM_ID cho HIS_TREATMENT that bai ");
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.hisSereServUpdateHeins))
                {
                    foreach (HisSereServUpdateHein update in this.hisSereServUpdateHeins)
                    {
                        update.RollbackData();
                    }
                }
                this.hisServiceReqUpdate.RollbackData();
                this.hisTreatmentUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
