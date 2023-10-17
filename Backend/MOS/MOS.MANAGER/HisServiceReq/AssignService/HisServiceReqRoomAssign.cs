using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisServiceRoom;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq.AssignService
{
    /// <summary>
    /// Xu ly nghiep vu chi dinh phong cho cac yeu cau dich vu
    /// </summary>
    class HisServiceReqRoomAssign : BusinessBase
    {
        #region Cau hinh loai phong tuong ung voi loai dich vu
        private static Dictionary<long, long> SERVICE_REQ_TYPE__ROOM_TYPE = new Dictionary<long, long>(){
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__G, IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__BUONG},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__CDHA, IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__XL},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__NS, IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__XL},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH, IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__XL},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TDCN, IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__XL},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TT, IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__XL},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KHAC, IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__XL},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__SA, IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__XL},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT, IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__XL},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN, IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__XL},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PHCN, IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__XL},
        };
        #endregion

        private List<ServiceReqDetail> serviceReqDetails;
        private List<V_HIS_SERVICE_ROOM> vServiceRooms;
        private List<V_HIS_ROOM_COUNTER_1> roomCounters;
        private List<long> roomIds;
        private List<long> executedRoomIds;
        private long reqRoomId;
        private long reqDepartmentId;
        private long reqBranchId;
        private List<long> intructionTimes;

        private V_HIS_ROOM reqRoom;

        internal HisServiceReqRoomAssign(List<V_HIS_SERVICE> services, List<ServiceReqDetailSDO> data, long reqRoomId, long reqDepartmentId, long reqBranchId, List<long> intructionTimes, List<HIS_SERE_SERV> existSerServs)
            : base()
        {
            this.Init(data, services, reqRoomId, reqDepartmentId, reqBranchId, intructionTimes, existSerServs);
        }

        internal HisServiceReqRoomAssign(CommonParam param, List<V_HIS_SERVICE> services, List<ServiceReqDetailSDO> data, long reqRoomId, long reqDepartmentId, long reqBranchId, List<long> intructionTimes, List<HIS_SERE_SERV> existSerServs)
            : base(param)
        {
            this.Init(data, services, reqRoomId, reqDepartmentId, reqBranchId, intructionTimes, existSerServs);
        }

        private void Init(List<ServiceReqDetailSDO> data, List<V_HIS_SERVICE> services, long reqRoomId, long reqDepartmentId, long reqBranchId, List<long> intructionTimes, List<HIS_SERE_SERV> existSerServs)
        {
            if (data != null)
            {
                this.serviceReqDetails = this.ConvertServiceReqDetail(data, services);

                //Lay du lieu tu cau hinh de tang hieu nang
                this.vServiceRooms =
                    HisServiceRoomCFG.DATA_VIEW != null && serviceReqDetails != null ?
                    HisServiceRoomCFG.DATA_VIEW.Where(o => serviceReqDetails.Where(t => t.ServiceId == o.SERVICE_ID && o.IS_ACTIVE == Constant.IS_TRUE).Any()).ToList() : null;
                roomIds = IsNotNullOrEmpty(this.vServiceRooms) ? this.vServiceRooms.Select(o => o.ROOM_ID).ToList() : null;

                this.reqBranchId = reqBranchId;
                this.reqRoomId = reqRoomId;
                this.reqDepartmentId = reqDepartmentId;
                this.intructionTimes = intructionTimes;
                this.reqRoom = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == this.reqRoomId);

                //Ton tai dich vu ko duoc nguoi dung chi dinh phong thi moi thuc hien truy van DB de lay room_counter
                if (data.Exists(o => (!o.RoomId.HasValue || o.RoomId.Value <= 0)))
                {
                    var tmpRoomCounter = HisRoomCounterCFG.DATA.Where(o => roomIds != null && roomIds.Contains(o.ID)).ToList();

                    //Sap xep theo thu tu uu tien:
                    //Phòng có thể xử lý cùng lúc nhiều dịch vụ nhất ==> phòng đã từng xử lý các dịch vụ đang chỉ định ==> cùng phòng với phòng người dùng đang đăng nhập ==> cùng khoa ==> cùng chi nhánh ==> số lượng yêu cầu chờ xử lý
                    if (HisServiceReqCFG.ASSIGN_ROOM_BY_EXECUTED_OPTION == HisServiceReqCFG.AssignRoomByExecutedOption.PRIORITIZED_PREVIOUSLY_EXECUTED_ROOM) // Key cho phep uu tien lay theo phong benh nhan da duoc chi dinh dich vu truoc do
                    {
                        this.executedRoomIds = IsNotNullOrEmpty(existSerServs) ? existSerServs.Where(o => services.Select(s => s.ID).Contains(o.SERVICE_ID)).Select(p => p.TDL_EXECUTE_ROOM_ID).ToList() : null;

                        this.roomCounters = tmpRoomCounter != null ? tmpRoomCounter
                        .OrderBy(o => (o.BRANCH_ID == this.reqBranchId ? 0 : 1))
                        .ThenByDescending(o => vServiceRooms.Count(c => c.ROOM_ID == o.ID))
                        .ThenBy(o => GetPrioritizeByExecutedRoom(executedRoomIds, o.EXECUTE_ROOM_ID))
                        .ThenBy(o => GetPrioritize(o.ID, reqRoomId))
                        .ThenBy(o => (o.ID == this.reqRoomId ? 0 : 1))
                        .ThenBy(o => (o.DEPARTMENT_ID == this.reqDepartmentId ? 0 : 1))
                        .ThenBy(o => o.TOTAL_OPEN_TODAY).ToList() : null;
                    }
                    else if (HisServiceReqCFG.ASSIGN_ROOM_BY_EXECUTED_OPTION == HisServiceReqCFG.AssignRoomByExecutedOption.EXACTLY_PREVIOUSLY_EXECUTED_ROOM) // Lay phong chinh xac da xu ly truoc do                    
                    {
                        List<V_HIS_SERVICE_ROOM> vNewServiceRooms = new List<V_HIS_SERVICE_ROOM>();
                        List<HIS_SERE_SERV> previouslySS = IsNotNullOrEmpty(existSerServs) ? existSerServs.Where(o => services.Select(s => s.ID).Contains(o.SERVICE_ID)).GroupBy(p => p.SERVICE_ID, (key, g) => g.OrderByDescending(y => y.TDL_INTRUCTION_TIME).First()).ToList() : null;
                        if (IsNotNullOrEmpty(previouslySS))
                        {
                            List<long> executedServiceIds = IsNotNullOrEmpty(previouslySS) ? previouslySS.Select(p => p.SERVICE_ID).ToList() : null;
                            var groups = this.vServiceRooms.GroupBy(g => g.SERVICE_ID);
                            foreach (var g in groups)
                            {
                                if (executedServiceIds.Contains(g.Key))
                                {
                                    HIS_SERE_SERV executedSS = previouslySS.FirstOrDefault(o => o.SERVICE_ID == g.Key);
                                    List<V_HIS_SERVICE_ROOM> executedSVRooms = g.ToList().Where(o => o.ROOM_ID == executedSS.TDL_EXECUTE_ROOM_ID).ToList();
                                    if (IsNotNullOrEmpty(executedSVRooms))
                                        vNewServiceRooms.AddRange(executedSVRooms);
                                    else
                                        vNewServiceRooms.AddRange(g.ToList());
                                }
                                else
                                {
                                    vNewServiceRooms.AddRange(g.ToList());
                                }
                            }

                            this.vServiceRooms = vNewServiceRooms;
                            this.roomIds = IsNotNullOrEmpty(this.vServiceRooms) ? this.vServiceRooms.Select(o => o.ROOM_ID).ToList() : null;
                            this.roomCounters = tmpRoomCounter != null ? tmpRoomCounter.Where(o => this.roomIds != null && this.roomIds.Contains(o.ID)).ToList() : null;
                        }
                        else
                        {
                            this.roomCounters = tmpRoomCounter != null ? tmpRoomCounter
                                .OrderBy(o => (o.BRANCH_ID == this.reqBranchId ? 0 : 1))
                                .ThenByDescending(o => vServiceRooms.Count(c => c.ROOM_ID == o.ID))
                                .ThenBy(o => GetPrioritize(o.ID, reqRoomId))
                                .ThenBy(o => (o.ID == this.reqRoomId ? 0 : 1))
                                .ThenBy(o => (o.DEPARTMENT_ID == this.reqDepartmentId ? 0 : 1))
                                .ThenBy(o => o.TOTAL_OPEN_TODAY).ToList() : null;
                        }
                    }
                    else
                    {
                        this.roomCounters = tmpRoomCounter != null ? tmpRoomCounter
                        .OrderBy(o => (o.BRANCH_ID == this.reqBranchId ? 0 : 1))
                        .ThenByDescending(o => vServiceRooms.Count(c => c.ROOM_ID == o.ID))
                        .ThenBy(o => GetPrioritize(o.ID, reqRoomId))
                        .ThenBy(o => (o.ID == this.reqRoomId ? 0 : 1))
                        .ThenBy(o => (o.DEPARTMENT_ID == this.reqDepartmentId ? 0 : 1))
                        .ThenBy(o => o.TOTAL_OPEN_TODAY).ToList() : null;
                    }
                }
            }
        }

        private int GetPrioritizeByExecutedRoom(List<long> executedRoomIds, long? executeRoom)
        {
            int prioritize = 1;
            try
            {
                if (executeRoom.HasValue && IsNotNullOrEmpty(executedRoomIds) && executedRoomIds.Contains(executeRoom.Value))
                {
                    prioritize = 0;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return 1;
            }
            return prioritize;
        }

        private int GetPrioritize(long roomId, long reqRoomId)
        {
            try
            {
                if (IsNotNullOrEmpty(HisExecuteRoomCFG.DATA))
                {
                    V_HIS_EXECUTE_ROOM exeRoom = HisExecuteRoomCFG.DATA.FirstOrDefault(o => o.ROOM_ID == roomId);
                    int prioritize = 1;
                    if (exeRoom != null)
                    {
                        if (exeRoom.IS_PRIORITIZE.HasValue)
                        {
                            prioritize = 0;
                        }
                        else
                        {
                            V_HIS_EXRO_ROOM exroRoom = HisExroRoomCFG.DATA
                                .FirstOrDefault(o => o.EXECUTE_ROOM_ID == exeRoom.ID
                                    && o.ROOM_ID == reqRoomId
                                    && o.IS_PRIORITY_REQUIRE == Constant.IS_TRUE
                                    && o.IS_ACTIVE == Constant.IS_TRUE);
                            if (exroRoom != null)
                            {
                                prioritize = 0;
                            }
                        }
                    }
                    return prioritize;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return 1;
            }
            return 1;
        }

        private List<ServiceReqDetail> ConvertServiceReqDetail(List<ServiceReqDetailSDO> serviceReqDetails, List<V_HIS_SERVICE> services)
        {
            List<ServiceReqDetail> result = null;
            if (IsNotNullOrEmpty(serviceReqDetails))
            {
                result = new List<ServiceReqDetail>();
                foreach (ServiceReqDetailSDO sdo in serviceReqDetails)
                {
                    V_HIS_SERVICE vHisService = IsNotNullOrEmpty(services) ? services.Where(o => o.ID == sdo.ServiceId).FirstOrDefault() : null;
                    if (vHisService == null)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuKhongTonTaiTrongCauHinh);
                        throw new Exception("service sau ko co trong cau hinh duoc load san vao RAM cua MOS. NV van hanh kiem tra lai hoac restart MOS de cap nhat lai cau hinh vao RAM. service_id: " + sdo.ServiceId);
                    }
                    ServiceReqDetail serviceReqDetail = new ServiceReqDetail(sdo);
                    serviceReqDetail.ServiceName = vHisService.SERVICE_NAME;
                    serviceReqDetail.IsAntibioticResistance = vHisService.IS_ANTIBIOTIC_RESISTANCE == Constant.IS_TRUE;
                    //Neu dich vu co cau hinh "module xu ly" thi gan theo dich vu, ko thi gan theo loai dich vu
                    serviceReqDetail.ExeServiceModuleId = vHisService.EXE_SERVICE_MODULE_ID.HasValue ? vHisService.EXE_SERVICE_MODULE_ID : vHisService.SETY_EXE_SERVICE_MODULE_ID;
                    serviceReqDetail.IsEnableAssignPrice = vHisService.IS_ENABLE_ASSIGN_PRICE == Constant.IS_TRUE;
                    serviceReqDetail.IsSplitServiceReq = vHisService.IS_SPLIT_SERVICE_REQ == Constant.IS_TRUE;
                    serviceReqDetail.IsSplitService = vHisService.IS_SPLIT_SERVICE == Constant.IS_TRUE;
                    serviceReqDetail.AttachAssignPrintTypeCode = vHisService.ATTACH_ASSIGN_PRINT_TYPE_CODE;
                    serviceReqDetail.ServiceReqTypeId = HisServiceReqTypeMappingCFG.SERVICE_TYPE_SERVICE_REQ_TYPE_MAPPING[vHisService.SERVICE_TYPE_ID];
                    serviceReqDetail.IsNotRequiredComplete = vHisService.IS_NOT_REQUIRED_COMPLETE == Constant.IS_TRUE;
                    serviceReqDetail.IsAutoSplitReq = vHisService.IS_AUTO_SPLIT_REQ == Constant.IS_TRUE;
                    serviceReqDetail.AllowSendPacs = vHisService.ALLOW_SEND_PACS == Constant.IS_TRUE;

                    if (vHisService.IS_SPLIT_REQ_BY_SAMPLE_TYPE == Constant.IS_TRUE)
                    {
                        if (String.IsNullOrWhiteSpace(serviceReqDetail.SampleTypeCode))
                        {
                            serviceReqDetail.SampleTypeCode = vHisService.SAMPLE_TYPE_CODE;
                        }
                    }
                    else
                    {
                        serviceReqDetail.SampleTypeCode = null;
                    }

                    //Trong truong hop chi dinh dich vu giuong va co chon giuong thi gan phong xu ly chinh la buong ma giuong do thuoc ve
                    if (vHisService.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G && sdo.BedId.HasValue)
                    {
                        HIS_BED bed = HisBedCFG.DATA.Where(o => o.ID == sdo.BedId.Value).FirstOrDefault();
                        if (bed != null)
                        {
                            V_HIS_BED_ROOM bedRoom = HisBedRoomCFG.DATA.Where(o => o.ID == bed.BED_ROOM_ID).FirstOrDefault();
                            if (bedRoom != null)
                            {
                                serviceReqDetail.RoomId = bedRoom.ROOM_ID;
                            }
                        }
                    }

                    result.Add(serviceReqDetail);
                }
            }
            return result;
        }

        /// <summary>
        /// Phan bo ServiceReqDetail vao tung room dua vao thong tin do nguoi dung chi dinh hoac he thong tu dong phan bo
        /// Viec tu dong phan bo duoc dua vao so luong service_req chua duoc xu ly tai cac phong
        /// Neu viec phan bo vao cac phong that bai thi throw exception
        /// </summary>
        /// <param name="serviceReqDetails"></param>
        /// <param name="vHisServices"></param>
        /// <returns></returns>
        internal List<RoomAssignData> RoomAssign()
        {
            List<RoomAssignData> result = null;
            if (IsNotNullOrEmpty(this.serviceReqDetails))
            {
                result = new List<RoomAssignData>();
                //Neu co cau hinh tach theo "doi tuong thanh toan" thi group theo doi tuong thanh toan + loai phieu chi dinh
                if (HisServiceReqCFG.SPLIT_BY_PATIENT_TYPE)
                {
                    var groups = this.serviceReqDetails.GroupBy(o => new { o.ServiceReqTypeId, o.PatientTypeId, o.ExeServiceModuleId, o.IsAntibioticResistance, o.AssignedExecuteLoginName, o.AssignedExecuteUserName, o.NumOrderBlockId, o.NumOrderIssueId, o.NumOrder, o.SampleTypeCode});
                    foreach (var group in groups)
                    {
                        long serviceReqTypeId = group.Key.ServiceReqTypeId;
                        List<ServiceReqDetail> unhandleServices = new List<ServiceReqDetail>(group);
                        List<V_HIS_SERVICE_ROOM> assigneServiceRooms = null;
                        List<V_HIS_SERVICE_ROOM> unassigneServiceRooms = null;
                        //Lay ra danh sach "room" co the thuc hien cac service
                        this.GetProcessRoom(unhandleServices, serviceReqTypeId, ref assigneServiceRooms, ref unassigneServiceRooms);

                        this.ProcessAssignedService(result, unhandleServices, assigneServiceRooms, serviceReqTypeId, group.Key.PatientTypeId, group.Key.ExeServiceModuleId, group.Key.IsAntibioticResistance, group.Key.AssignedExecuteLoginName, group.Key.AssignedExecuteUserName, group.Key.NumOrderBlockId, group.Key.NumOrderIssueId, group.Key.NumOrder, group.Key.SampleTypeCode);
                        this.ProcessUnassignedService(result, unhandleServices, unassigneServiceRooms, serviceReqTypeId, group.Key.PatientTypeId, group.Key.ExeServiceModuleId, group.Key.IsAntibioticResistance, group.Key.AssignedExecuteLoginName, group.Key.AssignedExecuteUserName, group.Key.NumOrderBlockId, group.Key.NumOrderIssueId, group.Key.NumOrder, group.Key.SampleTypeCode);
                    }
                }
                //Neu ko co cau hinh tach theo "doi tuong thanh toan" thi chi group theo loai phieu chi dinh
                else
                {
                    var groups = this.serviceReqDetails.GroupBy(o => new { o.ServiceReqTypeId, o.ExeServiceModuleId, o.IsAntibioticResistance, o.AssignedExecuteLoginName, o.AssignedExecuteUserName, o.NumOrderBlockId, o.NumOrderIssueId, o.NumOrder, o.SampleTypeCode });
                    foreach (var group in groups)
                    {
                        long serviceReqTypeId = group.Key.ServiceReqTypeId;
                        long? exeServiceModuleId = group.Key.ExeServiceModuleId;
                        List<ServiceReqDetail> unhandleServices = new List<ServiceReqDetail>(group);
                        List<V_HIS_SERVICE_ROOM> assigneServiceRooms = null;
                        List<V_HIS_SERVICE_ROOM> unassigneServiceRooms = null;

                        //Lay ra danh sach "room" co the thuc hien cac service
                        this.GetProcessRoom(unhandleServices, serviceReqTypeId, ref assigneServiceRooms, ref unassigneServiceRooms);

                        this.ProcessAssignedService(result, unhandleServices, assigneServiceRooms, serviceReqTypeId, null, exeServiceModuleId, group.Key.IsAntibioticResistance, group.Key.AssignedExecuteLoginName, group.Key.AssignedExecuteUserName, group.Key.NumOrderBlockId, group.Key.NumOrderIssueId, group.Key.NumOrder, group.Key.SampleTypeCode);

                        this.ProcessUnassignedService(result, unhandleServices, unassigneServiceRooms, serviceReqTypeId, null, exeServiceModuleId, group.Key.IsAntibioticResistance, group.Key.AssignedExecuteLoginName, group.Key.AssignedExecuteUserName, group.Key.NumOrderBlockId, group.Key.NumOrderIssueId, group.Key.NumOrder, group.Key.SampleTypeCode);
                    }
                }
            }

            List<RoomAssignData> rs = this.MakeSingleReq(result);
            this.MakeSingleService(rs);
            return rs;
        }

        /// <summary>
        /// Xu ly de tach dich vu trong y lenh (dua theo cau hinh trong "loai dich vu" va "dich vu")
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private List<RoomAssignData> MakeSingleReq(List<RoomAssignData> data)
        {
            if (data != null && data.Count > 0)
            {
                List<RoomAssignData> splitByServiceType = new List<RoomAssignData>();

                Mapper.CreateMap<RoomAssignData, RoomAssignData>();

                //Xu ly de tach moi y lenh chi co 1 dich vu
                foreach (RoomAssignData t in data)
                {
                    if (t.ServiceReqDetails != null && t.ServiceReqDetails.Count > 1)
                    {
                        if (t.ServiceReqDetails.Exists(o => o.IsAutoSplitReq))
                        {
                            List<ServiceReqDetail> details = t.ServiceReqDetails;
                            for (int i = 0; i < details.Count; i++)
                            {
                                RoomAssignData r = Mapper.Map<RoomAssignData>(t);
                                r.ServiceReqDetails = new List<ServiceReqDetail>() { details[i] };
                                splitByServiceType.Add(r);
                            }
                        }
                        else if (t.ServiceReqDetails.Exists(o => o.IsNotRequiredComplete))
                        {
                            var groupByRequiredComplete = t.ServiceReqDetails.GroupBy(o => o.IsNotRequiredComplete);
                            foreach (var groups in groupByRequiredComplete)
                            {
                                RoomAssignData r = Mapper.Map<RoomAssignData>(t);
                                r.ServiceReqDetails = groups.ToList();
                                splitByServiceType.Add(r);
                            }
                        }
                        else
                        {
                            splitByServiceType.Add(t);
                        }
                    }
                    else
                    {
                        splitByServiceType.Add(t);
                    }
                }

                List<RoomAssignData> splitByService = new List<RoomAssignData>();
                splitByService.AddRange(splitByServiceType);

                //Xu ly de tach moi dich vu chi co so luong = 1
                foreach (RoomAssignData t in splitByServiceType)
                {
                    if (t.ServiceReqDetails != null)
                    {
                        List<ServiceReqDetail> toRemoves = new List<ServiceReqDetail>();
                        foreach (ServiceReqDetail d in t.ServiceReqDetails)
                        {
                            //Neu dich vu co cau hinh "tach y lenh" va so luong > 1 va so luong la so nguyen thi xu ly de tach y lenh de moi dv co so luong = 1
                            if (d.IsSplitServiceReq && d.Amount > 1 && (d.Amount % 1) == 0)
                            {
                                int count = (int)d.Amount - 1;
                                d.Amount = 1;

                                //neu trong y lenh co dich vu khac thi tach y lenh moi
                                if (t.ServiceReqDetails.Count > 1 && toRemoves.Count < t.ServiceReqDetails.Count - 1)
                                {
                                    toRemoves.Add(d);
                                    count++;
                                }

                                //tach doi voi so luong 1
                                for (int i = 0; i < count; i++)
                                {
                                    RoomAssignData r = Mapper.Map<RoomAssignData>(t);
                                    r.ServiceReqDetails = new List<ServiceReqDetail>() { d };
                                    splitByService.Add(r);
                                }
                            }
                            else if (d.IsSplitServiceReq && d.Amount == 1 && t.ServiceReqDetails.Count > 1 && toRemoves.Count < t.ServiceReqDetails.Count - 1)//neu dich vu SL 1 va co nhieu dich vu cung phong va co cau hinh tach thi tach ra
                            {
                                RoomAssignData r = Mapper.Map<RoomAssignData>(t);
                                r.ServiceReqDetails = new List<ServiceReqDetail>() { d };
                                splitByService.Add(r);
                                toRemoves.Add(d);
                            }
                        }

                        //bo dich vu ra khoi nhom
                        //khong bo het tat ca
                        if (IsNotNullOrEmpty(toRemoves))
                        {
                            foreach (var item in toRemoves)
                            {
                                t.ServiceReqDetails.Remove(item);
                            }
                        }
                    }
                }
                return splitByService;
            }
            return null;
        }

        /// <summary>
        /// Neu dich vu nao cau hinh tach dich vu va co so luong > 1 thi tach ra nhieu dong
        /// </summary>
        /// <param name="data"></param>
        private void MakeSingleService(List<RoomAssignData> data)
        {
            if (data != null && data.Count > 0)
            {
                foreach (RoomAssignData ad in data)
                {
                    if (ad.ServiceReqDetails != null && ad.ServiceReqDetails.Count > 0)
                    {
                        List<ServiceReqDetail> newList = new List<ServiceReqDetail>();
                        foreach (ServiceReqDetail d in ad.ServiceReqDetails)
                        {
                            if (d.IsSplitService && d.Amount > 1)
                            {
                                decimal remain = d.Amount;

                                Mapper.CreateMap<ServiceReqDetail, ServiceReqDetail>();
                                while (remain > 0)
                                {
                                    ServiceReqDetail s = Mapper.Map<ServiceReqDetail>(d);
                                    s.Amount = remain > 1 ? 1 : remain;

                                    newList.Add(s);
                                    remain = remain - s.Amount;
                                }
                            }
                            else
                            {
                                newList.Add(d);
                            }
                        }
                        ad.ServiceReqDetails = newList;
                    }
                }
            }
        }

        /// <summary>
        /// Xu ly cac dich vu ma nguoi dung chi dinh room
        /// </summary>
        /// <param name="unhandleServices"></param>
        /// <param name="serviceRooms"></param>
        /// <returns></returns>
        private void ProcessAssignedService(List<RoomAssignData> result, List<ServiceReqDetail> unhandleServices, List<V_HIS_SERVICE_ROOM> serviceRooms, long serviceReqTypeId, long? patientTypeId, long? exeServiceModuleId, bool isAntibioticResistance, string assignedExecuteLoginName, string assignedExecuteUserName, long? numOrderBlockId, long? numOrderIssueId, long? numOrder, string sampleTypeCode)
        {
            //Xu ly cac dich vu da duoc nguoi dung chi dinh room
            List<ServiceReqDetail> assignedList = unhandleServices.Where(o => o.RoomId.HasValue && o.RoomId.Value > 0).ToList();

            if (IsNotNullOrEmpty(assignedList))
            {
                //Kiem tra tinh kha thi cua cac ServiceReqDetail (xem room co xu ly duoc dich vu khong)
                List<ServiceReqDetail> unhandlables = assignedList.Where(o => !serviceRooms.Where(t => t.SERVICE_ID == o.ServiceId && t.ROOM_ID == o.RoomId).Any()).ToList();
                this.AddMessageAndThrowException(unhandlables, LibraryMessage.Message.Enum.HisServiceReq_PhongDuocChiDinhKhongTheXuLyDichVu);
                foreach (ServiceReqDetail sdo in assignedList)
                {
                    V_HIS_SERVICE_ROOM room = serviceRooms.Where(o => o.ROOM_ID == sdo.RoomId).FirstOrDefault();
                    this.AddHandledServiceToResult(result, serviceReqTypeId, room.ROOM_ID, room.DEPARTMENT_ID, patientTypeId, exeServiceModuleId, isAntibioticResistance, assignedExecuteLoginName, assignedExecuteUserName, numOrderBlockId, numOrderIssueId, numOrder, sampleTypeCode, sdo);
                }
                this.RemoveHandledService(unhandleServices, assignedList);
            }
        }

        /// <summary>
        /// Xu ly cac dich vu ma nguoi dung ko chi dinh room ==> he thong tu dong chi dinh room dua vao so luot dang ky o tung phong
        /// </summary>
        /// <param name="result"></param>
        /// <param name="unhandleServices"></param>
        /// <param name="serviceRooms"></param>
        private void ProcessUnassignedService(List<RoomAssignData> result, List<ServiceReqDetail> unhandleServices, List<V_HIS_SERVICE_ROOM> serviceRooms, long serviceReqTypeId, long? patientTypeId, long? exeServiceModuleId, bool isAntibioticResistance, string assignedExecuteLoginName, string assignedExecuteUserName, long? numOrderBlockId, long? numOrderIssueId, long? numOrder, string sampleTypeCode)
        {
            if (!IsNotNullOrEmpty(unhandleServices))
            {
                return;
            }

            //Lay ra danh sach "room counter" tuong ung, uu tien cung phong, cung chi nhanh, cung khoa truoc
            //va sap xep theo thu tu tong cac request chua duoc xu ly trong ngay
            List<long> roomIds = serviceRooms.Select(o => o.ROOM_ID).ToList();
            List<V_HIS_ROOM_COUNTER_1> rooms = this.roomCounters
                .Where(o => roomIds != null && roomIds.Contains(o.ID))
                .ToList();

            if (!IsNotNullOrEmpty(rooms))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.LoiDuLieu);
                throw new Exception("Co loi du lieu V_His_Room_Counter. Khong co du lieu tuong ung voi serviceRooms: " + LogUtil.TraceData("serviceRooms", serviceRooms));
            }

            //Duyet danh sach room
            foreach (V_HIS_ROOM_COUNTER_1 room in rooms)
            {
                //neu ko con dich vu nao can chi dinh phong thi break
                if (unhandleServices.Count == 0)
                {
                    break;
                }

                //Lay ra danh sach ServiceReqDetail co the xu ly trong room nay
                List<long> serviceIdsOfRoom = serviceRooms.Where(o => o.ROOM_ID == room.ID).Select(o => o.SERVICE_ID).ToList();

                //Lay ra danh sach ServiceReqDetail co the duoc xu ly trong room nay va chua duoc chi dinh room (room_id = null)
                List<ServiceReqDetail> unassignedList = unhandleServices
                    .Where(o => (!o.RoomId.HasValue || o.RoomId.Value <= 0) && serviceRooms.Where(t => t.SERVICE_ID == o.ServiceId && t.ROOM_ID == room.ID).Any()).ToList();

                //Neu khong co dich vu nao co the chi dinh vao room nay thi chuyen sang room tiep theo
                if (IsNotNullOrEmpty(unassignedList))
                {
                    List<ServiceReqDetail> handlables = new List<ServiceReqDetail>();
                    handlables.AddRange(unassignedList);
                    this.AddHandledServiceToResult(result, serviceReqTypeId, room.ID, room.DEPARTMENT_ID, patientTypeId, exeServiceModuleId, isAntibioticResistance, assignedExecuteLoginName, assignedExecuteUserName, numOrderBlockId, numOrderIssueId, numOrder, sampleTypeCode, handlables);
                    this.RemoveHandledService(unhandleServices, handlables);
                }
            }

            //Sau khi duyet het room ma van ton tai ServiceReqDetail chua duoc xu ly thi canh bao va throw exception
            this.AddMessageAndThrowException(unhandleServices, LibraryMessage.Message.Enum.HisServiceReq_KhongTonTaiPhongXuLyDichVuSau);
        }

        /// <summary>
        /// Remove cac dich vu da duoc xu ly ra khoi danh sach chua xu ly
        /// </summary>
        /// <param name="unhandleServices"></param>
        /// <param name="handleServices"></param>
        private void RemoveHandledService(List<ServiceReqDetail> unhandleServices, List<ServiceReqDetail> handleServices)
        {
            //remove khoi danh sach chua xu ly
            unhandleServices.RemoveAll(x => handleServices.Where(o => o.ServiceId == x.ServiceId).Any());
        }

        /// Xu ly de bo sung cac service da duoc xu ly vao danh sach ket qua
        private void AddHandledServiceToResult(List<RoomAssignData> result, long serviceReqTypeId, long roomId, long departmentId, long? patientTypeId, long? exeServiceModuleId, bool isAntibioticResistance, string assignedExecuteLoginName, string assignedExecuteUserName, long? numOrderBlockId, long? numOrderIssueId, long? numOrder, string sampleTypeCode, ServiceReqDetail dataToAdd)
        {
            this.AddHandledServiceToResult(result, serviceReqTypeId, roomId, departmentId, patientTypeId, exeServiceModuleId, isAntibioticResistance, assignedExecuteLoginName, assignedExecuteUserName, numOrderBlockId, numOrderIssueId, numOrder, sampleTypeCode, new List<ServiceReqDetail>() { dataToAdd });
        }

        /// <summary>
        /// Xu ly de bo sung cac service da duoc xu ly vao danh sach ket qua
        /// </summary>
        /// <param name="result"></param>
        /// <param name="roomId"></param>
        /// <param name="departmentId"></param>
        /// <param name="dataToAdd"></param>
        private void AddHandledServiceToResult(List<RoomAssignData> result, long serviceReqTypeId, long roomId, long departmentId, long? patientTypeId, long? exeServiceModuleId, bool isAntibioticResistance, string assignedExecuteLoginName, string assignedExecuteUserName, long? numOrderBlockId, long? numOrderIssueId, long? numOrder, string sampleTypeCode, List<ServiceReqDetail> dataToAdd)
        {
            RoomAssignData added = null;

            //Neu co cau hinh tach theo doi tuong benh nhan
            if (HisServiceReqCFG.SPLIT_BY_PATIENT_TYPE)
            {
                added = result.Where(o => o.RoomId == roomId
                    && o.PatientTypeId == patientTypeId
                    && o.ServiceReqTypeId == serviceReqTypeId
                    && o.ExeServiceModuleId == exeServiceModuleId
                    && o.IsAntibioticResistance == isAntibioticResistance
                    && o.AssignedExecuteLoginName == assignedExecuteLoginName
                    && o.AssignedExecuteUserName == assignedExecuteUserName
                    && o.SampleTypeCode == sampleTypeCode
                    ).FirstOrDefault();
            }
            else
            {
                added = result.Where(o => o.RoomId == roomId
                    && o.ServiceReqTypeId == serviceReqTypeId
                    && o.ExeServiceModuleId == exeServiceModuleId
                    && o.IsAntibioticResistance == isAntibioticResistance
                    && o.AssignedExecuteLoginName == assignedExecuteLoginName
                    && o.AssignedExecuteUserName == assignedExecuteUserName
                    && o.SampleTypeCode == sampleTypeCode
                    ).FirstOrDefault();
            }

            if (added == null)
            {
                added = new RoomAssignData(serviceReqTypeId, roomId, departmentId, dataToAdd, patientTypeId, exeServiceModuleId, isAntibioticResistance, assignedExecuteLoginName, assignedExecuteUserName, numOrderBlockId, numOrderIssueId, numOrder, sampleTypeCode);
                result.Add(added);
            }
            else if (added.ServiceReqDetails == null)
            {
                added.ServiceReqDetails = dataToAdd;
            }
            else
            {
                added.ServiceReqDetails.AddRange(dataToAdd);
            }
        }

        /// <summary>
        /// Bo sung thong bao trong truong hop co dich vu khong the duoc xu ly
        /// </summary>
        /// <param name="unhandlables"></param>
        private void AddMessageAndThrowException(List<ServiceReqDetail> unhandlables, MOS.LibraryMessage.Message.Enum message)
        {
            if (IsNotNullOrEmpty(unhandlables))
            {
                List<string> unhandleServiceNames = unhandlables.Select(o => o.ServiceName).ToList();
                string extraMess = string.Join("; ", unhandleServiceNames);
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, message, extraMess);
                throw new Exception();
            }
        }

        /// <summary>
        /// Lay ra danh sach phong co the xu ly duoc dua vao danh sach service_id va service_req_type_id
        /// </summary>
        /// <param name="serviceIds"></param>
        /// <param name="serviceReqTypeId"></param>
        /// <returns></returns>
        private void GetProcessRoom(List<ServiceReqDetail> unhandleServices, long serviceReqTypeId, ref List<V_HIS_SERVICE_ROOM> assigneRooms, ref List<V_HIS_SERVICE_ROOM> unassigneRooms)
        {
            List<long> serviceIds = unhandleServices.Where(o => o.ServiceId > 0).Select(o => o.ServiceId).ToList();

            //Lay ra danh sach "room" co the thuc hien cac service
            List<V_HIS_SERVICE_ROOM> assignes = null;
            List<V_HIS_SERVICE_ROOM> unassignes = null;

            if (IsNotNullOrEmpty(HisRoomCFG.DATA) && IsNotNullOrEmpty(this.vServiceRooms) && IsNotNullOrEmpty(serviceIds))
            {
                long? roomTypeId = this.GetProperRoomTypeId(serviceReqTypeId);

                List<long> allowedRoomIds = HisRoomCFG.DATA
                    .Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE
                        && this.roomIds.Contains(o.ID)
                        && (!roomTypeId.HasValue || roomTypeId.Value == o.ROOM_TYPE_ID)
                        && this.IsActiveDepartment(o.DEPARTMENT_ID)
                        && this.IsAllowTime(o)
                        && this.IsAllowExecuteRoom(o.ID)
                        && this.IsNotPauseAssign(o.ID)
                    ).Select(o => o.ID).ToList();

                //Duyet theo tung dich vu de xem co dich vu nao duoc set uu tien hay khong                    
                foreach (var serviceId in serviceIds)
                {
                    var sRooms = this.vServiceRooms.Where(o => o.SERVICE_ID == serviceId && allowedRoomIds.Contains(o.ROOM_ID)).ToList();
                    if (!IsNotNullOrEmpty(sRooms))
                    {
                        continue;
                    }

                    //Khong lay uu tien danh cho xu ly dich vu duoc client chi dinh phong xu ly
                    if (assignes == null) assignes = new List<V_HIS_SERVICE_ROOM>();
                    assignes.AddRange(sRooms);

                    //Neu co phong duoc uu tien thi chi lay nhung phong uu tien dung cho dich vu tu dong 
                    if (sRooms.Any(a => a.IS_PRIORITY.HasValue && a.IS_PRIORITY.Value == Constant.IS_TRUE))
                    {
                        sRooms = sRooms.Where(o => o.IS_PRIORITY.HasValue && o.IS_PRIORITY.Value == Constant.IS_TRUE).ToList();
                    }
                    if (unassignes == null) unassignes = new List<V_HIS_SERVICE_ROOM>();
                    unassignes.AddRange(sRooms);
                }
            }

            if (!IsNotNullOrEmpty(unassignes))
            {
                List<string> unhandleServiceNames = unhandleServices.Select(o => o.ServiceName).ToList();
                string extraMess = string.Join("; ", unhandleServiceNames);

                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongTonTaiPhongXuLyDichVuSau, extraMess);
                throw new Exception();
            }

            assigneRooms = assignes;
            unassigneRooms = unassignes;
        }

        /// <summary>
        /// Lay loai phong xu ly tuong ung voi loai dich vu
        /// </summary>
        /// <param name="serviceTypeId"></param>
        /// <returns></returns>
        private long? GetProperRoomTypeId(long serviceReqTypeId)
        {
            if (SERVICE_REQ_TYPE__ROOM_TYPE.ContainsKey(serviceReqTypeId))
            {
                return SERVICE_REQ_TYPE__ROOM_TYPE[serviceReqTypeId];
            }
            return null;
        }

        private bool IsActiveDepartment(long departmentId)
        {
            return HisDepartmentCFG.DATA != null && HisDepartmentCFG.DATA.Exists(t => t.ID == departmentId && t.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE);
        }

        private bool IsAllowTime(V_HIS_ROOM room)
        {
            try
            {
                if (room.IS_RESTRICT_TIME == Constant.IS_TRUE)
                {
                    List<V_HIS_ROOM_TIME> roomTimes = HisRoomTimeCFG.DATA != null ? HisRoomTimeCFG.DATA.Where(t => t.ROOM_ID == room.ID).ToList() : null;
                    if (IsNotNullOrEmpty(roomTimes))
                    {
                        foreach (long instructionTime in this.intructionTimes)
                        {
                            DateTime dt = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(instructionTime).Value;
                            int day = (int)dt.DayOfWeek;
                            int time = Convert.ToInt32(dt.ToString("HHmmss"));
                            if (!roomTimes.Exists(e => e.DAY == (day + 1) && time >= Convert.ToInt32(e.FROM_TIME) && time <= Convert.ToInt32(e.TO_TIME)))
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        private bool IsAllowExecuteRoom(long roomId)
        {
            if (this.reqRoom.IS_RESTRICT_EXECUTE_ROOM == Constant.IS_TRUE)
            {
                List<V_HIS_EXRO_ROOM> exroRooms = HisExroRoomCFG.DATA != null ? HisExroRoomCFG.DATA.Where(t => t.ROOM_ID == this.reqRoomId && t.EXECUTE_ROOM_ROOM_ID == roomId && t.IS_ALLOW_REQUEST == Constant.IS_TRUE).ToList() : null;
                return exroRooms != null && exroRooms.Count > 0;
            }
            else
            {
                return true;
            }
        }

        private bool IsNotPauseAssign(long roomId)
        {
            V_HIS_EXECUTE_ROOM executeRoom = HisExecuteRoomCFG.DATA != null ? HisExecuteRoomCFG.DATA.Where(t => t.ROOM_ID == roomId).FirstOrDefault() : null;
            return executeRoom == null || executeRoom.IS_PAUSE_ENCLITIC != Constant.IS_TRUE;
        }
    }
}
