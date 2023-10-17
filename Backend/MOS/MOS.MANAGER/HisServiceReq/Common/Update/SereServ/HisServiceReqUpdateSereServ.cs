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
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisService;
using AutoMapper;
using MOS.MANAGER.EventLogUtil;
using MOS.UTILITY;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisServiceReq.Test;
using MOS.MANAGER.HisSereServ.Delete;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisTreatment.Util;
using MOS.MANAGER.HisServiceReq.Test.LisSenderV1;
using MOS.MANAGER.HisTransReq.CreateByService;

namespace MOS.MANAGER.HisServiceReq.Common.Update.SereServ
{
    partial class HisServiceReqUpdateSereServ : BusinessBase
    {
        private HisSereServUpdateHein hisSereServUpdateHein;
        private HisSereServCreate hisSereServCreate;
        private HisSereServUpdate hisSereServUpdate;
        private HisSereServDeleteSql hisSereServDelete;
        private HIS_SERVICE_REQ beforeUpdateServiceReq;
        private HisSereServExtCreate hisSereServExtCreate;
        private HisSereServExtUpdate hisSereServExtUpdate;
        private HisServiceReqTestCreate hisServiceReqTestCreate;

        Dictionary<ServiceReqDetailSDO, HIS_SERE_SERV> dicMapSereServ = new Dictionary<ServiceReqDetailSDO, HIS_SERE_SERV>();

        internal HisServiceReqUpdateSereServ()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqUpdateSereServ(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            //Nghiep vu xu ly o day luon thuc hien sau nghiep vu tao service_req
            this.hisSereServCreate = new HisSereServCreate(param);
            this.hisSereServDelete = new HisSereServDeleteSql(param);
            this.hisSereServExtCreate = new HisSereServExtCreate(param);
            this.hisSereServExtUpdate = new HisSereServExtUpdate(param);
            this.hisServiceReqTestCreate = new HisServiceReqTestCreate(param);
            this.hisSereServUpdate = new HisSereServUpdate(param);
        }

