using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisExecuteRoom;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisServiceReqStt;
using MOS.MANAGER.HisServiceRoom;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatmentBedRoom;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using MOS.MANAGER.HisPatient;

namespace MOS.MANAGER.HisServiceReq
{
    partial class HisServiceReqCheck : BusinessBase
    {
        internal HisServiceReqCheck()
            : base()
        {

        }

        internal HisServiceReqCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_SERVICE_REQ data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.SERVICE_REQ_STT_ID)) throw new ArgumentNullException("data.SERVICE_REQ_STT_ID");
                if (!IsGreaterThanZero(data.SERVICE_REQ_TYPE_ID)) throw new ArgumentNullException("data.SERVICE_REQ_TYPE_ID");
                if (!IsGreaterThanZero(data.TDL_PATIENT_ID)) throw new ArgumentNullException("data.TDL_PATIENT_ID");
                if (!IsGreaterThanZero(data.EXECUTE_DEPARTMENT_ID)) throw new ArgumentNullException("data.EXECUTE_DEPARTMENT_ID");
                if (!IsGreaterThanZero(data.EXECUTE_ROOM_ID)) throw new ArgumentNullException("data.EXECUTE_ROOM_ID");
                if (!IsGreaterThanZero(data.REQUEST_DEPARTMENT_ID)) throw new ArgumentNullException("data.REQUEST_DEPARTMENT_ID");
                if (!IsGreaterThanZero(data.REQUEST_ROOM_ID)) throw new ArgumentNullException("data.REQUEST_ROOM_ID");
                if (!IsGreaterThanZero(data.TREATMENT_ID)) throw new ArgumentNullException("data.TREATMENT_ID");
                if (!IsNotNullOrEmpty(data.REQUEST_LOGINNAME)) throw new ArgumentNullException("data.REQUEST_LOGINNAME");
                if (!IsNotNullOrEmpty(data.REQUEST_USERNAME)) throw new ArgumentNullException("data.REQUEST_USERNAME");
                if (data.INTRUCTION_TIME <= 0) throw new ArgumentNullException("data.INTRUCTION_TIME");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool VerifyRequireField(AssignServiceSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNullOrEmpty(data.ServiceReqDetails)) throw new ArgumentNullException("data.ServiceReqDetails");
                if (!IsGreaterThanZero(data.TreatmentId)) throw new ArgumentNullException("data.TreatmentId");
                if (!IsNotNullOrEmpty(data.InstructionTimes)) throw new ArgumentNullException("data.InstructionTimes");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsUnLock(HIS_SERVICE_REQ data)
        {
            bool valid = true;
            try
            {
                if (data.IS_ACTIVE == null || data.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                {
                    valid = false;
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDangBiKhoa);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra du lieu co o trang thai unlock (su dung danh sach data)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsUnLock(List<HIS_SERVICE_REQ> listData)
        {
            bool valid = true;
            try
            {
                if (listData != null && listData.Count > 0)
                {
                    foreach (var data in listData)
                    {
                        if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE) //khong duoc goi ham IsUnLock(data) vi vi pham nguyen tac doc lap
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (!valid)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        //internal bool IsUnlock(List<HIS_SERVICE_REQ> listData)
        //{
        //    bool valid = true;
        //    try
        //    {
        //        if (listData != null && listData.Count > 0)
        //        {
        //            foreach (var data in listData)
        //            {
        //                if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE) //khong duoc goi ham IsUnLock(data) vi vi pham nguyen tac doc lap
        //                {
        //                    valid = false;
        //                    break;
        //                }
        //            }
        //            if (!valid)
        //            {
        //                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogSystem.Error(ex);
        //        valid = false;
        //        param.HasException = true;
        //    }
        //    return valid;
        //}
        internal bool IsFinished(List<HIS_SERVICE_REQ> serviceReq)
        {
            try
            {
                List<string> hasCodes = serviceReq != null ? serviceReq
                    .Where(o => o.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                    .Select(o => o.SERVICE_REQ_CODE)
                    .ToList() : null;

                if (IsNotNullOrEmpty(hasCodes))
                {
                    string str = string.Join(",", hasCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChuaHoanThanh, str);
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
        /// <summary>
        /// Kiem tra su ton tai cua id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyId(long id, ref HIS_SERVICE_REQ data)
        {
            bool valid = true;
            try
            {
                data = new HisServiceReqGet().GetById(id);
                if (data == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    Logging("Id invalid." + LogUtil.TraceData("id", id), LogType.Error);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool VerifyIds(List<long> listId, List<HIS_SERVICE_REQ> listRaw)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                    filter.IDs = listId;
                    List<HIS_SERVICE_REQ> listData = new HisServiceReqGet().Get(filter);
                    if (listData == null || listId.Count != listData.Count)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        Logging("ListId invalid." + LogUtil.TraceData("listData", listData) + LogUtil.TraceData("listId", listId), LogType.Error);
                        valid = false;
                    }
                    else
                    {
                        listRaw.AddRange(listData);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra danh sach service yeu cau co match voi service_type_id hay khong
        /// </summary>
        /// <param name="serviceReqDetails"></param>
        /// <param name="serviceTypeId"></param>
        /// <returns></returns>
        internal bool IsMatchingServiceTypeId(List<ServiceReqDetailSDO> serviceReqDetails, long serviceReqTypeId, long? exeServiceModuleId)
        {
            bool valid = false;
            try
            {
                if (!IsGreaterThanZero(serviceReqTypeId)) throw new ArgumentNullException("serviceTypeId");
                if (!IsNotNullOrEmpty(serviceReqDetails)) throw new ArgumentNullException("serviceReqDetails");

                List<V_HIS_SERVICE> services = HisServiceCFG.DATA_VIEW.Where(o => serviceReqDetails != null && serviceReqDetails.Exists(t => t.ServiceId == o.ID)).ToList();
                if (IsNotNullOrEmpty(services))
                {
                    ///Kiem tra co loai service_req_type ko
                    List<V_HIS_SERVICE> notMatchRequestTypeList = services
                        .Where(o => HisServiceReqTypeMappingCFG.SERVICE_TYPE_SERVICE_REQ_TYPE_MAPPING[o.SERVICE_TYPE_ID] != serviceReqTypeId)
                        .ToList();
                    if (IsNotNullOrEmpty(notMatchRequestTypeList))
                    {
                        List<string> serviceNames = notMatchRequestTypeList.Select(o => o.SERVICE_NAME).ToList();
                        string str = string.Join(",", serviceNames);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_TonTaiYeuCauKhongCungLoai, str);
                        return false;
                    }

                    ///Kiem tra cac dich vu moi co cung loai form xu ly ko
                    List<V_HIS_SERVICE> notMatchModuleList = services
                        .Where(o => (o.EXE_SERVICE_MODULE_ID.HasValue && o.EXE_SERVICE_MODULE_ID != exeServiceModuleId) || (!o.EXE_SERVICE_MODULE_ID.HasValue && o.SETY_EXE_SERVICE_MODULE_ID.HasValue && o.SETY_EXE_SERVICE_MODULE_ID != exeServiceModuleId)).ToList();
                    if (IsNotNullOrEmpty(notMatchModuleList))
                    {
                        List<string> serviceNames = notMatchModuleList.Select(o => o.SERVICE_NAME).ToList();
                        string str = string.Join(",", serviceNames);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_TonTaiYeuCauKhongCungLoaiFormXuLy, str);
                        return false;
                    }
                    valid = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra danh sach service yeu cau co match voi service_type_id hay khong
        /// </summary>
        /// <param name="serviceReqDetails"></param>
        /// <param name="serviceTypeId"></param>
        /// <returns></returns>
        internal bool IsValidData(List<ServiceReqDetailSDO> serviceReqDetails, List<long> instructionTimes, ref List<V_HIS_SERVICE> services)
        {
            bool valid = true;
            try
            {
                List<ServiceReqDetailSDO> invalidNumOrderIssues = serviceReqDetails != null ?
                    serviceReqDetails.Where(o => (o.NumOrder.HasValue && !o.NumOrderIssueId.HasValue) || (!o.NumOrder.HasValue && o.NumOrderIssueId.HasValue)).ToList() : null;

                if (IsNotNullOrEmpty(invalidNumOrderIssues))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Neu truyen NumOrder thi bat buoc phai truyen NumOrderIssueId va nguoc lai");
                    return false;
                }

                List<V_HIS_SERVICE> tmp = HisServiceCFG.DATA_VIEW
                    .Where(o => serviceReqDetails != null
                        && serviceReqDetails.Exists(t => t.ServiceId == o.ID)).ToList();

                services = tmp;

                List<string> lockServices = services != null ? services.Where(o => o.IS_ACTIVE != Constant.IS_TRUE).Select(o => o.SERVICE_NAME).ToList() : null;
                if (IsNotNullOrEmpty(lockServices))
                {
                    string serviceCodeStr = string.Join(",", lockServices);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DichVuDangTamKhoa, serviceCodeStr);
                    valid = false;
                }

                if (IsNotNullOrEmpty(services) && services.Exists(t => (t.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH) && instructionTimes != null && instructionTimes.Count > 1))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongChoPhepChiDinhNhieuNgayVoiDichVuKhamHoacGiuong);
                    return false;
                }

                //Neu ton tai du lieu ko chon dich vu, va phong xu ly ko duoc cau hinh "cho phep khong chon dich vu" thi bao loi
                List<long> noServiceRoomIds = serviceReqDetails.Where(o => o.ServiceId == 0 && o.RoomId.HasValue && !HisExecuteRoomCFG.DATA.Exists(t => t.ROOM_ID == o.RoomId && t.ALLOW_NOT_CHOOSE_SERVICE == Constant.IS_TRUE)).Select(o => o.RoomId.Value).ToList();
                if (IsNotNullOrEmpty(noServiceRoomIds))
                {
                    List<string> roomNames = HisExecuteRoomCFG.DATA.Where(o => noServiceRoomIds.Contains(o.ROOM_ID)).Select(o => o.EXECUTE_ROOM_NAME).ToList();
                    string roomNameStr = string.Join(",", roomNames);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_PhongBatBuocChonDichVu, roomNameStr);
                    return false;
                }

                //Neu ton tai doi tuong bat buoc chon nguon khac thi kiem tra co dich vu nao ko chon nguon khac ko
                List<long> noOtherSourceIds = serviceReqDetails.Where(o => !o.OtherPaySourceId.HasValue && HisPatientTypeCFG.DATA.Exists(t => t.OTHER_PAY_SOURCE_IDS != null && t.ID == o.PatientTypeId)).Select(o => o.ServiceId).ToList();
                if (IsNotNullOrEmpty(noOtherSourceIds))
                {
                    List<string> serviceNames = HisServiceCFG.DATA_VIEW.Where(o => noOtherSourceIds.Contains(o.ID)).Select(o => o.SERVICE_NAME).ToList();
                    string serviceNameStr = string.Join(",", serviceNames);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_BatBuocChonNguonKhac, serviceNameStr);
                    valid = false;
                }

                //Neu ton tai doi tuong bat buoc chon nguon khac thi kiem tra xem nguon khac co hop le ko
                List<long> invalidOtherSourceIds = serviceReqDetails
                    .Where(o => o.OtherPaySourceId.HasValue && HisPatientTypeCFG.DATA.Exists(t => t.OTHER_PAY_SOURCE_IDS != null && !("," + t.OTHER_PAY_SOURCE_IDS + ",").Contains(o.OtherPaySourceId.ToString()) && t.ID == o.PatientTypeId)).Select(o => o.ServiceId).ToList();
                if (IsNotNullOrEmpty(noOtherSourceIds))
                {
                    List<string> serviceNames = HisServiceCFG.DATA_VIEW.Where(o => noOtherSourceIds.Contains(o.ID)).Select(o => o.SERVICE_NAME).ToList();
                    string serviceNameStr = string.Join(",", serviceNames);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_NguonKhacKhongHopLe, serviceNameStr);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidBedInfo(long treatmentId, List<ServiceReqDetailSDO> serviceReqDetails, List<long> instructionTimes, List<V_HIS_SERVICE> services, ref Dictionary<ServiceReqDetailSDO, HIS_TREATMENT_BED_ROOM> treatmentBedRoomDic)
        {
            bool inValidBedWithInstructionTimes = services
                .Exists(t => serviceReqDetails != null && serviceReqDetails.Exists(o => t.ID == o.ServiceId
                    && t.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G
                    && (o.BedStartTime.HasValue || o.BedFinishTime.HasValue || o.BedId.HasValue))
                );

            if (inValidBedWithInstructionTimes && (instructionTimes != null && instructionTimes.Count > 1))
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongChoPhepChonCheDoChiDinhNhieuNgayNeuChonGiuong);
                return false;
            }

            //Lay ra cac du lieu ma co 1 trong cac truong BedStartTime, BedFinishTime, BedId nhung khong co du ca 3 truong thi bao loi
            List<string> invalidBeds = services
                .Where(t => serviceReqDetails != null && serviceReqDetails.Exists(o => t.ID == o.ServiceId
                    && t.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G
                    && (o.BedStartTime.HasValue || o.BedFinishTime.HasValue || o.BedId.HasValue)
                    && (!o.BedStartTime.HasValue || !o.BedFinishTime.HasValue || !o.BedId.HasValue))
                ).Select(o => o.SERVICE_NAME).ToList();

            if (IsNotNullOrEmpty(invalidBeds))
            {
                string str = string.Join(",", invalidBeds);
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ThieuThongTinGiuong, str);
                return false;
            }

            List<long> bedIds = serviceReqDetails.Where(o => o.BedId.HasValue).Select(o => o.BedId.Value).ToList();
            List<HIS_BED> beds = bedIds != null ? HisBedCFG.DATA.Where(o => bedIds.Contains(o.ID)).ToList() : null;
            List<long> bedRoomIds = beds != null ? beds.Select(o => o.BED_ROOM_ID).ToList() : null;

            //Neu co chi dinh giuong thi lay thong tin treatment_bed_room de phuc vu nghiep vu
            if (IsNotNullOrEmpty(bedRoomIds))
            {
                HisTreatmentBedRoomFilterQuery filter = new HisTreatmentBedRoomFilterQuery();
                filter.TREATMENT_ID = treatmentId;
                filter.BED_ROOM_IDs = bedRoomIds;
                List<HIS_TREATMENT_BED_ROOM> treatmentBedRoomTmps = new HisTreatmentBedRoomGet().Get(filter);

                //Neu ko co thong tin vao buong tuong ung voi giuong duoc chon thi bao loi
                if (!IsNotNullOrEmpty(treatmentBedRoomTmps))
                {
                    List<string> bedRooms = HisBedRoomCFG.DATA.Where(o => bedRoomIds != null && bedRoomIds.Contains(o.ID)).Select(o => o.BED_ROOM_NAME).ToList();
                    string str = string.Join(",", bedRooms);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongCoThongTinVaoBuong, str);
                    return false;
                }

                treatmentBedRoomDic = new Dictionary<ServiceReqDetailSDO, HIS_TREATMENT_BED_ROOM>();

                foreach (ServiceReqDetailSDO sdo in serviceReqDetails)
                {
                    if (sdo.BedId.HasValue && sdo.BedStartTime.HasValue && sdo.BedFinishTime.HasValue)
                    {
                        HIS_BED bed = beds.Where(o => o.ID == sdo.BedId.Value).FirstOrDefault();
                        HIS_TREATMENT_BED_ROOM tbr = treatmentBedRoomTmps.Where(o => o.BED_ROOM_ID == bed.BED_ROOM_ID && o.ADD_TIME <= sdo.BedStartTime.Value && (!o.REMOVE_TIME.HasValue || o.REMOVE_TIME.Value >= sdo.BedFinishTime.Value)).FirstOrDefault();
                        if (tbr == null)
                        {
                            string startTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(sdo.BedStartTime.Value);
                            string finishTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(sdo.BedFinishTime.Value);

                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongCoThongTinVaoBuongTrongKhoang, bed.BED_NAME, startTime, finishTime);
                            return false;
                        }
                        else
                        {
                            treatmentBedRoomDic.Add(sdo, tbr);
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Kiem tra xem danh sach dich vu co the duoc xu ly tai phong hay khong
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="sereServs"></param>
        /// <returns></returns>
        internal bool IsProcessable(long roomId, List<HIS_SERE_SERV> sereServs)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(sereServs))
                {
                    //Danh sach cac service ko the duoc xu ly tai phong
                    List<string> unhandleServices = sereServs.Where(o => HisServiceRoomCFG.DATA_VIEW == null || !HisServiceRoomCFG.DATA_VIEW.Exists(t => t.SERVICE_ID == o.SERVICE_ID && t.ROOM_ID == roomId)).Select(o => o.TDL_SERVICE_NAME).ToList();

                    if (IsNotNullOrEmpty(unhandleServices))
                    {
                        string extraMess = string.Join("; ", unhandleServices);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_PhongDuocChiDinhKhongTheXuLyDichVu, extraMess);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra xem cac yeu cau co thuoc danh sach patient_type_id duoc cho phep hay khong
        /// </summary>
        /// <param name="patientTypeAllowIds">Danh sach patient_type_id cho phep duoc chuyen doi sang</param>
        /// <param name="patientTypeId">patient_type_id hien tai</param>
        /// <param name="serviceReqDetails">Danh sach yeu cau</param>
        /// <returns></returns>
        internal bool VerifyPatientTypeAllow(long patientTypeId, List<ServiceReqDetailSDO> serviceReqDetails)
        {
            bool valid = true;
            try
            {
                if (serviceReqDetails != null)
                {
                    //Lay danh sach patient_type_id cho phep chuyen doi tu patient_type_id ma benh nhan duoc ap dung
                    List<long> patientTypeAllowIds = HisPatientTypeAllowCFG.DATA.Where(o => o.PATIENT_TYPE_ID == patientTypeId && o.IS_ACTIVE == Constant.IS_TRUE).Select(o => o.PATIENT_TYPE_ALLOW_ID).ToList();

                    List<long> unallowedList = serviceReqDetails
                        .Where(o => o.PatientTypeId != patientTypeId
                            && (patientTypeAllowIds == null || !patientTypeAllowIds.Contains(o.PatientTypeId)))
                        .Select(o => o.PatientTypeId).ToList();
                    if (IsNotNullOrEmpty(unallowedList))
                    {
                        List<HIS_PATIENT_TYPE> hisPatientTypes = HisPatientTypeCFG.DATA.Where(o => unallowedList.Contains(o.ID)).ToList();

                        List<string> hisPatientTypeNames = hisPatientTypes.Select(o => o.PATIENT_TYPE_NAME).ToList();
                        string hisPatientTypeNameStr = string.Join(", ", hisPatientTypeNames);

                        HIS_PATIENT_TYPE hisPatientTypeNameFrom = HisPatientTypeCFG.DATA.Where(o => o.ID == patientTypeId).FirstOrDefault();
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAllow_KhongTonTaiDuLieu, hisPatientTypeNameFrom.PATIENT_TYPE_NAME, hisPatientTypeNameStr);
                        LogSystem.Warn("Ton tai doi tuong thanh toan khong duoc phep chuyen doi." + LogUtil.TraceData("unallowedList", unallowedList));
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsNotStarted(HIS_SERVICE_REQ data)
        {
            bool valid = true;
            try
            {
                if (data.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChiChoPhepThucHienKhiChuaBatDau);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsStatusNotExecute(HIS_SERVICE_REQ data)
        {
            bool valid = true;
            try
            {
                if (data.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_PhieuChiDinhDaThucHien, data.SERVICE_REQ_CODE);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool HasExecute(HIS_SERVICE_REQ data)
        {
            bool valid = true;
            try
            {
                if (data.IS_NO_EXECUTE == MOS.UTILITY.Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DichVuKhongThucHien);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsNotFinished(HIS_SERVICE_REQ data)
        {
            bool valid = true;
            try
            {
                if (data.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhDaKetThuc, data.SERVICE_REQ_CODE);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsFinished(HIS_SERVICE_REQ data)
        {
            bool valid = true;
            try
            {
                if (data.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhChuaKetThuc, data.SERVICE_REQ_CODE);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra xem don thuoc da co phieu xuat ban chua (trong truong hop ke don thuoc ngoai kho)
        /// </summary>
        /// <param name="serviceReq"></param>
        /// <returns></returns>
        internal bool HasNoSaleExpMest(HIS_SERVICE_REQ raw)
        {
            bool valid = true;
            try
            {
                if (HisServiceReqTypeCFG.PRESCRIPTION_TYPE_IDs.Contains(raw.SERVICE_REQ_TYPE_ID))
                {
                    List<HIS_EXP_MEST> saleExpMests = new HisExpMestGet().GetByPrescriptionId(raw.ID);
                    if (IsNotNullOrEmpty(saleExpMests))
                    {
                        List<string> expMestCodes = saleExpMests.Select(o => o.EXP_MEST_CODE).ToList();
                        string expMestCodeStr = string.Join(",", expMestCodes);

                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_TonTaiPhieuXuatBan, expMestCodeStr);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidInstructionTime(List<long> instructionTimes, HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                List<long> invalidByIntime = instructionTimes != null ? instructionTimes.Where(o => o < treatment.IN_TIME).ToList() : null;
                if (IsNotNullOrEmpty(invalidByIntime))
                {
                    string inTimeStr = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(treatment.IN_TIME);
                    string invalidStr = this.TimeToString(invalidByIntime);

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianYLenhNhoHonThoiGianVaoVien, invalidStr, inTimeStr);
                    return false;
                }

                List<long> invalidByFeelockTime = instructionTimes != null && treatment.FEE_LOCK_TIME.HasValue ? instructionTimes.Where(o => o > treatment.FEE_LOCK_TIME.Value).ToList() : null;

                if (IsNotNullOrEmpty(invalidByFeelockTime))
                {
                    string feeLockTimeStr = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(treatment.FEE_LOCK_TIME.Value);
                    string invalidStr = this.TimeToString(invalidByFeelockTime);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianYLenhLonHonThoiGianKhoaVienPhi, invalidStr, feeLockTimeStr);
                    return false;
                }

                List<long> invalidByOutTime = instructionTimes != null && treatment.OUT_TIME.HasValue ? instructionTimes.Where(o => o > treatment.OUT_TIME.Value).ToList() : null;

                if (IsNotNullOrEmpty(invalidByOutTime))
                {
                    string outTimeStr = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(treatment.OUT_TIME.Value);
                    string invalidStr = this.TimeToString(invalidByOutTime);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianYLenhLonHonThoiGianRaVien, invalidStr, outTimeStr);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidInstructionTime(long instructionTime, HIS_TREATMENT treatment)
        {
            return this.IsValidInstructionTime(new List<long>() { instructionTime }, treatment);
        }

        internal bool IsValidPatientTypeAlter(long instructionTime, List<HIS_PATIENT_TYPE_ALTER> ptas, ref HIS_PATIENT_TYPE_ALTER usingPta)
        {
            bool valid = true;
            try
            {
                usingPta = IsNotNullOrEmpty(ptas) ? ptas.Where(o => o.LOG_TIME <= instructionTime).OrderByDescending(o => o.LOG_TIME).FirstOrDefault() : null;
                if (usingPta == null)
                {
                    string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(instructionTime);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongTonTaiThongTinDienDoiTuongTruocThoiDiemYLenh, "", time);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsAllowAssignService(HIS_SERVICE_REQ data)
        {
            return this.IsAllowAssignService(data.SERVICE_REQ_TYPE_ID, data.REQUEST_LOGINNAME);
        }

        internal bool IsAllowAssignService(long serviceReqTypeId, string requestLoginname)
        {
            try
            {
                if (serviceReqTypeId == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KHAC
                    || serviceReqTypeId == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH
                    || serviceReqTypeId == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONDT
                    || serviceReqTypeId == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK
                    || serviceReqTypeId == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONM
                    || serviceReqTypeId == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONTT
                    || serviceReqTypeId == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__AN)
                {
                    return true;
                }

                //Neu co cau hinh chi cho phep bac sy moi duoc chi dinh DVKT
                if (HisServiceReqCFG.JUST_ALLOW_DOCTOR_ASSIGN_SERVICE
                    && serviceReqTypeId != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__G //key nay ko check khi chi dinh giuong
                    && !HisEmployeeUtil.IsDoctor(requestLoginname))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_BanKhongPhaiLaBacSy, requestLoginname);
                    return false;
                }

                //Neu co cau hinh chi cho phep nguoi chi dinh co chung chi hanh nghe moi duoc phep chi dinh cho BN
                if (HisServiceReqCFG.REQ_USER_MUST_HAVE_DIPLOMA
                    && !HisEmployeeUtil.HasDiploma(requestLoginname))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_NguoiChiDinhKhongCoChungChiHanhNghe, requestLoginname);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        internal bool HasNoTempBed(List<HIS_SERE_SERV> exists)
        {
            bool result = true;
            try
            {
                List<string> serviceNames = IsNotNullOrEmpty(exists) ? exists.Where(o => o.AMOUNT_TEMP.HasValue && o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G).Select(o => o.TDL_SERVICE_NAME).ToList() : null;

                if (IsNotNullOrEmpty(serviceNames))
                {
                    string nameStr = string.Join(",", serviceNames);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChiPhiGiuongTamTinh, nameStr);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool IsTypeRation(List<HIS_SERVICE_REQ> datas)
        {
            bool valid = true;
            try
            {
                var notTypes = datas != null ? datas.Where(o => o.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__AN).ToList() : null;
                if (IsNotNullOrEmpty(notTypes))
                {
                    string codes = String.Join(",", notTypes.Select(s => s.SERVICE_REQ_CODE).ToList());
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongPhaiYeuCauLoaiSuatAn, codes);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsTypeRation(HIS_SERVICE_REQ data)
        {
            bool valid = true;
            try
            {
                if (IsNotNull(data) && data.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__AN)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongPhaiYeuCauLoaiSuatAn, data.SERVICE_REQ_CODE);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsTypeXN(HIS_SERVICE_REQ data)
        {
            bool valid = true;
            try
            {
                if (IsNotNull(data) && data.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongPhaiYeuCauLoaiXetNghiem, data.SERVICE_REQ_CODE);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool HasNoRationSum(List<HIS_SERVICE_REQ> datas)
        {
            bool valid = true;
            try
            {
                var notTypes = datas != null ? datas.Where(o => o.RATION_SUM_ID.HasValue).ToList() : null;
                if (IsNotNullOrEmpty(notTypes))
                {
                    string codes = String.Join(",", notTypes.Select(s => s.SERVICE_REQ_CODE).ToList());
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YeuCauSuatAnDaDuocDuyet, codes);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool HasNoRationSum(HIS_SERVICE_REQ data)
        {
            bool valid = true;
            try
            {
                if (IsNotNull(data) && data.RATION_SUM_ID.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YeuCauSuatAnDaDuocDuyet, data.SERVICE_REQ_CODE);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        private string TimeToString(List<long> times)
        {
            if (times != null && times.Count > 0)
            {
                string result = "";
                foreach (long t in times)
                {
                    string timeStr = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(t);
                    result = result + "; " + timeStr;
                }
                return result.Substring(2, result.Length - 2);
            }
            return "";
        }

        internal bool VerifyAssignedExecuteUser(string assignedLoginname, long roomId)
        {
            bool valid = true;
            try
            {
                if (!String.IsNullOrWhiteSpace(assignedLoginname))
                {
                    List<HIS_USER_ROOM> uRooms = HisUserRoomCFG.DATA.Where(o => o.IS_ACTIVE == Constant.IS_TRUE && o.ROOM_ID == roomId && o.LOGINNAME == assignedLoginname).ToList();
                    if (!IsNotNullOrEmpty(uRooms))
                    {
                        V_HIS_ROOM r = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == roomId);
                        string name = r != null ? r.ROOM_NAME : "";
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.TaiKhoanKhongCoQuyenVaoPhong, assignedLoginname, name);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool VerifySessionCode(string sessionCode, long treatmentId, List<HIS_SERE_SERV> allSereServs, ref List<HIS_SERVICE_REQ> lstData, ref List<HIS_SERE_SERV> lstSereServDelete, ref List<HIS_BED_LOG> lstBedLogDelete)
        {
            bool valid = true;
            try
            {
                if (!String.IsNullOrWhiteSpace(sessionCode))
                {
                    HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                    filter.TREATMENT_ID = treatmentId;
                    filter.SESSION_CODE__EXACT = sessionCode;
                    List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(filter);

                    if (!IsNotNullOrEmpty(serviceReqs))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("SessionCode invalid: " + sessionCode);
                    }
                    lstData = serviceReqs;
                    lstSereServDelete = allSereServs.Where(o => serviceReqs.Any(a => a.ID == o.SERVICE_REQ_ID)).ToList();

                    List<long> serviceReqIds = serviceReqs.Select(o => o.ID).ToList();
                    lstBedLogDelete = new HisBedLogGet().GetByServiceReqIds(serviceReqIds);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsAllowNotChoiceService(HIS_SERVICE_REQ req)
        {
            bool valid = true;
            try
            {
                V_HIS_EXECUTE_ROOM exeRoom = HisExecuteRoomCFG.DATA.FirstOrDefault(o => o.ROOM_ID == req.EXECUTE_ROOM_ID);
                if (exeRoom == null || exeRoom.ALLOW_NOT_CHOOSE_SERVICE != Constant.IS_TRUE)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("EXECUTE_ROOM.ALLOW_NOT_CHOOSE_SERVICE <> 1");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidStartOrFinishTimeForSereServExt(long id, long? startTime, long? finishTime, ref List<HIS_SERE_SERV_EXT> ssExts)
        {
            bool valid = true;
            try
            {
                ssExts = new HisSereServExtGet().GetByServiceReqId(id);
                if (IsNotNullOrEmpty(ssExts))
                {
                    // Truong hop cap nhat ca thoi gian "Bat Dau" va thoi gian "Ket Thuc"
                    if (startTime.HasValue && finishTime.HasValue)
                    {
                        var minValidExts = ssExts.Where(o => o.BEGIN_TIME < startTime.Value).ToList();
                        if (IsNotNullOrEmpty(minValidExts))
                        {
                            minValidExts.ForEach(o => o.BEGIN_TIME = startTime.Value);
                        }
                        else
                        {
                            List<HIS_SERE_SERV_EXT> minExts = ssExts.GroupBy(g => g.BEGIN_TIME).OrderBy(o => o.FirstOrDefault().BEGIN_TIME).FirstOrDefault().ToList();
                            minExts.ForEach(o => o.BEGIN_TIME = startTime.Value);
                        }

                        var maxValidExts = ssExts.Where(o => o.END_TIME > finishTime.Value).ToList();
                        if (IsNotNullOrEmpty(maxValidExts))
                        {
                            maxValidExts.ForEach(o => o.END_TIME = finishTime.Value);
                        }
                        else
                        {
                            List<HIS_SERE_SERV_EXT> maxExts = ssExts.GroupBy(g => g.END_TIME).OrderByDescending(o => o.FirstOrDefault().END_TIME).FirstOrDefault().ToList();
                            maxExts.ForEach(o => o.END_TIME = finishTime.Value);
                        }
                        if (ssExts.Exists(o => o.BEGIN_TIME > o.END_TIME))
                        {
                            LogSystem.Error("Khi cap nhat dong thoi thoi gian bat dau va ket thuc");
                            MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianBatDauYLenhLonHonThoiGianKetThucDichVu);
                            return false;
                        }
                    }
                    else  // Truong hop cap nhat thoi gian "Bat Dau" hoac thoi gian "Ket thuc"
                    {
                        if (startTime.HasValue)
                        {
                            var ssExt = ssExts.OrderBy(o => o.END_TIME).FirstOrDefault();
                            if (startTime.Value > ssExt.END_TIME)
                            {
                                var ss = new HisSereServGet().GetById(ssExt.SERE_SERV_ID);
                                string svName = ss != null ? ss.TDL_SERVICE_NAME : "";
                                MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianBatDauYLenhLonHonThoiGianKetThucDichVu, svName);
                                return false;
                            }

                            // Gan du lieu moi
                            var minValidExts = ssExts.Where(o => o.BEGIN_TIME < startTime.Value).ToList();
                            if (IsNotNullOrEmpty(minValidExts))
                            {
                                minValidExts.ForEach(o => o.BEGIN_TIME = startTime.Value);
                            }
                            else
                            {
                                HIS_SERE_SERV_EXT minExt = ssExts.OrderBy(o => o.BEGIN_TIME).FirstOrDefault();
                                minExt.BEGIN_TIME = startTime.Value;
                            }
                        }
                        else if (finishTime.HasValue)
                        {
                            var ssExt = ssExts.OrderByDescending(o => o.BEGIN_TIME).FirstOrDefault();
                            if (finishTime.Value < ssExt.BEGIN_TIME)
                            {
                                var ss = new HisSereServGet().GetById(ssExt.SERE_SERV_ID);
                                string svName = ss != null ? ss.TDL_SERVICE_NAME : "";
                                MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianKetThucYLenhNhoHonThoiGianBatDauDichVu, svName);
                                return false;
                            }

                            // Gan du lieu moi
                            var maxValidExts = ssExts.Where(o => o.END_TIME > finishTime.Value).ToList();
                            if (IsNotNullOrEmpty(maxValidExts))
                            {
                                maxValidExts.ForEach(o => o.END_TIME = finishTime.Value);
                            }
                            else
                            {
                                HIS_SERE_SERV_EXT maxExt = ssExts.OrderByDescending(o => o.END_TIME).FirstOrDefault();
                                maxExt.END_TIME = finishTime.Value;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsAllowAssignServiceWithOxy(List<V_HIS_SERVICE> services)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(services))
                {
                    List<long> typeIds = new List<long>()
                    {
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC,
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT,
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU
                    };

                    if (!HisServiceReqCFG.IS_ALLOW_ASSIGN_OXYGEN)
                    {
                        var inValidServices = services.Where(o => typeIds.Contains(o.SERVICE_TYPE_ID)).ToList();
                        if (IsNotNullOrEmpty(inValidServices))
                        {
                            string names = string.Join(", ", inValidServices.Select(o => o.SERVICE_NAME).ToList());
                            MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DichVuKhongDuocPhepChiDinh, names);
                            return false;
                        }
                    }
                    else
                    {
                        var thuocServices = services.Where(o => o.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC).ToList();
                        if (IsNotNullOrEmpty(thuocServices))
                        {
                            var thuocMedicineTypes = HisMedicineTypeCFG.DATA.Where(o => thuocServices.Select(s => s.ID).Contains(o.SERVICE_ID)).ToList();
                            if (thuocMedicineTypes != null && thuocMedicineTypes.Exists(o => o.IS_OXYGEN != Constant.IS_TRUE))
                            {
                                string names = string.Join(", ", thuocServices.Select(o => o.SERVICE_NAME).ToList());
                                MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DichVuKhongDuocPhepChiDinh, names);
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidAccount()
        {
            bool valid = true;
            try
            {
                // Key = 1, Co thong tin dang nhap, va Khong phai la bac si thi chan
                string loginName = ResourceTokenManager.GetLoginName();
                if (HisServiceReqCFG.IS_ALLOW_FINISH_WHEN_ACCOUNT_IS_DOCTOR && !string.IsNullOrWhiteSpace(loginName) && !HisEmployeeUtil.IsDoctor(loginName))
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisEmployee_BanKhongPhaiLaBacSy);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsAdminOrReqAccount(HIS_SERVICE_REQ serviceReq)
        {
            bool valid = true;
            try
            {
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                if (serviceReq != null && serviceReq.REQUEST_LOGINNAME != loginName && !HisEmployeeUtil.IsAdmin(loginName))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChiChoPhepSuaDonDoMinhTaoHoacChiDinh);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidBHYTServices(List<ServiceReqDetailSDO> serviceReqDetails)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(serviceReqDetails))
                {
                    var invalidReqs = serviceReqDetails.Where(o => o.PatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && o.IsNotUseBhyt == true).ToList();
                    if (IsNotNullOrEmpty(invalidReqs))
                    {
                        List<V_HIS_SERVICE> services = HisServiceCFG.DATA_VIEW.Where(o => invalidReqs != null && invalidReqs.Exists(t => t.ServiceId == o.ID)).ToList();
                        List<string> names = services.Select(o => o.SERVICE_NAME).ToList();
                        string name = string.Join(", ", names);
                        MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongChoPhepChonKhongHuongBHYTVoiCacDichVuDTTTLaBHYT);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                valid = false;
                LogSystem.Error(ex);
            }

            return valid;
        }

        internal bool IsValidDoNotUseBHYT(List<ServiceReqDetailSDO> serviceReqDetails)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(serviceReqDetails))
                {
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    if (!HisEmployeeUtil.IsAdmin(loginName))
                    {
                        var invalidReqs = serviceReqDetails.Where(o => o.PatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).ToList();
                        if (IsNotNullOrEmpty(invalidReqs))
                        {
                            List<V_HIS_SERVICE> services = HisServiceCFG.DATA_VIEW.Where(o => invalidReqs != null && invalidReqs.Exists(t => t.ServiceId == o.ID)).ToList();
                            if (IsNotNullOrEmpty(services))
                            {
                                var serviceValid = services.Where(o => o.DO_NOT_USE_BHYT == Constant.IS_TRUE).ToList();
                                if (serviceValid != null && serviceValid.Count() > 0)
                                {
                                    List<string> lstValid = serviceValid.Select(o => string.Format("{0} - {1}", o.SERVICE_CODE, o.SERVICE_NAME)).ToList();
                                    string strValid = string.Join(", ", lstValid);

                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DichVuDuocCauHinhKhongHuongBHYT, strValid);
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                valid = false;
                LogSystem.Error(ex);
            }

            return valid;
        }

        internal bool IsTypeDONDT(HIS_SERVICE_REQ data)
        {
            bool valid = true;
            try
            {
                if (data.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONDT)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhKhongPhaiDonDieuTri);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
