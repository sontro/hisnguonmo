using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDeathCertBook;
using MOS.MANAGER.HisDepartment;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisDocumentBook;
using MOS.MANAGER.HisExecuteRoom;
using MOS.MANAGER.HisProgram;
using MOS.MANAGER.HisRationSum;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTransaction;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update.Finish
{
    class HisTreatmentFinishCheck : BusinessBase
    {
        internal HisTreatmentFinishCheck()
            : base()
        {
        }

        internal HisTreatmentFinishCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool IsValidForFinish(HisTreatmentFinishSDO data, HIS_TREATMENT treatment, List<HIS_SERE_SERV> existsSereServs, List<HIS_PATIENT_TYPE_ALTER> ptas, ref HIS_DEPARTMENT_TRAN lastDt, ref WorkPlaceSDO workPlace, ref HIS_PROGRAM program, ref V_HIS_DEATH_CERT_BOOK deathCertBook)
        {
            bool valid = true;
            try
            {
                HIS_PATIENT_TYPE_ALTER lastPta = ptas != null ? ptas.OrderByDescending(o => o.LOG_TIME).FirstOrDefault() : null;
                List<HIS_SERVICE_REQ> serviceReqs = null;
                //HIS_DEPARTMENT department = new HisDepartmentGet().GetById(lastDt.DEPARTMENT_ID);


                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                HisTreatmentCheckPrescription presChecker = new HisTreatmentCheckPrescription(param);
                HisDepartmentTranCheck dtChecker = new HisDepartmentTranCheck(param);

                valid = valid && checker.IsUnLock(treatment);
                valid = valid && checker.IsUnTemporaryLock(treatment);
                valid = valid && checker.IsUnpause(treatment);
                valid = valid && this.HasWorkPlaceInfo(data.EndRoomId, ref workPlace);
                valid = valid && checker.IsValidDepartment(data.TreatmentId, workPlace, ref lastDt);
                valid = valid && this.VerifyRequireField(data, lastPta, ref program);

                //Neu luu tam thi ko check cac nghiep vu duoi
                valid = valid && (data.IsTemporary || this.IsValidBhytClinicalEmergencyBedPolicy(data, treatment, existsSereServs));
                valid = valid && (data.IsTemporary || this.FinishTimeIsNotGreatherThenCurrentTime(data));
                valid = valid && this.IsValidOutTime(treatment, data, existsSereServs, ptas, lastPta, lastDt);
                valid = valid && (data.IsTemporary || this.VerifyFinishServiceReq(data, treatment, workPlace, lastPta, existsSereServs, ref serviceReqs));
                valid = valid && (data.IsTemporary || this.IsValidMinimumTimesForExam(treatment, serviceReqs, data.TreatmentFinishTime));
                valid = valid && (data.IsTemporary || this.IsIntructionTimeNotGreaterThanFinishTime(serviceReqs));
                valid = valid && (data.IsTemporary || this.VerifyApproveRation(serviceReqs, lastPta));
                valid = valid && (data.IsTemporary || presChecker.HasNoPostponePrescriptionByFinish(serviceReqs, lastPta, lastDt));
                valid = valid && (data.IsTemporary || dtChecker.IsFinishCoTreatment(lastDt));
                valid = valid && (data.IsTemporary || treatment.IS_OLD_TEMP_BED != Constant.IS_TRUE || dtChecker.HasNoTempBed(treatment.ID, lastDt.DEPARTMENT_ID)); //chi check giuong tam tinh voi cac ban ghi cu (cac ban ghi nay se chan vi ap dung theo co che cu)
                valid = valid && (data.IsTemporary || presChecker.IsMustApproveMobaPress(treatment));
                valid = valid && (data.IsTemporary || this.VerifyTimeSereServExt(data, treatment, existsSereServs));
                valid = valid && (data.IsTemporary || this.IsFinishMandatoryService(treatment, existsSereServs));
                valid = valid && checker.VerifyTreatmentFinishCheckTracking(data);
                valid = valid && (!data.IsExpXml4210Collinear || this.HasXml4210CollinearFolderPath());
                valid = valid && this.VerifyBlockAppointment(data, lastPta);
                valid = valid && this.IsValidDeathCertBook(data, workPlace, ref deathCertBook);
                valid = valid && this.ValidAppointment(data, existsSereServs);
                valid = valid && this.IsValidForCreateNewTreatment(data);
                valid = valid && this.IsValidIcdCode(treatment,data);
                valid = valid && this.IsValidAbortionInfo(data);
                valid = valid && this.IsValidTemporaryPres(serviceReqs);
                valid = valid && this.DoNotFinishTransactionNotIsActive(data);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        private bool IsValidForCreateNewTreatment(HisTreatmentFinishSDO data)
        {
            try
            {
                if (data.IsCreateNewTreatment && !data.NewTreatmentInTime.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ChuaNhapThoiGianVaoVienCuaHoSoMoi);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        private bool ValidAppointment(HisTreatmentFinishSDO data, List<HIS_SERE_SERV> existsSereServs)
        {
            try
            {
                if (HisTreatmentCFG.AUTO_CREATE_WHEN_APPOINTMENT &&
                    data.TreatmentEndTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__HEN)
                {
                    if (data.AppointmentExamRoomIds != null && data.AppointmentExamRoomIds.Count > 1)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ChonNhieuHon1PhongKham);
                        return false;
                    }
                    if (IsNotNullOrEmpty(data.AppointmentExamRoomIds))
                    {
                        List<V_HIS_EXECUTE_ROOM> executeRooms = HisExecuteRoomCFG.DATA.Where(o => data.AppointmentExamRoomIds.Contains(o.ROOM_ID)).ToList();
                        if (IsNotNullOrEmpty(executeRooms))
                        {
                            List<V_HIS_EXECUTE_ROOM> listExecuteRoomNos = executeRooms.Where(o => o.ALLOW_NOT_CHOOSE_SERVICE == Constant.IS_FALSE).ToList();
                            HIS_SERE_SERV mainExam = null;
                            if (IsNotNullOrEmpty(existsSereServs))
                            {
                                if (data.ServiceReqId.HasValue)
                                {
                                    mainExam = existsSereServs.FirstOrDefault(o => o.SERVICE_REQ_ID == data.ServiceReqId.Value);
                                }
                                else
                                {
                                    mainExam = existsSereServs.Where(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH &&
                                        o.IS_NO_EXECUTE != Constant.IS_TRUE).OrderByDescending(o => o.TDL_IS_MAIN_EXAM == 1 ? 1 : 0).FirstOrDefault();
                                }
                                if (!existsSereServs.Exists(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH) && IsNotNullOrEmpty(listExecuteRoomNos) && mainExam == null)
                                {
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_KhongTimThayThongTinChiDinhKham);
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Khong cho phep luu neu ton tai BEGIN_TIME nho hon IN_TIME và END_TIME lon hon OUT_TIME
        /// </summary>
        /// <param name="treatment"></param>
        /// <param name="serviceReqs"></param>
        /// <returns></returns>
        public bool VerifyTimeSereServExt(HisTreatmentFinishSDO data, HIS_TREATMENT treatment, List<HIS_SERE_SERV> existsSereServs)
        {
            try
            {
                //khong kiem tra cac dich vu la hao phi va khong thuc hien vi ko dua vao xml
                var ssHasExt = IsNotNullOrEmpty(existsSereServs) ? existsSereServs
                    .Where(o => HisServiceReqTypeCFG.CLINICAL_TYPE_IDs.Contains(o.TDL_SERVICE_REQ_TYPE_ID) || HisServiceReqTypeCFG.SUBCLINICAL_TYPE_IDs.Contains(o.TDL_SERVICE_REQ_TYPE_ID))
                    .Where(o => o.IS_EXPEND != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && o.IS_NO_EXECUTE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                    .ToList() : null;
                if (IsNotNullOrEmpty(ssHasExt))
                {
                    HisSereServExtFilterQuery filter = new HisSereServExtFilterQuery();
                    filter.SERE_SERV_IDs = ssHasExt.Select(s => s.ID).ToList();
                    var sereServExts = new HisSereServExtGet().Get(filter);
                    if (IsNotNullOrEmpty(sereServExts))
                    {
                        var extBeforeIn = sereServExts.Where(o => o.BEGIN_TIME < treatment.IN_TIME).ToList();

                        var extAfterOut = sereServExts.Where(o => o.END_TIME > data.TreatmentFinishTime).ToList();

                        List<string> beforeCode = new List<string>();
                        if (IsNotNullOrEmpty(extBeforeIn))
                        {
                            foreach (var item in extBeforeIn)
                            {
                                beforeCode.Add(string.Format("{0}-{1}",
                                    ssHasExt.FirstOrDefault(o => o.ID == item.SERE_SERV_ID).TDL_SERVICE_CODE,
                                    ssHasExt.FirstOrDefault(o => o.ID == item.SERE_SERV_ID).TDL_SERVICE_REQ_CODE));
                            }
                        }

                        List<string> afterCode = new List<string>();
                        if (IsNotNullOrEmpty(extAfterOut))
                        {
                            foreach (var item in extAfterOut)
                            {
                                afterCode.Add(string.Format("{0}-{1}",
                                    ssHasExt.FirstOrDefault(o => o.ID == item.SERE_SERV_ID).TDL_SERVICE_CODE,
                                    ssHasExt.FirstOrDefault(o => o.ID == item.SERE_SERV_ID).TDL_SERVICE_REQ_CODE));
                            }
                        }

                        if (IsNotNullOrEmpty(beforeCode) || IsNotNullOrEmpty(afterCode))
                        {
                            if (IsNotNullOrEmpty(beforeCode))
                            {
                                string tmp = string.Join(",", beforeCode);
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_CacDichVuSauCoThoiGianBatDauXuLyTruocThoiGianVaoVien, tmp);
                            }

                            if (IsNotNullOrEmpty(afterCode))
                            {
                                string tmp = string.Join(",", afterCode);
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_CacDichVuSauCoThoiGianKetThucXuLySauThoiGianRaVien, tmp);
                            }

                            return false;
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

        /// <summary>
        /// Doi voi BN dieu tri noi tru co cong kham chinh thuoc phong kham cap cuu, 
        /// co thoi gian dieu tri be hon 4h nhung lai co chi dinh ngay giuong co gia
        /// lon hon 0 --> thi hien thi canh bao, ko cho phep ket thuc
        /// </summary>
        /// <param name="data"></param>
        /// <param name="treatment"></param>
        /// <param name="existsSereServs"></param>
        /// <returns></returns>
        public bool IsValidBhytClinicalEmergencyBedPolicy(HisTreatmentFinishSDO data, HIS_TREATMENT treatment, List<HIS_SERE_SERV> existsSereServs)
        {
            if (HisHeinBhytCFG.EMERGENCY_EXAM_POLICY_OPTION == HisHeinBhytCFG.EmergencyExamPolicyOption.BY_EXECUTE_ROOM
                && treatment.CLINICAL_IN_TIME.HasValue
                && IsNotNullOrEmpty(existsSereServs))
            {
                //Kiem tra xem BN co cong kham chinh o phong kham cap cuu ko
                //(Can cu vao phong xu ly cua dich vu kham duoc danh dau la kham chinh)
                HIS_SERE_SERV firstExam = null;
                if (HisTreatmentCFG.IS_PRICING_FIRST_EXAM_AS_MAIN_EXAM)
                {
                    firstExam = existsSereServs
                        .Where(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH
                            && o.IS_NO_EXECUTE != Constant.IS_TRUE && o.SERVICE_REQ_ID.HasValue)
                        .OrderBy(o => o.TDL_INTRUCTION_TIME).ThenBy(o => o.ID).FirstOrDefault();
                }
                else
                {
                    firstExam = existsSereServs
                        .Where(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH
                            && o.IS_NO_EXECUTE != Constant.IS_TRUE && o.SERVICE_REQ_ID.HasValue)
                        .OrderByDescending(o => o.TDL_IS_MAIN_EXAM)
                        .ThenBy(o => o.TDL_INTRUCTION_TIME)
                        .ThenBy(o => o.ID).FirstOrDefault();
                }

                V_HIS_EXECUTE_ROOM firstExamRoom = firstExam != null ? HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == firstExam.TDL_EXECUTE_ROOM_ID).FirstOrDefault() : null;

                DateTime? clinicalInTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(treatment.CLINICAL_IN_TIME.Value);
                DateTime? outTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.TreatmentFinishTime);
                if (clinicalInTime.HasValue && outTime.HasValue && firstExamRoom != null && firstExamRoom.IS_EMERGENCY == Constant.IS_TRUE)
                {
                    //Tinh thoi gian dieu tri noi tru
                    double hours = (outTime.Value - clinicalInTime.Value).TotalSeconds / 3600;

                    //Neu thoi gian dieu tri noi tru be hon 4h
                    //va ton tai du lieu "ngay giuong" co doi tuong thanh toan BHYT co gia > 0 
                    //thi hien thi canh bao, ko cho ket thuc dieu tri
                    bool existsBed = existsSereServs.Exists(o =>
                        o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G
                        && o.IS_NO_EXECUTE != Constant.IS_TRUE
                        && o.SERVICE_REQ_ID.HasValue
                        && o.IS_EXPEND != Constant.IS_TRUE && o.VIR_PRICE > 0
                        && o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT);
                    if (hours < BhytConstant.CLINICAL_TIME_FOR_EMERGENCY && existsBed)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_BenhNhanBhytCapCuuBeHon4hCoNgayGiuong);
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Kiem tra xem cac phieu chi dinh da duoc xu ly het chua (trong truong hop co cau hinh)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="existsSereServs"></param>
        /// <returns></returns>
        public bool VerifyFinishServiceReq(HisTreatmentFinishSDO data, HIS_TREATMENT treatment, WorkPlaceSDO workPlace, HIS_PATIENT_TYPE_ALTER pta, List<HIS_SERE_SERV> existsSereServs, ref List<HIS_SERVICE_REQ> serviceReqs)
        {
            try
            {
                //Va viec ket thuc ko phai la "luu tam thoi"
                if (!data.IsTemporary)
                {
                    HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                    filter.HAS_EXECUTE = true;
                    filter.TREATMENT_ID = data.TreatmentId;
                    serviceReqs = new HisServiceReqGet().Get(filter);

                    List<string> invalidFinishLists = IsNotNullOrEmpty(serviceReqs) ? serviceReqs
                        .Where(o => o.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT
                            && (HisServiceReqTypeCFG.CLINICAL_TYPE_IDs.Contains(o.SERVICE_REQ_TYPE_ID) || HisServiceReqTypeCFG.SUBCLINICAL_TYPE_IDs.Contains(o.SERVICE_REQ_TYPE_ID))
                            && o.IS_NOT_REQUIRED_COMPLETE != Constant.IS_TRUE
                        && o.FINISH_TIME > data.TreatmentFinishTime)
                        .Select(o => o.SERVICE_REQ_CODE)
                        .ToList() : null;

                    if (IsNotNullOrEmpty(invalidFinishLists))
                    {
                        string codeStr = string.Join(",", invalidFinishLists);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ThoiGianTraKetQuaLonHonThoiGianKetThucDieuTri, codeStr);
                        return false;
                    }

                    List<long> autoFinishServiceIds = HisTreatmentCFG.AutoFinishServiceIds(workPlace.BranchId);

                    if (HisTreatmentCFG.MUST_FINISH_ALL_EXAM_BEFORE_FINISH_TREATMENT)
                    {
                        //Kiem tra xem co y/c kham nao chua duoc ket thuc hay ko (ko tinh y/c kham hien tai)
                        List<HIS_SERVICE_REQ> unfinishExams = IsNotNullOrEmpty(serviceReqs) ? serviceReqs
                            .Where(o => o.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT
                                && o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH
                                && o.ID != data.ServiceReqId
                                && existsSereServs != null
                                && existsSereServs.Exists(t => t.SERVICE_REQ_ID == o.ID && (autoFinishServiceIds == null || !autoFinishServiceIds.Contains(t.SERVICE_ID))))
                            .ToList() : null;

                        if (IsNotNullOrEmpty(unfinishExams))
                        {
                            string examStr = "";
                            foreach (HIS_SERVICE_REQ exam in unfinishExams)
                            {
                                string executeRoomName = HisExecuteRoomCFG.DATA != null ? HisExecuteRoomCFG.DATA.Where(o => exam.EXECUTE_ROOM_ID == o.ROOM_ID).Select(o => o.EXECUTE_ROOM_NAME).FirstOrDefault() : null;
                                examStr += string.Format("{0} ({1}), ", exam.SERVICE_REQ_CODE, executeRoomName);
                            }
                            examStr = examStr.Substring(0, examStr.Length - 2);

                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_YeuCauKhamChuaKetThucChiChoPhepXuTriKetThucKham, examStr);
                            return false;
                        }
                    }


                    //Neu co cau hinh "bat buoc ket thuc toan bo dich vu truoc khi ket thuc ho so dieu tri"
                    if (HisTreatmentCFG.MUST_FINISH_ALL_SERVICES_BEFORE_FINISH_TREATMENT == HisTreatmentCFG.MustFinishAllServicesBeforeFinishTreatment.BLOCK_AND_WARNING)
                    {
                        //Neu co cau hinh danh sach dich vu tu dong hoan thanh thi 
                        //lay ra danh sach cac phieu y lenh de bo qua kiem tra
                        List<HIS_SERE_SERV> autos = existsSereServs != null ? existsSereServs
                            .Where(o => !o.IS_NO_EXECUTE.HasValue
                                && autoFinishServiceIds != null
                                && autoFinishServiceIds.Contains(o.SERVICE_ID)).ToList() : null;

                        List<long> notCheckIds = IsNotNullOrEmpty(autos) ? autos.Select(o => o.SERVICE_REQ_ID.Value).Distinct().ToList() : null;
                        //Neu trong yeu cau co nhieu dich vu, co dich vu tu dong ket thuc, dich vu khac thi khong tu dong thi van can check ket thuc
                        notCheckIds = IsNotNullOrEmpty(notCheckIds) ? notCheckIds.Where(o => !existsSereServs.Any(a => o == a.SERVICE_REQ_ID && !autos.Any(e => e.ID == a.ID))).ToList() : null;

                        //lay cac y lenh lam sang va can lam sang
                        List<HIS_SERVICE_REQ> unFinishServiceReqs = IsNotNullOrEmpty(serviceReqs) ? serviceReqs
                            .Where(o => notCheckIds == null || !notCheckIds.Contains(o.ID))
                            .Where(o => HisServiceReqTypeCFG.CLINICAL_TYPE_IDs.Contains(o.SERVICE_REQ_TYPE_ID) || HisServiceReqTypeCFG.SUBCLINICAL_TYPE_IDs.Contains(o.SERVICE_REQ_TYPE_ID))
                            .Where(o => o.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL || o.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                            //ko check doi voi dich vu kham ma nguoi dung dang ket thuc dieu tri tu man hinh xu ly cua dich vu kham day
                            //(trong truong hop ket thuc dieu tri o man hinh xu ly kham)
                            .Where(o => o.ID != data.ServiceReqId)
                            .Where(o => o.IS_NOT_REQUIRED_COMPLETE != 1)
                            .ToList() : null;
                        if (IsNotNullOrEmpty(unFinishServiceReqs))
                        {
                            string str = "";
                            foreach (HIS_SERVICE_REQ exam in unFinishServiceReqs)
                            {
                                string executeRoomName = HisExecuteRoomCFG.DATA != null ? HisExecuteRoomCFG.DATA.Where(o => exam.EXECUTE_ROOM_ID == o.ROOM_ID).Select(o => o.EXECUTE_ROOM_NAME).FirstOrDefault() : null;
                                str += string.Format("{0} ({1}), ", exam.SERVICE_REQ_CODE, executeRoomName);
                            }
                            str = str.Substring(0, str.Length - 2);

                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_CacPhieuChiDinhSauChuaKetThuc, str);
                            return false;
                        }

                    }
                    else if (HisTreatmentCFG.MUST_FINISH_ALL_SERVICES_BEFORE_FINISH_TREATMENT == HisTreatmentCFG.MustFinishAllServicesBeforeFinishTreatment.BLOCK_AND_WARNING_WITH_NOI_TRU
                        && treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
                    {
                        var invalidReqs = IsNotNullOrEmpty(serviceReqs) ? serviceReqs.Where(o =>
                                                    o.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT
                                                 && o.IS_NO_EXECUTE != Constant.IS_TRUE && o.IS_NOT_REQUIRED_COMPLETE != Constant.IS_TRUE
                                                 && (HisServiceReqTypeCFG.CLINICAL_TYPE_IDs.Contains(o.SERVICE_REQ_TYPE_ID) || HisServiceReqTypeCFG.SUBCLINICAL_TYPE_IDs.Contains(o.SERVICE_REQ_TYPE_ID))
                                             ).ToList() : null;
                        if (IsNotNullOrEmpty(invalidReqs))
                        {
                            List<string> codes = new List<string>();
                            foreach (var item in invalidReqs)
                            {
                                string executeRoomName = HisExecuteRoomCFG.DATA != null ? HisExecuteRoomCFG.DATA.Where(o => item.EXECUTE_ROOM_ID == o.ROOM_ID).Select(o => o.EXECUTE_ROOM_NAME).FirstOrDefault() : null;
                                string code = string.Format("{0} ({1})", item.SERVICE_REQ_CODE, executeRoomName);
                                codes.Add(code);
                            }
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CacPhieuChiDinhSauChuaHoanThanh, string.Join(", ", codes));
                            return false;
                        }
                    }

                    //thoi gian ket thuc dich vu phai be hon thoi gian ra vien
                    if (HisTreatmentCFG.SERVICE_FINISH_TIME_MUST_BE_LESS_THAN_OUT_TIME > 0)
                    {
                        List<HIS_SERE_SERV> checkSereServ = existsSereServs != null ? existsSereServs.Where(o =>
                            HisServiceTypeCFG.SUBCLINICAL_TYPE_IDs.Contains(o.TDL_SERVICE_TYPE_ID)
                            && (autoFinishServiceIds == null || !autoFinishServiceIds.Contains(o.SERVICE_ID))).ToList() : null;

                        if (IsNotNullOrEmpty(checkSereServ))
                        {
                            List<HIS_SERE_SERV_EXT> sereServExtCheck = new HisSereServExt.HisSereServExtGet().GetBySereServIds(checkSereServ.Select(s => s.ID).ToList());
                            long lastSereServEndTimeValue = 0;
                            HIS_SERE_SERV lastSereServ = null;
                            foreach (var item in checkSereServ)
                            {
                                HIS_SERE_SERV_EXT ext = IsNotNullOrEmpty(sereServExtCheck) ? sereServExtCheck.FirstOrDefault(o => o.SERE_SERV_ID == item.ID) : null;
                                HIS_SERVICE_REQ req = IsNotNullOrEmpty(serviceReqs) ? serviceReqs.FirstOrDefault(o => o.ID == item.SERVICE_REQ_ID) : null;
                                if (ext != null && ext.END_TIME.HasValue && ext.END_TIME.Value > lastSereServEndTimeValue)
                                {
                                    lastSereServEndTimeValue = ext.END_TIME.Value;
                                    lastSereServ = item;
                                }
                                else if (req != null && req.FINISH_TIME.HasValue && req.FINISH_TIME.Value > lastSereServEndTimeValue)
                                {
                                    lastSereServEndTimeValue = req.FINISH_TIME.Value;
                                    lastSereServ = item;
                                }
                            }

                            if (lastSereServEndTimeValue > 0 && Inventec.Common.DateTime.Check.IsValidTime(lastSereServEndTimeValue) && lastSereServ != null)
                            {
                                int serviceFinishTimeMustBeLessThanOutTime = HisTreatmentCFG.SERVICE_FINISH_TIME_MUST_BE_LESS_THAN_OUT_TIME;
                                var lastSereServEndTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(lastSereServEndTimeValue).Value;
                                lastSereServEndTime = lastSereServEndTime.AddSeconds(-lastSereServEndTime.Second);
                                var treatmentFinishTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.TreatmentFinishTime).Value;
                                treatmentFinishTime = treatmentFinishTime.AddSeconds(-treatmentFinishTime.Second);
                                if ((treatmentFinishTime - lastSereServEndTime).TotalMinutes < serviceFinishTimeMustBeLessThanOutTime)
                                {
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ThoiGianKetThucDieuTriPhaiLonHonThoiGianKetThucDichVu___Phut, lastSereServ.TDL_SERVICE_NAME, Inventec.Common.DateTime.Convert.TimeNumberToTimeStringWithoutSecond(lastSereServEndTimeValue), serviceFinishTimeMustBeLessThanOutTime.ToString());
                                    return false;
                                }
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

        public bool IsValidOutTime(HIS_TREATMENT hisTreatment, HisTreatmentFinishSDO data, List<HIS_SERE_SERV> existedSereServs, List<HIS_PATIENT_TYPE_ALTER> ptas, HIS_PATIENT_TYPE_ALTER lastPta, HIS_DEPARTMENT_TRAN lastDt)
        {
            if (hisTreatment == null || hisTreatment.IN_TIME > data.TreatmentFinishTime)
            {
                string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(hisTreatment.IN_TIME);
                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_ThoiGianRaVienKhongDuocNhoHonThoiGianVaoVien, time);
                return false;
            }

            if (hisTreatment.CLINICAL_IN_TIME.HasValue && hisTreatment.CLINICAL_IN_TIME.Value > data.TreatmentFinishTime)
            {
                string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(hisTreatment.CLINICAL_IN_TIME.Value);
                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_ThoiGianRaVienKhongDuocNhoHonThoiGianNhapVien, time);
                return false;
            }

            if (!lastDt.DEPARTMENT_IN_TIME.HasValue)
            {
                HIS_DEPARTMENT department = HisDepartmentCFG.DATA
                    .Where(o => o.ID == lastDt.DEPARTMENT_ID)
                    .FirstOrDefault();
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_BenhNhanDangChoTiepNhanVaoKhoa, department.DEPARTMENT_NAME);
                return false;
            }

            V_HIS_ROOM hisRoom = HisRoomCFG.DATA != null ? HisRoomCFG.DATA.Where(o => o.ID == data.EndRoomId).FirstOrDefault() : null;
            if (hisRoom == null || lastDt.DEPARTMENT_ID != hisRoom.DEPARTMENT_ID)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_BenhNhanDaChuyenKhoiKhoaKhongChoPhepThucHienKetThuc);
                return false;
            }

            if (lastDt.DEPARTMENT_IN_TIME.HasValue && lastDt.DEPARTMENT_IN_TIME > data.TreatmentFinishTime)
            {
                string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(lastDt.DEPARTMENT_IN_TIME.Value);
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ThoiGianKetThucDieuTriKhongDuocNhoHonThoiGianVaoKhoa, time);
                return false;
            }

            if (lastPta == null || lastPta.LOG_TIME > data.TreatmentFinishTime)
            {
                string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(lastPta.LOG_TIME);
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ThoiGianKetThucDieuTriKhongDuocNhoHonThoiGianXacLapDoiTuongCuoiCung, time);
                return false;
            }

            //Kiem tra xem co y lenh nao co thoi gian lon hon thoi gian ra vien hay khong
            List<string> serviceReqCodes = existedSereServs != null ? existedSereServs
                .Where(o => o.AMOUNT > 0 && !o.IS_NO_EXECUTE.HasValue && o.TDL_INTRUCTION_TIME > data.TreatmentFinishTime)
                .Select(o => o.TDL_SERVICE_REQ_CODE).Distinct().ToList() : null;

            if (IsNotNullOrEmpty(serviceReqCodes))
            {
                string serviceReqCodeStr = string.Join(",", serviceReqCodes);
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_CacPhieuChiDinhCoThoiGianYLenhLonHonThoiGianRaVien, serviceReqCodeStr);

                // Neu la "Luu Tam" thi van cho tiep tuc xu ly
                if (data.IsTemporary)
                {
                    return true;
                }
                return false;
            }

            return true;
        }

        public bool FinishTimeIsNotGreatherThenCurrentTime(HisTreatmentFinishSDO data)
        {
            long currentTime = Inventec.Common.DateTime.Get.Now().Value;
            if (HisTreatmentCFG.FINISH_TIME_NOT_GREATER_THAN_CURRENT_TIME && data.TreatmentFinishTime > currentTime)
            {
                string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(currentTime);
                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_ThoiGianRaVienKhongDuocLonHonThoiGianHienTai, time);
                return false;
            }
            return true;
        }


        /// <summary>
        /// Kiem tra cac truong du lieu bat buoc khi thuc hien ket thuc treatment
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool VerifyRequireField(HisTreatmentFinishSDO data, HIS_PATIENT_TYPE_ALTER lastPta, ref HIS_PROGRAM program)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.TreatmentId)) throw new ArgumentNullException("data.TreatmentId");
                if (!IsGreaterThanZero(data.TreatmentEndTypeId)) throw new ArgumentNullException("data.TreatmentEndTypeId");

                //Neu ko nhap ket qua thi kiem tra xem co phai la Kham hay khong
                if (!IsNotNull(data.TreatmentResultId) && lastPta.TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM && lastPta.TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_BatBuocPhaiNhapKetQuaDieuTriVoiBenhNhanDieuTri);
                    return false;
                }
                if (IsNotNullOrEmpty(data.AppointmentExamRoomIds))
                {
                    List<string> executeRoomNames = HisExecuteRoomCFG.DATA
                        .Where(o => data.AppointmentExamRoomIds.Contains(o.ROOM_ID) && o.IS_EXAM != Constant.IS_TRUE)
                        .Select(o => o.EXECUTE_ROOM_NAME)
                        .ToList();
                    if (IsNotNullOrEmpty(executeRoomNames))
                    {
                        string nameStr = string.Join(",", executeRoomNames);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_KhongPhaiLaPhongKham, nameStr);
                        return false;
                    }
                }

                program = data.ProgramId.HasValue ? new HisProgramGet().GetById(data.ProgramId.Value) : null;

                if (data.CreateOutPatientMediRecord
                    || (lastPta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU && HisTreatmentCFG.MUST_SET_PROGRAM_WHEN_FINISHING_IN_PATIENT))
                {
                    if (program == null)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ChuaCoThongTinChuongTrinh);
                        return false;
                    }
                    if (HisTreatmentCFG.AUTO_STORE_MEDI_RECORD_BY_PROGRAM && !program.DATA_STORE_ID.HasValue)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ChuongTrinhChuaThietLapTuBenhAn, program.PROGRAM_NAME);
                        return false;
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        public bool VerifyRequireField2(HisTreatmentFinishSDO data, HIS_PATIENT_TYPE_ALTER lastPta, ref HIS_PROGRAM program)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.TreatmentId)) throw new ArgumentNullException("data.TreatmentId");

                if (IsNotNullOrEmpty(data.AppointmentExamRoomIds))
                {
                    List<string> executeRoomNames = HisExecuteRoomCFG.DATA
                        .Where(o => data.AppointmentExamRoomIds.Contains(o.ROOM_ID) && o.IS_EXAM != Constant.IS_TRUE)
                        .Select(o => o.EXECUTE_ROOM_NAME)
                        .ToList();
                    if (IsNotNullOrEmpty(executeRoomNames))
                    {
                        string nameStr = string.Join(",", executeRoomNames);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_KhongPhaiLaPhongKham, nameStr);
                        return false;
                    }
                }

                program = data.ProgramId.HasValue ? new HisProgramGet().GetById(data.ProgramId.Value) : null;

                if (data.CreateOutPatientMediRecord
                    || (lastPta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU && HisTreatmentCFG.MUST_SET_PROGRAM_WHEN_FINISHING_IN_PATIENT))
                {
                    if (program == null)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ChuaCoThongTinChuongTrinh);
                        return false;
                    }
                    if (HisTreatmentCFG.AUTO_STORE_MEDI_RECORD_BY_PROGRAM && !program.DATA_STORE_ID.HasValue)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ChuongTrinhChuaThietLapTuBenhAn, program.PROGRAM_NAME);
                        return false;
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        public bool HasXml4210CollinearFolderPath()
        {
            bool valid = true;
            try
            {
                if (string.IsNullOrWhiteSpace(HisTreatmentCFG.XML4210_COLLINEAR_FOLDER_PATH))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ChuaCauHinhThuMucLuuXmlThongTuyen);
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

        public bool VerifyBlockAppointment(HisTreatmentFinishSDO data, HIS_PATIENT_TYPE_ALTER lastPta)
        {
            bool valid = true;
            try
            {
                if (HisTreatmentCFG.BLOCK_APPOINTMENT_OPTION == 1
                    && data.TreatmentEndTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__HEN
                    && lastPta.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                {
                    if (lastPta.RIGHT_ROUTE_CODE == MOS.LibraryHein.Bhyt.HeinRightRoute.HeinRightRouteCode.FALSE)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_BenhNhanBhytTraiTuyenKhongChoPhepHenKham);
                        return false;
                    }

                    if (lastPta.HEIN_CARD_TO_TIME.HasValue && data.AppointmentTime.HasValue)
                    {
                        DateTime toData = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(lastPta.HEIN_CARD_TO_TIME.Value).Value;
                        DateTime appointmentDate = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.AppointmentTime.Value).Value;
                        if (appointmentDate.Date > toData.Date)
                        {
                            string to_str = Inventec.Common.DateTime.Convert.TimeNumberToDateString(lastPta.HEIN_CARD_TO_TIME.Value);
                            string appoint_str = Inventec.Common.DateTime.Convert.TimeNumberToDateString(data.AppointmentTime.Value);
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_NgayHenKhamLoHonNgayHanDenTheBHYT, appoint_str, to_str);
                            return false;
                        }
                    }
                }
                else if (HisTreatmentCFG.BLOCK_APPOINTMENT_OPTION == 2
                    && lastPta.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT  // Doi tuong benh nhan cua dien doi tuong moi nhat tuong ung voi ho so la BHYT theo cau hinh
                    && lastPta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM  // Dien dieu tri cua dien doi tuong moi nhat tuong ung voi ho so la Kham
                    && lastPta.RIGHT_ROUTE_TYPE_CODE == MOS.LibraryHein.Bhyt.HeinRightRouteType.HeinRightRouteTypeCode.APPOINTMENT  // Loai cua dien doi tuong moi nhat tuong ung voi ho so la Trai Tuyen Hen Kham
                    && data.TreatmentEndTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__HEN)  // Loai ket thuc dieu tri la Hen Kham
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_BenhNhanTraiTuyenHenKhamKhongChoPhepXuTriKetThucVoiLoaiLaHenKham);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        public bool VerifyApproveRation(List<HIS_SERVICE_REQ> serviceReqs, HIS_PATIENT_TYPE_ALTER lastPta)
        {
            bool valid = true;
            try
            {
                if (HisTreatmentCFG.MUST_APPROVE_RATION_BEFORE_FINISH != HisTreatmentCFG.MustApproveRationBeforeFinishTreatment.BLOCK)
                {
                    return true;
                }

                if (lastPta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                {
                    return true;
                }

                List<HIS_SERVICE_REQ> reqRations = serviceReqs != null ?
                    serviceReqs.Where(o => o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__AN).ToList() : null;

                if (!IsNotNullOrEmpty(reqRations))
                {
                    return true;
                }

                List<long> rationIds = reqRations.Where(o => o.RATION_SUM_ID.HasValue).Select(s => s.RATION_SUM_ID.Value).ToList();

                List<HIS_RATION_SUM> rationSums = null;

                if (IsNotNullOrEmpty(rationIds))
                {
                    HisRationSumFilterQuery rationFilter = new HisRationSumFilterQuery();
                    rationFilter.IDs = rationIds;
                    rationFilter.RATION_SUM_STT_IDs = new List<long>() { IMSys.DbConfig.HIS_RS.HIS_RATION_SUM_STT.ID__REQ, IMSys.DbConfig.HIS_RS.HIS_RATION_SUM_STT.ID__REJECT };
                    rationSums = new HisRationSumGet().Get(rationFilter);
                }

                List<string> reqNotRationSums = reqRations.Where(o => !o.RATION_SUM_ID.HasValue).Select(s => s.SERVICE_REQ_CODE).ToList();

                if (IsNotNullOrEmpty(reqNotRationSums) || IsNotNullOrEmpty(rationSums))
                {
                    List<string> msgs = new List<string>();

                    if (IsNotNullOrEmpty(rationSums))
                    {
                        foreach (var item in rationSums)
                        {
                            string maYLenhs = string.Join(",", reqRations.Where(o => o.RATION_SUM_ID == item.ID).Select(s => s.SERVICE_REQ_CODE).ToList());
                            msgs.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisServiceReq_MaPhieuTongHopSuatAnMaYLenh, param.LanguageCode), item.RATION_SUM_CODE, maYLenhs));
                        }
                    }

                    if (IsNotNullOrEmpty(reqNotRationSums))
                    {
                        string maYLenhs = string.Join(",", reqNotRationSums);
                        msgs.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisServiceReq_ChuaTongHopSuatAnMaYLenh, param.LanguageCode), maYLenhs));
                    }

                    string messages = String.Join(".\n", msgs);

                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_YLenhChuaTongHopHoacDuyetSuatAn, messages);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        public bool IsValidDeathCertBook(HisTreatmentFinishSDO data, WorkPlaceSDO workPlace, ref V_HIS_DEATH_CERT_BOOK deathCertBook)
        {
            bool valid = true;
            try
            {
                if (data.DeathCertBookId.HasValue && data.TreatmentEndTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHET)
                {
                    deathCertBook = new HisDeathCertBookGet().GetViewById(data.DeathCertBookId.Value);

                    if (deathCertBook == null)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_SoChungTuKhongTonTai);
                        return false;
                    }

                    if (deathCertBook.IS_ACTIVE != Constant.IS_TRUE)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_SoChungTuDangBiKhoa, deathCertBook.DEATH_CERT_BOOK_NAME);
                        return false;
                    }

                    if (deathCertBook.CURRENT_DEATH_CERT_NUM.HasValue && deathCertBook.CURRENT_DEATH_CERT_NUM >= (deathCertBook.FROM_NUM_ORDER + deathCertBook.TOTAL - 1))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_SoChungTuDaHetSo, deathCertBook.DEATH_CERT_BOOK_NAME);
                        return false;
                    }

                    if (workPlace != null && deathCertBook.BRANCH_ID.HasValue && deathCertBook.BRANCH_ID != workPlace.BranchId)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_SoChungTuThuocChiNhanhKhac, deathCertBook.DEATH_CERT_BOOK_NAME);
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }


        internal bool IsValidMinimumTimesForExam(HIS_TREATMENT treatment, List<HIS_SERVICE_REQ> serviceReqs, long outTime)
        {
            bool result = true;
            if (HisTreatmentCFG.MINIMUM_TIMES_FOR_EXAM_WITH_SUBCLINICALS.HasValue)
            {
                List<long> serviceTypeIds = new List<long>()
                {
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__CDHA,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__NS,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__SA,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TDCN,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TT,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT
                };

                if (IsNotNullOrEmpty(serviceReqs) && outTime > treatment.IN_TIME)
                {
                    var timeBetweenInAndOut = ProcessOutAndInTime(treatment.IN_TIME, outTime);

                    if (timeBetweenInAndOut > 0)
                    {
                        bool isNotValid = true;
                        isNotValid = isNotValid && (treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM);
                        isNotValid = isNotValid && (treatment.TDL_PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT);
                        isNotValid = isNotValid && (treatment.IS_EMERGENCY != Constant.IS_TRUE);
                        isNotValid = isNotValid && serviceReqs.Exists(o => o.IS_NO_EXECUTE != Constant.IS_TRUE && o.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT && IsNotNullOrEmpty(serviceTypeIds) && serviceTypeIds.Contains(o.SERVICE_REQ_TYPE_ID));
                        isNotValid = isNotValid && (Math.Floor(timeBetweenInAndOut) <= HisTreatmentCFG.MINIMUM_TIMES_FOR_EXAM_WITH_SUBCLINICALS.Value);

                        if (isNotValid)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_BenhNhanCoThoiGianKhamVaCLSItHonPhutChoPhepKetThuc, HisTreatmentCFG.MINIMUM_TIMES_FOR_EXAM_WITH_SUBCLINICALS.Value.ToString());
                            result = false;
                        }
                    }
                    else
                    {
                        LogSystem.Info(string.Format("Thoi gian vao vien lon hon thoi gian ra vien theo TREATMENT_CODE: {0}", treatment.TREATMENT_CODE));
                    }
                }
                else
                {
                    LogSystem.Info(string.Format("Khong co thong tin thoi gian ra vien hoac khong co serviceReps theo TREATMENT_CODE: {0}", treatment.TREATMENT_CODE));
                }
            }
            else
            {
                LogSystem.Info("Key cau hinh khai bao so phut cho cho phep ket thu dieu tri voi dien dieu tri kham chua duoc khai bao");
            }
            return result;
        }

        private double ProcessOutAndInTime(long inTime, long outTime)
        {
            DateTime? dateInTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(inTime);
            DateTime? dateOutTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(outTime);
            double minutes = dateInTime.HasValue && dateOutTime.HasValue ? (dateOutTime - dateInTime).Value.TotalMinutes : 0;
            return minutes;
        }

        public bool IsIntructionTimeNotGreaterThanFinishTime(List<HIS_SERVICE_REQ> serviceReqs)
        {
            bool result = true;
            if (HisTreatmentCFG.IS_INTRUCTION_TIME_MUST_NOT_GREATER_THAN_FINISH_TIME && IsNotNullOrEmpty(serviceReqs))
            {
                List<long> inValidTypeIds = new List<long>()
                {
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONDT,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONTT,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONM
                };

                var serviceReqNotValid = serviceReqs.Where(w => !inValidTypeIds.Contains(w.SERVICE_REQ_TYPE_ID) && w.INTRUCTION_TIME > w.FINISH_TIME).ToList();
                if (IsNotNullOrEmpty(serviceReqNotValid))
                {
                    var ids = serviceReqNotValid.Select(s => s.SERVICE_REQ_CODE).ToList();
                    string codes = string.Join(", ", ids);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_CacYLenhCoThoiGianChiDinhLonHonThoiGianKetThuc, codes);
                    result = false;
                }
            }
            return result;
        }

        internal bool IsValidMinProcessTime(long? startTime, long? finishTime, List<HIS_SERE_SERV> recentSereServs)
        {
            bool valid = true;
            try
            {
                if (startTime.HasValue && finishTime.HasValue && startTime > 0 && finishTime > 0
                    && recentSereServs != null && recentSereServs.Count > 0)
                {
                    DateTime? beginTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(startTime.Value);
                    DateTime? endTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(finishTime.Value);
                    var listServiceIds = recentSereServs.Select(o => o.SERVICE_ID).ToList() ?? new List<long>();
                    var listServices = HisServiceCFG.HAS_MIN_PROCESS_TIME_DATA_VIEW.Where(o => listServiceIds.Contains(o.ID)).ToList() ?? new List<V_HIS_SERVICE>();
                    if (beginTime.HasValue && endTime.HasValue)
                    {
                        List<long> listMinProcessTime_Invalid = new List<long>();
                        foreach (var sereServ in recentSereServs)
                        {
                            double minutes = (endTime - beginTime).Value.TotalMinutes;
                            var service = IsNotNull(sereServ) ? listServices.Where(o => o.ID == sereServ.SERVICE_ID).FirstOrDefault() : null;
                            var minProcessTime = IsNotNull(service) && service.MIN_PROCESS_TIME.HasValue ? service.MIN_PROCESS_TIME.Value : 0;

                            string patientType = sereServ.PATIENT_TYPE_ID.ToString();

                            List<string> listPatientTypeNotCheckMin = IsNotNull(service) && !string.IsNullOrWhiteSpace(service.MIN_PROC_TIME_EXCEPT_PATY_IDS)
                                 ? service.MIN_PROC_TIME_EXCEPT_PATY_IDS.Split(',').ToList() : null;
                            bool isNotCheckMinTime = IsNotNullOrEmpty(listPatientTypeNotCheckMin) ? listPatientTypeNotCheckMin.Contains(patientType) : false;

                            if (!isNotCheckMinTime && minProcessTime > 0 && minutes < minProcessTime)
                            {
                                listMinProcessTime_Invalid.Add(minProcessTime);
                            }
                        }
                        if (listMinProcessTime_Invalid != null && listMinProcessTime_Invalid.Count > 0)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_BenhNhanCoThoiGianThucHienKhamItHonSoPhut, listMinProcessTime_Invalid.Max().ToString());
                            return false;
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

        internal bool IsFinishMandatoryService(HIS_TREATMENT treatment, List<HIS_SERE_SERV> existsSereServs)
        {
            bool valid = true;
            try
            {
                HIS_TREATMENT_TYPE treatmentType = HisTreatmentTypeCFG.DATA.Where(o => o.ID == treatment.TDL_TREATMENT_TYPE_ID).FirstOrDefault();

                if (treatmentType.REQUIRED_SERVICE_ID != null)
                {
                    if (!existsSereServs.Exists(o => o.SERVICE_ID == treatmentType.REQUIRED_SERVICE_ID &&
                        o.IS_DELETE != Constant.IS_TRUE && o.IS_NO_EXECUTE != Constant.IS_TRUE))
                    {
                        V_HIS_SERVICE vservice = HisServiceCFG.DATA_VIEW.Where(o => o.ID == treatmentType.REQUIRED_SERVICE_ID).FirstOrDefault();
                        string sv = string.Format("{0}-{1}", vservice.SERVICE_CODE, vservice.SERVICE_NAME);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_BanCanChiDinhDichVu, sv);
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

        internal bool IsValidIcdCode(HIS_TREATMENT treatment, HisTreatmentFinishSDO data)
        {
            bool valid = true;
            try
            {
                HIS_ICD icd = HisIcdCFG.DATA.Where(o =>o.ICD_CODE == data.IcdCode).FirstOrDefault();
                if (icd != null && icd.DO_NOT_USE_HEIN == Constant.IS_TRUE)
                {
                    if (data != null && treatment.TDL_PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisIcd_MaBenhKhongDuocBaoHiemYTeThanhToan, data.IcdCode);
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

        internal bool IsValidFinishTime(OutPatientPresSDO data, HIS_SERVICE_REQ parent)
        {
            bool valid = true;
            try
            {
                if (parent != null && data.TreatmentFinishSDO != null && parent.START_TIME > data.TreatmentFinishSDO.TreatmentFinishTime)
                {
                    string beginTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(parent.START_TIME.Value);
                    string endTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.TreatmentFinishSDO.TreatmentFinishTime);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhCoThoiGianXuLyLonHonThoiGianKetthucDieutri
, parent.SERVICE_REQ_CODE, beginTime, endTime);
                    return false;

                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return valid;
        }

        //ktra thông tin pha thai
        internal bool IsValidAbortionInfo(HisTreatmentFinishSDO data)
        {
            bool valid = true;
            try
            {
                if (data != null
                    && ((data.TreatmentEndTypeExtId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__NGHI_OM) || (data.TreatmentEndTypeExtId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__NGHI_DUONG_THAI))
                    && (data.IsPregnancyTermination == true)
                    && ((data.GestationalAge == null) || (data.PregnancyTerminationReason == null) || (data.PregnancyTerminationTime == null)))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ThieuThongTinTuoiThaiHoacLyDoDinhChiThai);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return valid;
        }

        //ktra đơn tạm
        internal bool IsValidTemporaryPres(List<HIS_SERVICE_REQ> serviceReqs)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(serviceReqs))
                {
                    List<HIS_SERVICE_REQ> srs = serviceReqs.Where(o => o.IS_TEMPORARY_PRES == Constant.IS_TRUE).ToList();
                    if (IsNotNullOrEmpty(srs))
                    {
                        long hisServiceReq = srs.Count();
                        string serviceReqCodes = string.Join(",", srs.Select(s => s.SERVICE_REQ_CODE).Distinct());
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_HoSoConDonThuocTamChuaXuLy, hisServiceReq.ToString(), serviceReqCodes);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return valid;
        }

        //không cho phép kết thúc điều trị nếu tồn tại giao dịch đang bị khóa
        internal bool DoNotFinishTransactionNotIsActive(HisTreatmentFinishSDO data)
        {
            bool valid = true;
            try
            {
                if (data != null && !data.IsTemporary)
                {
                    HisTransactionFilterQuery filter = new HisTransactionFilterQuery();
                    filter.TREATMENT_ID = data.TreatmentId;
                    filter.IS_DELETE = Constant.IS_FALSE;
                    filter.IS_CANCEL = false;
                    filter.IS_ACTIVE = Constant.IS_FALSE;
                    List<HIS_TRANSACTION> transations = new HisTransactionGet().Get(filter);
                    if (IsNotNullOrEmpty(transations))
                    {
                        List<string> transactionCodes = transations.Select(o => o.TRANSACTION_CODE).ToList();
                        if (IsNotNullOrEmpty(transactionCodes))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_GiaoDichDangBiTamKhoa, string.Join(", ", transactionCodes));
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