        /// <summary>
        /// Xu ly de tao yeu cau dich vu dua vao danh sach ServiceReqDetailSDO
        /// </summary>
        /// <param name="serviceReqDetails"></param>
        /// <param name="returnHisServiceReqs"></param>
        /// <returns></returns>
        internal bool Update(HisServiceReqUpdateSDO data, ref HisServiceReqUpdateResultSDO resultData, ref ServiceReqDetailSDO sdo)
        {
            bool result = false;
            try
            {
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisServiceReqUpdateSereServCheck updateChecker = new HisServiceReqUpdateSereServCheck(param);
                HisServiceReqCheck checker = new HisServiceReqCheck(param);

                List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByServiceReqId(data.ServiceReqId);
                bool valid = true;
                HIS_SERVICE_REQ raw = null;
                HIS_TREATMENT hisTreatment = null;

                valid = valid && checker.VerifyId(data.ServiceReqId, ref raw);
                valid = valid && (!IsNotNullOrEmpty(data.InsertServices) || checker.IsMatchingServiceTypeId(data.InsertServices, raw.SERVICE_REQ_TYPE_ID, raw.EXE_SERVICE_MODULE_ID));
                valid = valid && updateChecker.IsValidData(data.UpdateServices);
                valid = valid && updateChecker.IsAllowed(raw, hisSereServs, data);
                valid = valid && updateChecker.IsValidAmount(raw, hisSereServs, data.DeleteSereServIds, data.InsertServices);
                valid = valid && checker.HasNoTempBed(hisSereServs);
                valid = valid && treatmentChecker.IsUnLock(raw.TREATMENT_ID, ref hisTreatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(hisTreatment);
                valid = valid && treatmentChecker.IsUnpause(hisTreatment);
                valid = valid && treatmentChecker.IsUnLockHein(hisTreatment);
                valid = valid && checker.IsValidBHYTServices(data.InsertServices);
                valid = valid && checker.IsValidBHYTServices(data.UpdateServices);
                valid = valid && updateChecker.IsValidDoNotUseBHYT(data.InsertServices);
                valid = valid && updateChecker.IsValidDoNotUseBHYT(data.UpdateServices);
                valid = valid && updateChecker.IsValidSampleTime(raw);

                if (valid)
                {
                    List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(raw.TREATMENT_ID);

                    HIS_PATIENT_TYPE_ALTER usingPta = ptas
                        .Where(o => o.LOG_TIME <= raw.INTRUCTION_TIME)
                        .OrderByDescending(o => o.LOG_TIME)
                        .ThenByDescending(o => o.ID)
                        .FirstOrDefault();

                    List<HIS_SERE_SERV> createSereServs = new List<HIS_SERE_SERV>();
                    List<HIS_SERE_SERV> DeleteSereServs = new List<HIS_SERE_SERV>();
                    ///Can goi process "delete" sau "create" de tranh truong hop create that bai,
                    ///ko rollback duoc du lieu da delete
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();

                    HIS_SERVICE_REQ oldServiceReq = Mapper.Map<HIS_SERVICE_REQ>(raw);
                    List<HIS_SERE_SERV> oldSereServs = Mapper.Map<List<HIS_SERE_SERV>>(hisSereServs);

                    this.ProcessExecuteRoom(data, raw, hisSereServs);
                    this.CreateSereServ(hisTreatment, usingPta, data.InsertServices, raw, ref createSereServs);
                    this.ProcessSereServExt(raw, data.UpdateServices, hisSereServs);
                    this.ProcessUpdateSereServ(hisTreatment, usingPta, raw, data.UpdateServices, hisSereServs);
                    this.CreateTestServiceReq(raw, createSereServs);
                    this.DeleteSereServ(data.DeleteSereServIds, raw, ref DeleteSereServs);
                    this.ProcessServiceReq(raw, oldSereServs, createSereServs, data.DeleteSereServIds, data);

                    //Cap nhat ti le BHYT cho sere_serv: chi thuc hien khi co y/c, tranh thuc hien nhieu lan, giam hieu nang
                    this.hisSereServUpdateHein = new HisSereServUpdateHein(param, hisTreatment, ptas, false);
                    if (!this.hisSereServUpdateHein.UpdateDb())
                    {
                        throw new Exception("Du lieu se bi rollback. Ket thuc nghiep vu");
                    }

                    // Tạo yêu cầu thanh toán chi tiết dịch vụ
                    List<HIS_SERE_SERV> updateSereServs = new HisSereServGet().GetByServiceReqId(data.ServiceReqId);
                    updateSereServs = updateSereServs.Where(o => !DeleteSereServs.Exists(p => p.ID == o.ID)).ToList();
                    bool isChangePrice = false;
                    foreach (var ss in oldSereServs)
                    {
                        var itemUpdate = updateSereServs.FirstOrDefault(o => o.ID == ss.ID);
                        if (!IsNotNull(itemUpdate))
                        {
                            isChangePrice = true;
                            break;
                        }
                        if (((ss.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT) && (itemUpdate.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
                            || ((ss.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT) && (itemUpdate.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
                            || (ss.VIR_TOTAL_PATIENT_PRICE != itemUpdate.VIR_TOTAL_PATIENT_PRICE))
                        {
                            isChangePrice = true;
                            break;
                        }
                    }

                    List<HIS_SERVICE_REQ> serviceReqs = new List<HIS_SERVICE_REQ>();
                    serviceReqs.Add(raw);

                    if ((IsNotNullOrEmpty(createSereServs) && createSereServs.Exists(o => o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && o.VIR_TOTAL_PATIENT_PRICE > 0)) || IsNotNullOrEmpty(DeleteSereServs) || isChangePrice)
                    {
                        WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(raw.REQUEST_ROOM_ID);
                        if (workPlace != null)
                        {
                            if (!new HisTransReqCreateByService(param).Run(hisTreatment, serviceReqs, workPlace))
                            {
                                Inventec.Common.Logging.LogSystem.Error("Tao HisTransReq that bai");
                                Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param));
                            }
                        }
                    }

                    //Kiem tra co vuot qua 6 thang luong co ban khong
                    this.ProcessCheckBaseSalary(hisTreatment, ptas);

                    this.PassResult(data.ServiceReqId, ref resultData);

                    HisServiceReqLog.Run(oldServiceReq, oldSereServs, resultData.ServiceReq, resultData.SereServs, LibraryEventLog.EventLog.Enum.HisServiceReq_SuaYLenh);
                    result = true;

                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
                resultData = null;
            }
            return result;
        }

        private void RollbackData()
        {
            if (this.hisSereServUpdateHein != null)
            {
                this.hisSereServUpdateHein.RollbackData();
            }
            if (this.beforeUpdateServiceReq != null)
            {
                if (!DAOWorker.HisServiceReqDAO.Update(this.beforeUpdateServiceReq))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                    throw new Exception("Rollback thong tin HisServiceReq that bai." + LogUtil.TraceData("data", this.beforeUpdateServiceReq));
                }
            }
            this.hisSereServUpdate.RollbackData();
            this.hisSereServExtUpdate.RollbackData();
            this.hisSereServExtCreate.RollbackData();
            this.hisSereServCreate.RollbackData();
            this.hisSereServDelete.Rollback();
        }

        private void PassResult(long serviceReqId, ref HisServiceReqUpdateResultSDO resultData)
        {
            resultData = new HisServiceReqUpdateResultSDO();
            resultData.SereServs = new HisSereServGet().GetViewByServiceReqId(serviceReqId);
            resultData.ServiceReq = new HisServiceReqGet().GetViewById(serviceReqId);
        }

        /// <summary>
        /// Them sere_serv tuong ung voi service_req
        /// </summary>
        /// <param name="list"></param>
        private void CreateSereServ(HIS_TREATMENT hisTreatment, HIS_PATIENT_TYPE_ALTER usingPta, List<ServiceReqDetailSDO> serviceReqDetails, HIS_SERVICE_REQ raw, ref List<HIS_SERE_SERV> createSereServs)
        {
            if (IsNotNullOrEmpty(serviceReqDetails))
            {
                HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, hisTreatment, null, null);
                List<HIS_SERE_SERV> hisSereServs = new List<HIS_SERE_SERV>();
                HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == raw.EXECUTE_DEPARTMENT_ID).FirstOrDefault();
                //Tao danh sach sere_serv tuong ung voi service_req
                foreach (ServiceReqDetailSDO req in serviceReqDetails)
                {
                    HIS_SERE_SERV sereServ = new HIS_SERE_SERV();
                    sereServ.SERVICE_ID = req.ServiceId;
                    sereServ.SERVICE_REQ_ID = raw.ID;
                    sereServ.AMOUNT = req.Amount;
                    sereServ.PARENT_ID = req.ParentId;
                    sereServ.PATIENT_TYPE_ID = req.PatientTypeId;
                    sereServ.IS_EXPEND = req.IsExpend;
                    sereServ.IS_OUT_PARENT_FEE = req.IsOutParentFee;
                    sereServ.PACKAGE_ID = req.PackageId;
                    sereServ.OTHER_PAY_SOURCE_ID = hisTreatment.OTHER_PAY_SOURCE_ID;
                    sereServ.IS_NOT_USE_BHYT = req.IsNotUseBhyt ? (short?)Constant.IS_TRUE : null;
                    //Chi gan doi tuong phu thu khi bat cau hinh
                    if (HisSereServCFG.SET_PRIMARY_PATIENT_TYPE == HisSereServCFG.SetPrimaryPatientType.AUTO
                        || HisSereServCFG.SET_PRIMARY_PATIENT_TYPE == HisSereServCFG.SetPrimaryPatientType.YES)
                    {
                        sereServ.PRIMARY_PATIENT_TYPE_ID = req.PrimaryPatientTypeId;
                    }

                    //can set thua du lieu o day luon do phan xu ly ti le thanh toan 
                    //co su dung cac truong thua du lieu
                    HisSereServUtil.SetTdl(sereServ, raw);

                    if (!priceAdder.AddPrice(sereServ, raw.INTRUCTION_TIME, department.BRANCH_ID, raw.REQUEST_ROOM_ID, raw.REQUEST_DEPARTMENT_ID, raw.EXECUTE_ROOM_ID))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                    hisSereServs.Add(sereServ);
                    dicMapSereServ[req] = sereServ;
                }

                //Insert thong tin sere_serv vao CSDL
                if (!this.hisSereServCreate.CreateList(hisSereServs, raw, false))
                {
                    throw new Exception("Du lieu se bi rollback. Ket thuc nghiep vu");
                }
                createSereServs = hisSereServs;
            }
        }

        private void ProcessUpdateSereServ(HIS_TREATMENT treatment, HIS_PATIENT_TYPE_ALTER usingPta, HIS_SERVICE_REQ raw, List<ServiceReqDetailSDO> updates, List<HIS_SERE_SERV> hisSereServs)
        {
            if (IsNotNullOrEmpty(updates) && IsNotNullOrEmpty(hisSereServs) && treatment != null)
            {
                HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);
                List<HIS_SERE_SERV> toUpdates = new List<HIS_SERE_SERV>();
                List<HIS_SERE_SERV> befores = new List<HIS_SERE_SERV>();
                Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();

                foreach (ServiceReqDetailSDO sdo in updates)
                {
                    HIS_SERE_SERV s = hisSereServs.Where(o => sdo.SereServId == o.ID).FirstOrDefault();
                    HIS_SERE_SERV before = Mapper.Map<HIS_SERE_SERV>(s);


                    //Neu co su thay doi thong tin thi bo sung vao danh sach can cap nhat
                    if (sdo != null && (s.IS_OUT_PARENT_FEE != sdo.IsOutParentFee || s.IS_EXPEND != sdo.IsExpend || s.PATIENT_TYPE_ID != sdo.PatientTypeId || s.AMOUNT != sdo.Amount || s.PRIMARY_PATIENT_TYPE_ID != sdo.PrimaryPatientTypeId || s.IS_NOT_USE_BHYT != (sdo.IsNotUseBhyt ? (short?)Constant.IS_TRUE : null)))
                    {
                        s.AMOUNT = sdo.Amount;
                        s.PATIENT_TYPE_ID = sdo.PatientTypeId;
                        s.IS_OUT_PARENT_FEE = sdo.IsOutParentFee;
                        s.IS_EXPEND = sdo.IsExpend;
                        s.IS_NOT_USE_BHYT = sdo.IsNotUseBhyt ? (short?)Constant.IS_TRUE : null;
                        //Chi gan doi tuong phu thu khi bat cau hinh
                        if (HisSereServCFG.SET_PRIMARY_PATIENT_TYPE == HisSereServCFG.SetPrimaryPatientType.AUTO
                            || HisSereServCFG.SET_PRIMARY_PATIENT_TYPE == HisSereServCFG.SetPrimaryPatientType.YES)
                        {
                            s.PRIMARY_PATIENT_TYPE_ID = sdo.PrimaryPatientTypeId;
                        }

                        //can set thua du lieu o day luon do phan xu ly ti le thanh toan 
                        //co su dung cac truong thua du lieu
                        HisSereServUtil.SetTdl(s, raw);

                        if (!priceAdder.AddPrice(s, s.TDL_INTRUCTION_TIME, s.TDL_EXECUTE_BRANCH_ID, s.TDL_REQUEST_ROOM_ID, s.TDL_REQUEST_DEPARTMENT_ID, s.TDL_EXECUTE_ROOM_ID))
                        {
                            throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                        }
                        toUpdates.Add(s);
                        befores.Add(before);
                    }
                }

                if (IsNotNullOrEmpty(toUpdates))
                {
                    if (!this.hisSereServUpdate.UpdateList(toUpdates, befores, false))
                    {
                        throw new Exception("Cap nhat sere_serv that bai");
                    }
                }
            }
        }

        /// <summary>
        /// Xoa sere_serv thuoc service_req
        /// </summary>
        /// <param name="sereServIds"></param>
        private void DeleteSereServ(List<long> sereServIds, HIS_SERVICE_REQ raw, ref List<HIS_SERE_SERV> DeleteSereServs)
        {
            if (IsNotNullOrEmpty(sereServIds))
            {
                List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByIds(sereServIds);
                if (IsNotNullOrEmpty(sereServs))
                {
                    List<HIS_SERE_SERV> hasExecuteds = sereServs
                        .Where(o => o.EXECUTE_TIME.HasValue).ToList();
                    if (IsNotNullOrEmpty(hasExecuteds))
                    {
                        string serviceNameStr = string.Join(",", hasExecuteds.Select(o => o.TDL_SERVICE_NAME).ToList());
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuDaXuLyKhongChoPhepHuy, serviceNameStr);
                        throw new Exception();
                    }

                    //Kiem tra xem co sere_serv nao tuong ung voi y/c xet nghiem da co ket qua khong
                    List<HIS_SERE_SERV_TEIN> sereServTeins = new HisSereServTeinGet().GetBySereServIds(sereServIds);
                    List<HIS_SERE_SERV_TEIN> hasValueSereServTeins = sereServTeins != null ? sereServTeins.Where(o => !string.IsNullOrWhiteSpace(o.VALUE)).ToList() : null;
                    if (IsNotNullOrEmpty(hasValueSereServTeins))
                    {
                        List<string> serviceNames = sereServs.Where(o => hasValueSereServTeins.Where(t => t.SERE_SERV_ID == o.ID).Any()).Select(o => o.TDL_SERVICE_NAME).ToList();
                        string serviceNameStr = string.Join(",", serviceNames); ;
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_YeuCauXetNghiemDaCoKetQuaKhongChoPhepHuy, serviceNameStr);
                        throw new Exception();
                    }

                    if (!this.hisSereServDelete.Run(sereServs))
                    {
                        throw new Exception("Xoa du lieu sere_serv that bai." + LogUtil.TraceData("sereServs", sereServs));
                    }
                    DeleteSereServs = sereServs;
                }
            }
        }

        private void ProcessExecuteRoom(HisServiceReqUpdateSDO data, HIS_SERVICE_REQ raw, List<HIS_SERE_SERV> hisSereServs)
        {
            long newExecuteRoomId = data.ExecuteRoomId ?? raw.EXECUTE_ROOM_ID;

            //kiem tra xem cac dich vu cu va moi co the thuc hien o phong xu ly moi hay khong
            //lay cac dich vu cu va ko nam trong danh sach y/c xoa
            List<long> serviceIds = new List<long>();
            if (IsNotNullOrEmpty(hisSereServs))
            {
                List<long> oldServiceIds = hisSereServs
                    .Where(o => data.DeleteSereServIds == null || !data.DeleteSereServIds.Contains(o.ID))
                    .Select(o => o.SERVICE_ID).ToList();
                serviceIds.AddRange(oldServiceIds);
            }

            if (IsNotNullOrEmpty(data.InsertServices))
            {
                //thiet lap phong xu ly cua cac dich vu moi la phong xu ly cua phieu chi dinh
                data.InsertServices.ForEach(o => o.RoomId = newExecuteRoomId);
                List<long> news = data.InsertServices.Select(o => o.ServiceId).ToList();
                serviceIds.AddRange(news);
            }

            List<long> invalids = serviceIds
                .Where(o => !HisServiceRoomCFG.DATA_VIEW.Where(t => t.SERVICE_ID == o && t.ROOM_ID == newExecuteRoomId && t.IS_ACTIVE == Constant.IS_TRUE).Any())
                .ToList();

            if (IsNotNullOrEmpty(invalids))
            {
                List<string> serviceNames = HisServiceCFG.DATA_VIEW.Where(o => invalids.Contains(o.ID)).Select(o => o.SERVICE_NAME).ToList();
                string serviceNameStr = string.Join(",", serviceNames);
                string roomName = HisExecuteRoomCFG.DATA.Where(o => newExecuteRoomId == o.ROOM_ID).Select(o => o.EXECUTE_ROOM_NAME).FirstOrDefault();

                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DichVuSauKhongTheXuLyTaiPhong, serviceNameStr, roomName);
                throw new Exception();
            }

            if (raw.EXECUTE_ROOM_ID != newExecuteRoomId)
            {
                Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                this.beforeUpdateServiceReq = Mapper.Map<HIS_SERVICE_REQ>(raw);
                V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == newExecuteRoomId).FirstOrDefault();
                raw.EXECUTE_ROOM_ID = newExecuteRoomId;
                raw.EXECUTE_DEPARTMENT_ID = room.DEPARTMENT_ID;
                //Neu y lenh cu co chi dinh nguoi xu ly thi can cap nhat lai thong tin nguoi xu ly theo phong moi
                if (!string.IsNullOrWhiteSpace(raw.ASSIGNED_EXECUTE_LOGINNAME))
                {
                    raw.ASSIGNED_EXECUTE_LOGINNAME = room.RESPONSIBLE_LOGINNAME;
                    raw.ASSIGNED_EXECUTE_USERNAME = room.RESPONSIBLE_USERNAME;
                }
            }
        }

        private void ProcessSereServExt(HIS_SERVICE_REQ serviceReq, List<ServiceReqDetailSDO> updateServices, List<HIS_SERE_SERV> hisSereServs)
        {
            List<HIS_SERE_SERV_EXT> beforeUpdates = new List<HIS_SERE_SERV_EXT>();
            List<HIS_SERE_SERV_EXT> updateds = new List<HIS_SERE_SERV_EXT>();
            List<HIS_SERE_SERV_EXT> inserteds = new List<HIS_SERE_SERV_EXT>();
            Mapper.CreateMap<HIS_SERE_SERV_EXT, HIS_SERE_SERV_EXT>();
            List<HIS_SERE_SERV_EXT> ssExts = new HisSereServExtGet().GetByServiceReqId(serviceReq.ID);
            ssExts = ssExts != null ? ssExts.OrderByDescending(o => o.ID).ToList() : null;
            if (dicMapSereServ != null && dicMapSereServ.Count > 0)
            {
                foreach (var item in dicMapSereServ)
                {
                    HIS_SERE_SERV ss = item.Value;
                    ServiceReqDetailSDO sdo = item.Key;
                    HIS_SERE_SERV_EXT exists = ssExts != null ? ssExts.FirstOrDefault(o => o.SERE_SERV_ID == ss.ID) : null;
                    if (exists != null)
                    {
                        if ((exists.INSTRUCTION_NOTE ?? "") != (sdo.InstructionNote ?? ""))
                        {
                            beforeUpdates.Add(Mapper.Map<HIS_SERE_SERV_EXT>(exists));
                            exists.INSTRUCTION_NOTE = sdo.InstructionNote;
                            updateds.Add(exists);
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrWhiteSpace(sdo.InstructionNote))
                        {
                            exists = new HIS_SERE_SERV_EXT();
                            exists.SERE_SERV_ID = ss.ID;
                            exists.INSTRUCTION_NOTE = sdo.InstructionNote;
                            HisSereServExtUtil.SetTdl(exists, ss);
                            inserteds.Add(exists);
                        }
                    }
                }
            }

            if (IsNotNullOrEmpty(updateServices))
            {
                foreach (ServiceReqDetailSDO sdo in updateServices)
                {
                    HIS_SERE_SERV ss = hisSereServs != null ? hisSereServs.FirstOrDefault(o => o.ID == sdo.SereServId.Value) : null;
                    if (ss == null)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("ServiceReqDetailSDO.SereServId khong chinh xac: " + LogUtil.TraceData("Sdo", sdo));
                    }
                    HIS_SERE_SERV_EXT exists = ssExts != null ? ssExts.FirstOrDefault(o => o.SERE_SERV_ID == sdo.SereServId.Value) : null;
                    if (exists != null)
                    {
                        if ((exists.INSTRUCTION_NOTE ?? "") != (sdo.InstructionNote ?? ""))
                        {
                            beforeUpdates.Add(Mapper.Map<HIS_SERE_SERV_EXT>(exists));
                            exists.INSTRUCTION_NOTE = sdo.InstructionNote;
                            updateds.Add(exists);
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrWhiteSpace(sdo.InstructionNote))
                        {
                            exists = new HIS_SERE_SERV_EXT();
                            exists.SERE_SERV_ID = ss.ID;
                            exists.INSTRUCTION_NOTE = sdo.InstructionNote;
                            HisSereServExtUtil.SetTdl(exists, ss);
                            inserteds.Add(exists);
                        }
                    }
                }
            }

            if (IsNotNullOrEmpty(inserteds))
            {
                if (!this.hisSereServExtCreate.CreateList(inserteds))
                {
                    throw new Exception("Khong tao duoc HIS_SERE_SERV_EXT");
                }
            }

            if (IsNotNullOrEmpty(updateds))
            {
                if (!this.hisSereServExtUpdate.UpdateList(updateds, beforeUpdates))
                {
                    throw new Exception("Khong sua duoc HIS_SERE_SERV_EXT");
                }
            }
        }

        private void CreateTestServiceReq(HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> createSereServs)
        {
            //Kiem tra xem co y/c xet nghiem ko, neu co thi bo sung insert
            if (serviceReq.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN)
            {
                return;
            }

            if (IsNotNullOrEmpty(createSereServs))
            {
                if (!this.hisServiceReqTestCreate.Create(serviceReq, createSereServs))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
            }
        }

        private void ProcessServiceReq(HIS_SERVICE_REQ raw, List<HIS_SERE_SERV> oldSereServs, List<HIS_SERE_SERV> createSereServs, List<long> deleteSereServIds, HisServiceReqUpdateSDO data)
        {
            if (this.beforeUpdateServiceReq == null)
            {
                this.beforeUpdateServiceReq = Mapper.Map<HIS_SERVICE_REQ>(raw);
            }
            raw.LIS_STT_ID = LisUtil.LIS_STT_ID__UPDATE;
            raw.IS_UPDATED_EXT = Constant.IS_TRUE;

            List<long> olds = oldSereServs.Where(o => deleteSereServIds == null || !deleteSereServIds.Contains(o.ID)).Select(s => s.SERVICE_ID).ToList();
            List<long> news = createSereServs != null ? createSereServs.Select(s => s.SERVICE_ID).ToList() : null;

            if (IsNotNullOrEmpty(news))
            {
                olds.AddRange(news);
            }

            List<string> attachAssignPrintTypeCodes = HisServiceCFG.DATA_VIEW.Where(o => o.ATTACH_ASSIGN_PRINT_TYPE_CODE != null && olds != null && olds.Contains(o.ID)).Select(o => o.ATTACH_ASSIGN_PRINT_TYPE_CODE).ToList();

            raw.TDL_SERVICE_IDS = String.Join(";", olds);
            raw.ATTACH_ASSIGN_PRINT_TYPE_CODE = IsNotNullOrEmpty(attachAssignPrintTypeCodes) ? string.Join(",", attachAssignPrintTypeCodes.Distinct().ToList()) : null;
            raw.INTRUCTION_TIME = data.InstructionTime;
            if (!DAOWorker.HisServiceReqDAO.Update(raw))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                throw new Exception("Cap nhat thong tin HisServiceReq that bai.");
            }

        }

        /// <summary>
        /// Xu ly de gan doi tuong phu thu
        /// </summary>
        /// <param name="hisTreatment"></param>
        /// <param name="usingPta"></param>
        /// <param name="sereServ"></param>
        private void ProcessSetPrimaryPatientType(HIS_TREATMENT treatment, HIS_PATIENT_TYPE_ALTER usingPta, HIS_SERE_SERV sereServ)
        {
            //Neu co cau hinh "tu dong gan doi tuong phu thu" (primary_patient_type_id)
            //Khi do, primary_patient_type_id lay theo patient_type_id do nguoi dung chon,
            //con patient_type_id duoc gan lai theo patient_type_id cua doi tuong BN 
            if (HisSereServCFG.SET_PRIMARY_PATIENT_TYPE == HisSereServCFG.SetPrimaryPatientType.AUTO
                || HisSereServCFG.SET_PRIMARY_PATIENT_TYPE == HisSereServCFG.SetPrimaryPatientType.YES)
            {
                SereServPriceUtil.SetPrimaryPatientTypeId(null, sereServ, usingPta, treatment);
            }
        }

        private void ProcessCheckBaseSalary(HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> patyAlters)
        {
            try
            {
                new HisTreatmentCheckOverSixMonthSalary(param).Run(treatment, patyAlters, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
